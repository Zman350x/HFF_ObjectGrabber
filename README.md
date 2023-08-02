# Grab Count Tracker
## About
<!-- ####################################################################### -->
Grab Count Tracker is a small mod for Human: Fall Flat that counts the number of
times the player grabs something. This value is displayed in the lower-left
corner of the screen. The mod is intended to be used in the "minimum grabs"
challenge run of the game but can be used for any purpose where keeping track of
player grabs may be beneficial.

![Grab counter in lower-left corner](/counter_example_image.png)

This mod uses TextMeshPro for the counter text, so it should scale appropriately
regardless of resolution. This also has the benefit of allowing for the use of a
Human: Fall Flat font and style.
## Installation
1)	Download the current 32-bit (x86) version of BepInEx 5 from
https://github.com/BepInEx/BepInEx/releases (*do not download BepInEx 6*)
    - i.e. "BepInEx_x86_5.#.#.#.zip"
3)	Extract *all* the contents of the .zip into your "Human: Fall Flat" Steam
directory
    - i.e. "C:\Program Files (x86)\Steam\steamapps\common\Human Fall Flat"
5)	Launch Human: Fall Flat (so that BepInEx can generate folders/files)
6)	Close Human: Fall Flat when you reach the menu
4)	Place the mod `.dll` file in `BepInEx\plugins` inside the Human: Fall Flat
folder
## How to use
The in-game shell can be accessed by pressing the `~` or `F1` keys.

Running the `grab_toggle` command will enable/disable the mod. (default disable)

Running the `grab_reset` command will reset the counted grabs to 0.