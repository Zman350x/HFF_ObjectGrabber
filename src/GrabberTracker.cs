using System.Linq;

namespace ObjectGrabber
{
    using BepInEx;
    using BepInEx.Configuration;
    using HarmonyLib;
    using UnityEngine;
    using TMPro;

    [BepInPlugin("top.zman350x.hff.objectgrabber", "Grab Count Tracker", "1.3.1")]
    [BepInProcess("Human.exe")]
    public sealed class GrabberTracker : BaseUnityPlugin
    {
        public static GrabberTracker instance;
        public bool isEnabled;
        
        private ConfigEntry<bool> defaultEnabled;

        private uint grabs;

        private GameObject textObj;
        private TextMeshProUGUI textVisuals;

        private void Awake()
        {
            instance = this;

            defaultEnabled = Config.Bind("General.Toggles",
                                         "DefaultEnabled",
                                         false,
                                         "Whether or not the \"Grab: X\" text is visible by default when you launch the game.");

            isEnabled = defaultEnabled.Value;

            Harmony.CreateAndPatchAll(typeof(GrabberTracker), "GrabberTracker");
        }

        private void OnDestroy() 
        {
            Destroy(textObj);
            Harmony.UnpatchID("GrabberTracker");
        }

        private void Start()
        {
            SetupTMP(ref textObj, ref textVisuals, new Vector3(106.4688f, 9f, 0f));
            textObj.SetActive(isEnabled);
            grabs = 0;
            UpdateText();

            Shell.RegisterCommand("grab_reset", (string x) => {
                ResetGrabCounter();
            }, "Reset grab counter to 0");

            Shell.RegisterCommand("grab_toggle", (string x) => {
                SetGrabCounterState(!isEnabled);
            }, "Enable/disable grab counter");
        }

        public void ResetGrabCounter(bool print = true)
        {
            grabs = 0;
            UpdateText();
            if (print)
                Shell.Print("Grab counter reset");
        }

        public void SetGrabCounterState(bool state, bool print = true)
        {
            isEnabled = state;
            textObj.SetActive(isEnabled);
            ResetGrabCounter(false);
            if (print)
                Shell.Print(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
        }

        private void UpdateText()
        {
            textVisuals.text = "Grabs: " + grabs;
        }

        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            ++instance.grabs;
            instance.UpdateText();
        }

        private static void SetupTMP(ref GameObject gameObj, ref TextMeshProUGUI textContent, Vector3 coords)
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
