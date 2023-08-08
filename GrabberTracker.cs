using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectGrabber
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using BepInEx;
    using HarmonyLib;
    using TMPro;
    using HumanAPI;
    using Multiplayer;

    public enum LevelName
    {
        Menu = -1,
        Mansion = 0,
        Train = 1,
        Carry = 2,
        Mountain = 3,
        Demolition = 4,
        Castle = 5,
        Water = 6,
        PowerPlant = 7,
        Aztec = 8,
        Dark = 9,
        Steam = 10,
        Ice = 11,
        Reprise = 12,
        Credits = 13,
        Thermal = 14,
        Factory = 15,
        Golf = 16,
        City = 17,
        Forest = 18,
        Laboratory = 19,
        Lumber = 20,
        RedRock = 21,
        Tower = 22,
        Miniature = 23,
        CopperWorld = 24,
        Workshop = 25,
        Lobby = 26,
        Other = 27
    }

    [BepInPlugin("org.bepinex.plugins.humanfallflat.objectgrabber", "Grab Count Tracker", "1.2.0")]
    [BepInProcess("Human.exe")]
    public class GrabberTracker : BaseUnityPlugin
    {
        //mod modes
        public static bool isEnabled;
        public static bool isPractice;

        private static uint grabs; //current grab count
        private static uint grabsAtCP; //grab count as of start of current checkpoint
        private static uint grabsAtLevel; //grab count as of start of current level

        //static fields for on-screen TMP text
        private static GameObject textObj;
        private static TextMeshProUGUI textVisuals;

        //static fields for practice mode tools
        private static LevelName currentLevel;
        private static BreakableLock castleLock;
        private static float castleLockImpact;
        private static bool isPassedLoadingZone;

        static GrabberTracker()
        {
            //default mod modes
            isEnabled = false;
            isPractice = false;

            //reset counters
            grabsAtLevel = 0;
            grabsAtCP = 0;
            grabs = 0;

            //setup on-screen TMP text
            setupTMP(ref textObj, ref textVisuals, new Vector3(106.4688f, 22f, 0f));
            refreshGrabText();
            textObj.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker), "GrabberTracker");

            //reset grab counter; update text
            Shell.RegisterCommand("grab_reset", (string x) => {
                grabs = grabsAtCP = grabsAtLevel = 0;
                refreshGrabText();
                Shell.Print("Grab counter reset");
            }, "Reset grab counter to 0");

            //toggle display of grab counter; reset grab counter; update text
            Shell.RegisterCommand("grab_toggle", (string x) => {
                isEnabled = !isEnabled;
                textObj.SetActive(isEnabled);
                grabs = grabsAtCP = grabsAtLevel = 0;
                refreshGrabText();
                Shell.Print(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
            }, "Toggle grab counter");

            //toggle display of grab counter; reset grab counter; update text
            Shell.RegisterCommand("grab_practice", (string x) => {
                isPractice = !isPractice;
                refreshGrabText();
                Shell.Print(isPractice ? "Practice mode enabled" : "Practice mode disabled");
            }, "Toggle grab counter practice mode");
        }

        //because everything is static, the instance created by BepInEx isn't required
        public void Awake()
        {
            Destroy(this);
        }

        //update the TMP text on-screen
        private static void refreshGrabText()
        {
            //Adds "Practice Mode" to text if in practice mode
            if (isPractice)
            {
                //Adds lock pogress to text if in practice mode and the castle lock hasn't shattered yet
                if (currentLevel == LevelName.Castle && !castleLock.shattered)
                {
                    textVisuals.text = $"Practice Mode\nLock Progress: {castleLockImpact}/300\nGrabs: {grabs}";
                }
                else if (currentLevel == LevelName.Water)
                {
                    textVisuals.text = $"Practice Mode\nLoading Zone Triggered: {isPassedLoadingZone}\nGrabs: {grabs}";
                }
                else
                {
                    textVisuals.text = $"Practice Mode\nGrabs: {grabs}";
                }
            }
            else
            {
                textVisuals.text = $"Grabs: {grabs}";
            }
        }

        //create TMP object and set all necessary properties
        private static void setupTMP(ref GameObject gameObj, ref TextMeshProUGUI textContent, Vector3 coords)
        {
            gameObj = new GameObject("GrabberText");
            gameObj.transform.parent = GameObject.Find("Menu").transform;
            gameObj.AddComponent<CanvasRenderer>();

            textContent = gameObj.AddComponent<TextMeshProUGUI>();
            textContent.color = Color.white;
            textContent.fontSize = 50;
            textContent.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>()
                .Single(font => font.name == "Blogger_Sans-Bold SDF");
            textContent.fontMaterial = Resources.FindObjectsOfTypeAll<Material>()
                .Single(material => material.name == "Blogger_Sans-Bold SDF Instruction");
            textContent.enableWordWrapping = false;
            textContent.autoSizeTextContainer = false;
            textContent.enableAutoSizing = false;
            textContent.alignment = TextAlignmentOptions.BottomLeft;

            RectTransform textRect = gameObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.zero;
            textRect.anchoredPosition3D = coords;

            gameObj.transform.localRotation = Quaternion.identity;
            gameObj.transform.localScale = Vector3.one;
            gameObj.layer = LayerMask.NameToLayer("UI");
        }

        //update GrabsAtLevel and grabsAtCP when entering a new level
        //sets the currentLevel & loads any practice tools if needed
        [HarmonyPatch(typeof(Game), "AfterLoad")]
        [HarmonyPrefix]
        private static void AfterLoad(int checkpointNumber, int subobjectives)
        {
            grabsAtCP = grabsAtLevel = grabs;
            isPassedLoadingZone = false;

            //set currentLevel based on level loaded
            switch (Game.instance.currentLevelType)
            {
                case WorkshopItemSource.BuiltIn:
                {
                    currentLevel = (LevelName)Game.instance.currentLevelNumber;
                    break;
                }
                case WorkshopItemSource.EditorPick:
                {
                    currentLevel = (LevelName) Game.instance.currentLevelNumber + 14;
                    break;
                }
                case WorkshopItemSource.Subscription:
	            case WorkshopItemSource.LocalWorkshop:
                {
                    currentLevel = LevelName.Workshop;
                    break;
                }
                case WorkshopItemSource.BuiltInLobbies:
	            case WorkshopItemSource.SubscriptionLobbies:
                {
                    currentLevel = LevelName.Lobby;
                    break;
                }
                default:
                {
                    currentLevel = LevelName.Other;
                    break;
                }
            }

            //AfterLoad() is only ever called in the Inactive state when loading a lobby
            if (Game.instance.state == GameState.Inactive)
            {
                currentLevel = LevelName.Lobby;
            }

            //load practice tools based on level if needed
            switch (currentLevel)
            {
                case LevelName.Castle:
                {
                    castleLock = GameObject.Find("Prison-Walls.001").GetComponentInChildren<BreakableLock>();
                    break;
                }
                default: { break; }
            }

            refreshGrabText();
        }

        //update grabsAtCP when entering a new checkpoint
        [HarmonyPatch(typeof(Game), "EnterCheckpoint")]
        [HarmonyPrefix]
        private static void EnterCheckpoint(int checkpoint, int subObjectives)
        {
            bool flag = false;
            if (Game.instance.currentCheckpointNumber < checkpoint)
            {
                flag = true;
            }
            else if (Game.currentLevel.nonLinearCheckpoints && Game.instance.currentCheckpointNumber != checkpoint)
            {
                flag = true;
            }
            else if (Game.instance.currentCheckpointNumber == checkpoint && subObjectives != 0)
            {
                flag = true;
            }

            if (flag)
            {
                grabsAtCP = grabs;
            }
        }

        //update grabsAtCP when entering a new checkpoint
        [HarmonyPatch(typeof(Game), "EnterPassZone")]
        [HarmonyPrefix]
        private static void EnterPassZone()
        {
            isPassedLoadingZone = true;
            refreshGrabText();
        }

        //reset grab counter to what it was at the start of the level
        [HarmonyPatch(typeof(Game), "RestartLevel")]
        [HarmonyPostfix]
        private static void RestartLevel(bool reset = true)
        {
            if (isPractice)
            {
                grabs = grabsAtCP = grabsAtLevel;
                refreshGrabText();
            }
        }

        //reset grab counter to what it was at the start of the checkpoint, if in pracice mode
        [HarmonyPatch(typeof(Level), "Reset")]
        [HarmonyPostfix]
        private static void Reset(int checkpoint, int subObjectives)
        {
            if (isPractice)
            {
                grabs = grabsAtCP;
                refreshGrabText();
            }
        }

        //set the currentLevel to Menu
        [HarmonyPatch(typeof(App), "EnterMenu")]
        [HarmonyPostfix]
        private static void EnterMenu()
        {
            currentLevel = LevelName.Menu;
            refreshGrabText();
        }

        //increment grabs counter when the game detects a grab; 
        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            grabs++;
            refreshGrabText();
        }

        [HarmonyPatch(typeof(Breakable), "FixedUpdateOnImpact")]
        [HarmonyPostfix]
        private static void FixedUpdateOnImpact(float ___accumulatedImpact)
        {
            castleLockImpact = ___accumulatedImpact;
            refreshGrabText();
        }
    }
}