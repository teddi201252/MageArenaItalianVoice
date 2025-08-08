using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Dissonance;
using HarmonyLib;
using Recognissimo.Components;
using UnityEngine;


namespace MageArenaRussianVoice.Patches
{
    [HarmonyPatch(typeof(VoiceControlListener))]
    public static class VoiceControlListenerPatch
    {
        private static readonly Dictionary<string[], Action<VoiceControlListener>> russianCommandMap = new Dictionary<string[], Action<VoiceControlListener>>()
        {
            { new[] {"огненный", "шар"}, v => v.CastFireball() },
            { new[] {"сосулька"}, v => v.CastFrostBolt() },
            { new[] {"вход"}, v => v.CastWorm() },
            { new[] {"выход"}, v => v.CastHole() },
            { new[] {"магический", "снаряд"}, v => v.CastMagicMissle() },
            { new[] {"зеркало"}, v => v.ActivateMirror() }
        };
        private static readonly Dictionary<string, string[]> russianAdditionalCommandMap = new Dictionary<string, string[]>
        {
            {"rock", new[] {"валун"}},
            {"wisp", new[] {"дух"}},
            {"blast", new[] {"чёрный", "луч"}},
            {"divine", new[] {"благословление"}},
            {"blink", new[] {"прыжок"}},
            {"thunderbolt", new[] {"гром"}},
        };
        private static readonly AccessTools.FieldRef<VoiceControlListener, SpeechRecognizer> srRef =
            AccessTools.FieldRefAccess<VoiceControlListener, SpeechRecognizer>("sr");

        private static readonly AccessTools.FieldRef<VoiceControlListener, VoiceBroadcastTrigger> vbtRef =
            AccessTools.FieldRefAccess<VoiceControlListener, VoiceBroadcastTrigger>("vbt");

        private static readonly MethodInfo restartsrMethod =
            AccessTools.Method(typeof(VoiceControlListener), "restartsr");

        [HarmonyPatch("waitgetplayer")]
        [HarmonyPrefix]
        private static bool WaitGetPlayerPrefix(VoiceControlListener __instance, ref IEnumerator __result)
        {
            __result = ModifiedWaitGetPlayer(__instance);
            return false;
        }

        private static IEnumerator ModifiedWaitGetPlayer(VoiceControlListener instance)
        {
            while (instance.pi == null)
            {
                if (Camera.main.transform.parent != null &&
                    Camera.main.transform.parent.TryGetComponent<PlayerInventory>(out var playerInventory))
                {
                    instance.pi = playerInventory;
                }
                yield return null;
            }

            instance.GetComponent<SetUpModelProvider>().Setup();
            yield return null;

            srRef(instance) = instance.GetComponent<SpeechRecognizer>();
            instance.SpellPages = new List<ISpellCommand>();
            MonoBehaviour[] components = instance.gameObject.GetComponents<MonoBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                ISpellCommand spellCommand = components[i] as ISpellCommand;
                if (spellCommand != null)
                {
                    instance.SpellPages.Add(spellCommand);
                }
            }
            // foreach (ISpellCommand spellCommand2 in instance.SpellPages)
            // {
            //     spellCommand2.ResetVoiceDetect();
            // }
            var recognizer = srRef(instance);
            //Main spells add to vocabulary
            foreach (var pair in russianCommandMap)
            {
                foreach (var word in pair.Key)
                {
                    recognizer.Vocabulary.Add(word);
                }
            }
            foreach (var pair in russianAdditionalCommandMap)
            {
                foreach (string word in pair.Value)
                {
                    recognizer.Vocabulary.Add(word);
                }
            }
            recognizer.ResultReady.AddListener(res => instance.tryresult(res.text));
            yield return new WaitForSeconds(1f);
            instance.GetComponent<SpeechRecognizer>().StartProcessing();
            while (instance.isActiveAndEnabled)
            {
                yield return new WaitForSeconds(30f);
                var vbt = vbtRef(instance);
                if (!vbt.IsTransmitting)
                {
                    recognizer.StopProcessing();
                    instance.StartCoroutine((IEnumerator)restartsrMethod.Invoke(instance, null));
                }
            }
            yield break;
        }

        [HarmonyPatch("tryresult")]
        [HarmonyPrefix]
        private static bool TryResultPrefix(VoiceControlListener __instance, string res)
        {
            if (res != null)
            {
                foreach (var command in russianCommandMap) {
                    if (command.Key.Any(keyword => res.Contains(keyword)))
                    {
                        command.Value(__instance);
                    }
                }
                foreach (var pair in russianAdditionalCommandMap)
                {
                    if (pair.Value.Any(keyword => res.Contains(keyword)))
                    {
                        var spell = __instance.SpellPages.FirstOrDefault(s => 
                            s != null && s.GetSpellName() == pair.Key);
                        
                        spell?.TryCastSpell();
                        //return;
                    }
                }
                srRef(__instance).StopProcessing();
		        __instance.StartCoroutine((IEnumerator)restartsrMethod.Invoke(__instance, null));
            }
            return false;
        }
    }
}