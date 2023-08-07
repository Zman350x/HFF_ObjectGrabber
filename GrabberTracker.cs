using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;

namespace ObjectGrabber
{
    using HarmonyLib;
    using UnityEngine;
    using TMPro;

    [BepInPlugin("org.bepinex.plugins.humanfallflat.objectgrabber", "Grab Count Tracker", "1.1.0")]
    [BepInProcess("Human.exe")]
    public class GrabberTracker : BaseUnityPlugin
    {
        public static GrabberTracker instance;
        public bool isEnabled;

        private static uint grabs; // changed from private to private static in case we ever want to grab this var with autosplitter
        private static uint grabsAtCP; // set only once per checkpoint reached
        private static uint grabsAtLevel; // set only at beginning of each level

        private GameObject textObj;
        private TextMeshProUGUI textVisuals;

        public void Start()
        {
            instance = this;
            isEnabled = false;
            grabs = 0;

            setupTMP(ref textObj, ref textVisuals, new Vector3(106.4688f, 9f, 0f));
            textVisuals.text = "Grabs: " + grabs;
            textObj.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker));

            Shell.RegisterCommand("grab_reset", (string x) => { // reset grab counter, update text
                grabs = grabsAtCP = 0;
                textVisuals.text = "Grabs: " + grabs;
                Shell.Print("Grab counter reset");
            }, "Reset grab counter to 0");

            Shell.RegisterCommand("grab_toggle", (string x) => { // reset grab counter, update text, toggle display of grab counter
                isEnabled = !isEnabled;
                textObj.SetActive(isEnabled);
                grabs = grabsAtCP = 0;
                textVisuals.text = "Grabs: " + grabs;
                Shell.Print(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
            }, "Toggle grab counter");
        }

        [HarmonyPatch(typeof(HumanAPI.Level), "Reset")] // for checkpoint reloads; Game.RestartCheckpoint() was giving me problems
        [HarmonyPostfix]
        private static void Reset(int checkpoint, int subObjectives)
        {
            grabs = grabsAtCP; // reset grab counter to what it was at the start of the checkpoint
			refreshGrabText();
		}

        [HarmonyPatch(typeof(Game), "EnterCheckpoint")] // for when you enter a checkpoint
        [HarmonyPrefix]
        private static void EnterCheckpoint(int checkpoint, int subObjectives)
        {
            if (checkpoint > Game.instance.currentCheckpointNumber)
            {
                grabsAtCP = grabs; // update grabs when reloading a checkpoint to current grab counter
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
            instance.textVisuals.text = "Grabs: " + grabs;
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