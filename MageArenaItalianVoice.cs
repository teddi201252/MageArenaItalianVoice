using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MageArenaItalianVoice.Config;

namespace MageArenaItalianVoice;

[BepInPlugin("com.teddi201252.magearenaitalianvoice", "MageArenaItalianVoice", "1.0.0")]
public class MageArenaItalianVoice : BaseUnityPlugin
{
    private readonly Harmony harmony = new Harmony("com.teddi201252.magearenaitalianvoice");
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        VoiceCommandConfig.Init(Config);
        harmony.PatchAll();
        Logger.LogInfo("MageArenaItalianVoice loaded!");
    }
    
    

}
