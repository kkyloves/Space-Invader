using UnityEngine;

namespace _Test.Script.General
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip bgmAudioClip;
        [Range(0f, 1f)] [SerializeField] private float bgmVolume = 0.6f;
        [Range(0f, 1f)] [SerializeField] private float sfxVolume = 1f;
        private AudioSource bgmSource;

        private void Awake()
        {
            if (bgmSource == null)
            {
                bgmSource = gameObject.AddComponent<AudioSource>();
            }

            bgmSource.loop = true;
            bgmSource.playOnAwake = false;
            bgmSource.volume = bgmVolume;

            PlayBgm(bgmAudioClip);
        }

        public void PlaySfx(AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null)
            {
                return;
            }

            var sfxObject = new GameObject($"SFX_{clip.name}");
            sfxObject.transform.SetParent(transform);

            var source = sfxObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = sfxVolume * volumeScale;
            source.loop = false;
            source.playOnAwake = false;
            source.Play();

            Destroy(sfxObject, clip.length);
        }

        public void PlayBgm(AudioClip clip, bool loop = true)
        {
            if (clip == null)
            {
                return;
            }

            if (bgmSource.clip == clip && bgmSource.isPlaying)
            {
                return;
            }

            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
    }
}