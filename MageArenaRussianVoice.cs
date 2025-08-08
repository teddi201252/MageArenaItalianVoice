using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MageArenaRussianVoice;

[BepInPlugin("com.infernumvii.magearenarussianvoice", "MageArenaRussianVoice", "1.1.2")]
public class MageArenaRussianVoice : BaseUnityPlugin
{
    private readonly Harmony harmony = new Harmony("com.infernumvii.magearenarussianvoice");
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        harmony.PatchAll();
        Logger.LogInfo("MageArenaRussianVoice loaded!");
    }
    
    

}
