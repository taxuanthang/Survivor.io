using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;

        public AudioMixer mixer;

        [SerializeField] SimpleAudioEvent _playerHit_Sound;

        [SerializeField] SimpleAudioEvent _enemyHit_Sound;

        [SerializeField] SimpleAudioEvent _enemyDie_Sound;

        [SerializeField] SimpleAudioEvent _playerDie_Sound;

        [SerializeField] SimpleAudioEvent _background_Music;

        private void Awake()
        {
            if(musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            this.enabled = false;
        }

        public void SetUp()
        {
            this.enabled = true;
        }

        [Button]
        public void TestSFX()
        {
            ToggleMuteSFX(true);
        }

        [Button]
        public void TestMusic()
        {
            ToggleMuteMusic(true);
        }

        public void ToggleMuteMusic(bool isMuted)
        {
            if (isMuted)
            {
                mixer.SetFloat("MusicVol", -80f); // Tắt sạch
            }
            else
            {
                mixer.SetFloat("MusicVol", 0f);   // Bật về mức tối đa
            }
        }

        public void ToggleMuteSFX(bool isMuted)
        {
            if (isMuted)
            {
                mixer.SetFloat("SFXVol", -80f); // Tắt sạch
            }
            else
            {
                mixer.SetFloat("SFXVol", 0f);   // Bật về mức tối đa
            }
        }

        [Button]
        public void PlayEnemyHit()
        {
            _enemyHit_Sound.Play(sfxSource);
        }

        [Button]
        public void PlayEnemyDie()
        {
            _enemyDie_Sound.Play(sfxSource);
        }

        [Button]
        public void PlayPlayerHit(float dmg)
        {
            _playerHit_Sound.Play(sfxSource);
        }

        [Button]
        public void PlayPlayerDie()
        {
            _playerDie_Sound.Play(sfxSource);
        }

        //[Button]
        //public async Task PlayPlayerHit()
        //{
        //    _win_Sound.Play(sfxSource);
        //    await Task.Delay((int)_win_Sound.clips[0].length * 1000);
        //}

        [Button]
        public void PlayBackgroundSound()
        {
            _background_Music.Play(musicSource);
        }

        public void OnEnable()
        {
            EventManager.instance.OnEnemyDie.AddListener(PlayEnemyDie);
            EventManager.instance.OnEnemyHit.AddListener(PlayEnemyHit);
            EventManager.instance.OnPlayerHit.AddListener(PlayPlayerHit);
            EventManager.instance.OnPlayerDie.AddListener(PlayPlayerDie);
        }

        public void OnDisable()
        {
            if(EventManager.instance == null) return;
            EventManager.instance.OnEnemyDie.RemoveListener(PlayEnemyDie);
            EventManager.instance.OnEnemyHit.RemoveListener(PlayEnemyHit);
            EventManager.instance.OnPlayerHit.RemoveListener(PlayPlayerHit);
            EventManager.instance.OnPlayerDie.RemoveListener(PlayPlayerDie);
        }
    }
}
