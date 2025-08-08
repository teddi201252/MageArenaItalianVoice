using System;
using System.Collections.Generic;
using System.ComponentModel;
using HarmonyLib;
using Recognissimo.Components;
using UnityEngine;


namespace MageArenaRussianVoice.Patches
{
    [HarmonyPatch(typeof(SetUpModelProvider), "Setup")]
    public static class SetUpModelProviderPatch
    {
        private static readonly String nameOfModel = "vosk-model-small-ru-0.22";
        [HarmonyPrefix]
        public static bool Prefix(SetUpModelProvider __instance)
        {
            StreamingAssetsLanguageModelProvider streamingAssetsLanguageModelProvider = __instance.gameObject.AddComponent<StreamingAssetsLanguageModelProvider>();
            streamingAssetsLanguageModelProvider.language = SystemLanguage.Russian;
            streamingAssetsLanguageModelProvider.languageModels = new List<StreamingAssetsLanguageModel>
            {
                new StreamingAssetsLanguageModel
                {
                    language = SystemLanguage.Russian,
                    path = $"LanguageModels/{nameOfModel}"
                }
            };
            SpeechRecognizer speechRecognizer = __instance.GetComponent<SpeechRecognizer>();
            speechRecognizer.LanguageModelProvider = streamingAssetsLanguageModelProvider;
            return false;
        }
    }
}
