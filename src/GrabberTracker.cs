namespace ObjectGrabber
{
    using BepInEx;
    using BepInEx.Configuration;
    using HarmonyLib;
    using UnityEngine;
    using ZmanBase;

    [BepInPlugin("top.zman350x.hff.objectgrabber", "Grab Count Tracker", "2.0.0")]
    [BepInDependency("top.zman350x.hff.zmanbase")]
    [BepInProcess("Human.exe")]
    public sealed class GrabberTracker : BaseUnityPlugin
    {
        public static GrabberTracker instance;
        public bool isEnabled;

        private ConfigEntry<bool> defaultEnabled;

        private uint grabs;

        private UIManager.CornerText grabText;

        private void Awake()
        {
            instance = this;

            defaultEnabled = Config.Bind("General.Toggles",
                                         "DefaultEnabled",
                                         false,
                                         "Whether or not the \"Grab: X\" text is visible by default when you launch the game.");

            isEnabled = defaultEnabled.Value;
        }

        private void OnDestroy()
        {
            SetGrabCounterState(false);
        }

        private void Start()
        {
            SetGrabCounterState(isEnabled);

            Shell.RegisterCommand("grab_reset", () => {
                ResetGrabCounter();
            }, "USAGE: grab_reset\r\n\r\nResets the grab counter to 0.");

            Shell.RegisterCommand("grab_toggle", () => {
                SetGrabCounterState(!isEnabled);
            }, "USAGE: grab_toggle\r\n\r\nToggles the state of the grab counter.");

            Shell.RegisterCommand("grab_enable", () => {
                SetGrabCounterState(true);
            }, "USAGE: grab_enable\r\n\r\nEnables the grab counter.");

            Shell.RegisterCommand("grab_disable", () => {
                SetGrabCounterState(false);
            }, "USAGE: grab_disable\r\n\r\nDisables the grab counter.");
        }

        public void ResetGrabCounter(bool print = true)
        {
            grabs = 0;
            UpdateText();
            if (print)
                Debug.Log("Grab counter reset");
        }

        public void SetGrabCounterState(bool state, bool print = true)
        {
            if ((!state) && (!(grabText is null)))
            {
                grabText.Delete();
                grabText = null;
            }
            else if (state && (grabText is null))
            {
                grabText = new UIManager.CornerText("grabText");
            }

            if (state && !isEnabled)
                Harmony.CreateAndPatchAll(typeof(GrabberTracker), "GrabberTracker");
            else if (!state && isEnabled)
                Harmony.UnpatchID("GrabberTracker");

            isEnabled = state;
            ResetGrabCounter(false);
            if (print)
                Debug.Log(isEnabled ? "Grab counter enabled" : "Grab counter disabled");
        }

        private void UpdateText()
        {
            if (grabText is null)
                return;

            grabText.SetText("Grabs: " + grabs);
        }

        [HarmonyPatch(typeof(GrabManager), "ObjectGrabbed")]
        [HarmonyPrefix]
        private static void ObjectGrabbed(GameObject grabObject)
        {
            ++instance.grabs;
            instance.UpdateText();
        }
    }
}
