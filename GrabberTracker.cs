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

        //static fields for practice mode lock progess
        private static bool isCastle;
        private static BreakableLock castleLock;
        private static float castleLockImpact;

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
                if (isCastle && !castleLock.shattered)
                {
                    textVisuals.text = $"Practice Mode\nLock Progress: {castleLockImpact}/300\nGrabs: {grabs}";
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
        //checks if the level being loaded is castle & instantiates castleLock if so
        [HarmonyPatch(typeof(Game), "LevelLoaded")]
        [HarmonyPrefix]
        private static void LevelLoaded()
        {
            grabsAtCP = grabsAtLevel = grabs;

            if (SceneManager.GetActiveScene().name.Equals("Siege"))
            {
                isCastle = true;
                castleLock = GameObject.Find("Prison-Walls.001").GetComponentInChildren<BreakableLock>();
            }
            else
            {
                isCastle = false;
            }
        }

        //update grabsAtCP when entering a new checkpoint
        [HarmonyPatch(typeof(Game), "EnterCheckpoint")]
        [HarmonyPrefix]
        private static void EnterCheckpoint(int checkpoint, int subObjectives)
        {
            if (checkpoint > Game.instance.currentCheckpointNumber)
            {
                grabsAtCP = grabs;
            }
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
        [HarmonyPatch(typeof(HumanAPI.Level), "Reset")]
        [HarmonyPostfix]
        private static void Reset(int checkpoint, int subObjectives)
        {
            if (isPractice)
            {
                grabs = grabsAtCP;
                refreshGrabText();
            }
        }

        //increment grabs counter when the game detects a grab; 
        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            grabs++;
            refreshGrabText();
        }

        [HarmonyPatch(typeof(HumanAPI.Breakable), "FixedUpdateOnImpact")]
        [HarmonyPostfix]
        private static void FixedUpdateOnImpact(float ___accumulatedImpact)
        {
            castleLockImpact = ___accumulatedImpact;
            refreshGrabText();
        }
    }
}