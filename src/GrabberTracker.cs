using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;

namespace ObjectGrabber
{
    using HarmonyLib;
    using UnityEngine;
    using TMPro;

    [BepInPlugin("top.zman350x.hff.objectgrabber", "Grab Count Tracker", "1.2.0")]
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

            setupTMP(ref textObj, ref textVisuals, new Vector3(106.4688f, 9f, 0f));
            textVisuals.text = "Grabs: " + grabs;
            textObj.SetActive(isEnabled);

            Harmony.CreateAndPatchAll(typeof(GrabberTracker), "GrabberTracker");

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
