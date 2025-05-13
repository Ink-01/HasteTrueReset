using Landfall.Modding;
using UnityEngine;
using HarmonyLib;

namespace TrueResetHaste;

[LandfallPlugin]
public class TrueResetHaste
{
    public static string guid { get; private set; } = "ink01.haste.truereset";

    private static Harmony harmony { get; set; }

    static TrueResetHaste()
    {
        harmony = new(guid);

        Debug.Log("True reset initializing! Why did you download this!");

        harmony.PatchAll(typeof(TrueResetPatch));

        Debug.Log("Patched the method that deletes your save file, amazing");
    }
}

[HarmonyPatch]
public static class TrueResetPatch
{

    public static void ByeSave()
    {
        // Bye!
        // This text doesn't show up, but the save gets cleared. I dont know why.
        Debug.Log("True Reset initiated. Have fun restarting...");
        SaveSystem.Clear();
    }

    // Just really making sure it's working

    [HarmonyPatch(typeof(OpeningCutscene), "Start")]
    [HarmonyPostfix]
    public static void TimeTravelStart(OpeningCutscene __instance)
    {
        Debug.Log("TimeTravelStart");
        if (__instance.isTimeTravel)
            ByeSave();
    }

    [HarmonyPatch(typeof(GM_Hub), "Start")]
    [HarmonyPostfix]
    public static void TimeTravelLoadFrom()
    {
        Debug.Log("TimeTravelLoadFrom");
        if (GM_Hub.loadingFromTimeTravelCutscene)
            ByeSave();
    }

    [HarmonyPatch(typeof(MainMenu), "Start")]
    [HarmonyPostfix]
    public static void TimeTravelThing(MainMenu __instance)
    {
        Debug.Log("TimeTravelThing");
        if (__instance.timeTravel)
            ByeSave();
    }
}
