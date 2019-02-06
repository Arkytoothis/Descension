using UnityEngine;
using System.Collections;
using Descension.Core;

namespace Descension
{
    public class AudioManager : Singleton<AudioManager>
    {
        public enum AudioChannel { Master, Sfx, Music };

        float masterVolumePercent = .5f;
        float effectVolumePercent = .5f;
        float ambientVolumePercent = .5f;
        float uiVolumePercent = .5f;
        float voiceVolumePercent = .5f;
        float musicVolumePercent = .25f;

        AudioSource ambientSource;
        AudioSource uiSource;
        AudioSource effectSource;
        AudioSource voiceSource;
        AudioSource[] musicSources;
        int activeMusicSourceIndex;

        Transform audioListener;

        SoundLibrary library;

        void Awake()
        {
            Reload();

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                musicSources[i].loop = true;
                newMusicSource.transform.parent = transform;
            }

            GameObject newAmbientsource = new GameObject("Ambient source");
            ambientSource = newAmbientsource.AddComponent<AudioSource>();
            ambientSource.loop = true;
            newAmbientsource.transform.parent = transform;

            GameObject newUIsource = new GameObject("UI source");
            uiSource = newUIsource.AddComponent<AudioSource>();
            newUIsource.transform.parent = transform;

            GameObject newEffectsource = new GameObject("Effect source");
            effectSource = newEffectsource.AddComponent<AudioSource>();
            newEffectsource.transform.parent = transform;

            GameObject newVoicesource = new GameObject("Voice source");
            voiceSource = newVoicesource.AddComponent<AudioSource>();
            newVoicesource.transform.parent = transform;
        }

        private void Start()
        {
            audioListener = FindObjectOfType<AudioListener>().transform;
        }

        public void SetVolume(float volumePercent, AudioChannel channel)
        {
            switch (channel)
            {
                case AudioChannel.Master:
                    masterVolumePercent = volumePercent;
                    break;
                case AudioChannel.Sfx:
                    effectVolumePercent = volumePercent;
                    break;
                case AudioChannel.Music:
                    musicVolumePercent = volumePercent;
                    break;
            }

            musicSources[0].volume = musicVolumePercent * masterVolumePercent;
            musicSources[1].volume = musicVolumePercent * masterVolumePercent;
        }

        public void PlayMusic(string musicName, float fadeDuration = 1)
        {
            activeMusicSourceIndex = 1 - activeMusicSourceIndex;
            musicSources[activeMusicSourceIndex].clip = library.GetMusicFromName(musicName);
            musicSources[activeMusicSourceIndex].Play();

            StartCoroutine(AnimateMusicCrossfade(fadeDuration));
        }

        public void PlayMusic(string musicName, int index, float fadeDuration = 1)
        {
            activeMusicSourceIndex = index;
            musicSources[activeMusicSourceIndex].clip = library.GetMusicFromName(musicName);
            musicSources[activeMusicSourceIndex].Play();

            StartCoroutine(AnimateMusicCrossfade(fadeDuration));
        }

        public void PlayEffect(AudioClip clip, Vector3 pos)
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, pos, effectVolumePercent * masterVolumePercent);
            }
        }

        public void PlayEffect(string soundName, Vector3 pos)
        {
            PlayEffect(library.GetEffectFromName(soundName), pos);
        }

        public void PlayAmbient(string ambientName, bool loop)
        {
            ambientSource.clip = library.GetAmbientFromName(ambientName);
            ambientSource.volume = ambientVolumePercent * masterVolumePercent;
            ambientSource.loop = loop;
            ambientSource.Play();
        }

        public void PlayUI(AudioClip clip)
        {
            uiSource.PlayOneShot(clip, uiVolumePercent * masterVolumePercent);
        }

        public void PlayUI(string soundName)
        {
            uiSource.PlayOneShot(library.GetUIFromName(soundName), uiVolumePercent * masterVolumePercent);
        }

        public void PlayVoice(string soundName)
        {
            uiSource.PlayOneShot(library.GetVoiceFromName(soundName), voiceVolumePercent * masterVolumePercent);
        }

        IEnumerator AnimateMusicCrossfade(float duration)
        {
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / duration;
                musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
                musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
                yield return null;
            }
        }
    }
}