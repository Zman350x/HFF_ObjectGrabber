using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;

namespace ObjectGrabber
{
    using HarmonyLib;
    using UnityEngine;
    using TMPro;

    [BepInPlugin("org.bepinex.plugins.humanfallflat.objectgrabber", "Grab Count Tracker", "1.0.0")]
    [BepInProcess("Human.exe")]
    public class GrabberTracker : BaseUnityPlugin
    {
        public static GrabberTracker instance;
        public bool isEnabled;

        private uint grabs;

        private GameObject textObj;
        private TextMeshProUGUI textVisuals;

        public void Start()
        {
            instance = this;
            isEnabled = false;
            grabs = 0;

            setupTMP();
            textVisuals.text = "Grabs: " + grabs;
            textObj.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker));

            Shell.RegisterCommand("grab_reset", (string x) => {
                grabs = 0; textVisuals.text = "Grabs: " + grabs;
                Shell.Print("Grab counter reset");
            }, "Reset grab counter to 0");

            Shell.RegisterCommand("grab_toggle", (string x) => {
                isEnabled = !isEnabled; textObj.SetActive(isEnabled);
                grabs = 0; textVisuals.text = "Grabs: " + grabs;
                Shell.Print(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
            },"Enable/disable grab counter");
        }

        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            instance.grabs++;
            instance.textVisuals.text = "Grabs: " + instance.grabs;
        }

        private void setupTMP()
        {
            //comments show values for old font setup
            textObj = new GameObject("GrabberText");
            textObj.transform.parent = GameObject.Find("Menu").transform;
            textObj.AddComponent<CanvasRenderer>();

            textVisuals = textObj.AddComponent<TextMeshProUGUI>();
            textVisuals.color = Color.white; //black
            textVisuals.fontSize = 50; //60
            textVisuals.font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Single(font => font.name == "Blogger_Sans-Bold SDF"); //Menu SDF
            textVisuals.fontMaterial = Resources.FindObjectsOfTypeAll<Material>().Single(material => material.name == "Blogger_Sans-Bold SDF Instruction"); //remove line
            textVisuals.enableWordWrapping = false;
            textVisuals.alignment = TextAlignmentOptions.BaselineLeft;

            textObj.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            textObj.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            textObj.transform.localPosition = new Vector3(-854, -531, 0); //-858, -534, 0
            textObj.transform.localRotation = Quaternion.identity;
            textObj.transform.localScale = Vector3.one;
            textObj.layer = LayerMask.NameToLayer("UI");
        }
    }
}