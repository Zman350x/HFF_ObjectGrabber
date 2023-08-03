using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
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

        private GameObject textObj2;
        private TextMeshProUGUI textVisuals2;

        private TMP_FontAsset font;

        public void Start()
        {
            instance = this;
            isEnabled = false;
            grabs = 0;

            font = UnifontInfo.createFont();

            setupTMP(ref textObj, ref textVisuals, new Vector3(-854, -531, 0));
            setupTMP(ref textObj2, ref textVisuals2, new Vector3(-854, -471, 0));
            textVisuals.text = "Grabs: " + grabs;
            textObj.SetActive(isEnabled);
            textObj2.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker));

            Shell.RegisterCommand("grab_reset", (string x) => {
                grabs = 0; textVisuals.text = "Grabs: " + grabs;
                Shell.Print("Grab counter reset");
            }, "Reset grab counter to 0");

            Shell.RegisterCommand("grab_toggle", (string x) => {
                isEnabled = !isEnabled; textObj.SetActive(isEnabled); textObj2.SetActive(isEnabled);
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

        [HarmonyPatch(typeof(HumanAPI.Breakable), "FixedUpdateOnImpact")]
        [HarmonyPostfix]
        private static void FixedUpdateOnImpact(float ___accumulatedImpact)
        {
            instance.textVisuals2.text = "Lock Progress: " + ___accumulatedImpact + "/300";
        }

        private void setupTMP(ref GameObject gameObj, ref TextMeshProUGUI textContent, Vector3 coords)
        {
            //comments show values for old font setup
            gameObj = new GameObject("GrabberText");
            gameObj.transform.parent = GameObject.Find("Menu").transform;
            gameObj.AddComponent<CanvasRenderer>();

            textContent = gameObj.AddComponent<TextMeshProUGUI>();
            textContent.color = Color.white; //black
            textContent.fontSize = 50; //60
            textContent.font = font; //Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Single(font => font.name == "Blogger_Sans-Bold SDF"); //Menu SDF
            //textContent.fontMaterial = Resources.FindObjectsOfTypeAll<Material>().Single(material => material.name == "Blogger_Sans-Bold SDF Instruction"); //remove line
            textContent.enableWordWrapping = false;
            textContent.alignment = TextAlignmentOptions.BaselineLeft;

            gameObj.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            gameObj.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            gameObj.transform.localPosition = coords; //-858, -534, 0
            gameObj.transform.localRotation = Quaternion.identity;
            gameObj.transform.localScale = Vector3.one;
            gameObj.layer = LayerMask.NameToLayer("UI");
        }
    }
}