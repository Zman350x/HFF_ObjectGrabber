using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;

namespace ObjectGrabber
{
    using HarmonyLib;
    using UnityEngine;
    using TMPro;

    [BepInPlugin("org.bepinex.plugins.humanfallflat.objectgrabber", "Grab Count Tracker", "1.2.0")]
    [BepInProcess("Human.exe")]
    public class GrabberTracker : BaseUnityPlugin
    {
        public static bool isEnabled;

        private static uint grabs; //current grab count
        private static uint grabsAtCP; //grab count as of start of current checkpoint
        private static uint grabsAtLevel; //grab count as of start of current level

        private static GameObject textObj;
        private static TextMeshProUGUI textVisuals;

        static GrabberTracker()
        {
            isEnabled = false;
            grabs = 0;

            setupTMP(ref textObj, ref textVisuals, new Vector3(106.4688f, 9f, 0f));
            refreshGrabText();
            textObj.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker), "GrabberTracker");

            //reset grab counter, update text
            Shell.RegisterCommand("grab_reset", (string x) => {
                grabs = grabsAtCP = grabsAtLevel = 0;
                refreshGrabText();
                Shell.Print("Grab counter reset");
            }, "Reset grab counter to 0");

            //toggle display of grab counter, reset grab counter, update text
            Shell.RegisterCommand("grab_toggle", (string x) => {
                isEnabled = !isEnabled;
                textObj.SetActive(isEnabled);
                grabs = grabsAtCP = grabsAtLevel = 0;
                refreshGrabText();
                Shell.Print(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
            }, "Toggle grab counter");
        }

        //reset grab counter to what it was at the start of the checkpoint
        [HarmonyPatch(typeof(HumanAPI.Level), "Reset")]
        [HarmonyPostfix]
        private static void Reset(int checkpoint, int subObjectives)
        {
            grabs = grabsAtCP;
			refreshGrabText();
		}

        //update grabs when reloading a checkpoint to current grab counter
        [HarmonyPatch(typeof(Game), "EnterCheckpoint")]
        [HarmonyPrefix]
        private static void EnterCheckpoint(int checkpoint, int subObjectives)
        {
            if (checkpoint > Game.instance.currentCheckpointNumber)
            {
                grabsAtCP = grabs;
            }
        }

        [HarmonyPatch(typeof(Game), "LevelLoaded")] // when a level finishes loading
        [HarmonyPrefix]
        private static void LevelLoaded()
        {
            grabsAtCP = grabsAtLevel = grabs; // update grabs when restarting level/checkpoint to current grab counter
        }

        [HarmonyPatch(typeof(Game), "RestartLevel")] // when a level is restarted
        [HarmonyPostfix]
        private static void RestartLevel(bool reset = true)
        {
            grabs = grabsAtLevel; // reset grab counter to what it was at the start of the level
			refreshGrabText();
		}

        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            grabs++;
            refreshGrabText();
        }

        private static void refreshGrabText()
        {
            textVisuals.text = "Grabs: " + grabs;
        }

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
            textContent.alignment = TextAlignmentOptions.BaselineLeft;

            RectTransform textRect = gameObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.zero;
            textRect.anchoredPosition3D = coords;

            gameObj.transform.localRotation = Quaternion.identity;
            gameObj.transform.localScale = Vector3.one;
            gameObj.layer = LayerMask.NameToLayer("UI");
        }
    }
}