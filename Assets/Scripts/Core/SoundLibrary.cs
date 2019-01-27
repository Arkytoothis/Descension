using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Descension
{
    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip[] group;
    }

    [System.Serializable]
    public class SoundLibrary : MonoBehaviour
    {
        [SerializeField] SoundGroup[] musicGroups = null;
        [SerializeField] SoundGroup[] ambientGroups = null;
        [SerializeField] SoundGroup[] effectGroups = null;
        [SerializeField] SoundGroup[] uiGroups = null;
        [SerializeField] SoundGroup[] voiceGroups = null;

        Dictionary<string, AudioClip[]> music = new Dictionary<string, AudioClip[]>();
        Dictionary<string, AudioClip[]> ambients = new Dictionary<string, AudioClip[]>();
        Dictionary<string, AudioClip[]> effects = new Dictionary<string, AudioClip[]>();
        Dictionary<string, AudioClip[]> ui = new Dictionary<string, AudioClip[]>();
        Dictionary<string, AudioClip[]> voices = new Dictionary<string, AudioClip[]>();

        void Awake()
        {
            foreach (SoundGroup musicGroup in musicGroups)
                music.Add(musicGroup.groupID, musicGroup.group);

            foreach (SoundGroup ambientGroup in ambientGroups)
                ambients.Add(ambientGroup.groupID, ambientGroup.group);

            foreach (SoundGroup effectGroup in effectGroups)
                effects.Add(effectGroup.groupID, effectGroup.group);

            foreach (SoundGroup uiGroup in uiGroups)
                ui.Add(uiGroup.groupID, uiGroup.group);

            foreach (SoundGroup voiceGroup in voiceGroups)
                voices.Add(voiceGroup.groupID, voiceGroup.group);
        }

        public AudioClip GetMusicFromName(string name)
        {
            if (name != "Random")
            {
                if (music.ContainsKey(name))
                {
                    AudioClip[] sounds = music[name];
                    return sounds[Random.Range(0, sounds.Length)];
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public AudioClip GetAmbientFromName(string name)
        {
            if (ambients.ContainsKey(name))
            {
                AudioClip[] sounds = ambients[name];
                return sounds[Random.Range(0, sounds.Length)];
            }
            return null;
        }

        public AudioClip GetEffectFromName(string name)
        {
            if (effects.ContainsKey(name))
            {
                AudioClip[] sounds = effects[name];
                return sounds[Random.Range(0, sounds.Length)];
            }
            return null;
        }

        public AudioClip GetUIFromName(string name)
        {
            if (ui.ContainsKey(name))
            {
                AudioClip[] sounds = ui[name];
                return sounds[Random.Range(0, sounds.Length)];
            }
            return null;
        }

        public AudioClip GetVoiceFromName(string name)
        {
            if (voices.ContainsKey(name))
            {
                AudioClip[] sounds = voices[name];
                return sounds[Random.Range(0, sounds.Length)];
            }
            return null;
        }
    }
}