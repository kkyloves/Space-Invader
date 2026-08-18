using _Test.Script.Data;
using _Test.Script.General;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Test.Script.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private AudioClip hitAudioClip;
        [SerializeField] private AudioClip explodeAudioClip;

        [Header("Death Animation")]
        [SerializeField] private float shakeDuration = 0.25f;
        [SerializeField] private float shakeStrength = 0.4f;
        [SerializeField] private float punchDuration = 0.2f;
        [SerializeField] private float sinkDistance = 2f;
        [SerializeField] private float sinkDuration = 0.35f;

        private GameData gameData;
        private AudioManager audioManager;

        [Inject]
        public void Construct(GameData gameData, AudioManager audioManager)
        {
            this.gameData = gameData;
            this.audioManager = audioManager;
        }

        public void HandleDamage()
        {
            gameData.HandleDamage();

            var livesRemaining = gameData.PlayerLives;

            if (livesRemaining <= 0)
            {
                audioManager.PlaySfx(explodeAudioClip);
                PlayDeathAnimation();
            }
            else
            {
                audioManager.PlaySfx(hitAudioClip);
            }
        }

        private void PlayDeathAnimation()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOShakePosition(
                shakeDuration,
                strength: new Vector3(shakeStrength, shakeStrength * 0.5f, shakeStrength),
                vibrato: 30,
                randomness: 90,
                fadeOut: true));

            sequence.Append(transform.DOPunchPosition(
                Vector3.up * 0.6f,
                punchDuration,
                vibrato: 10,
                elasticity: 0.4f));

            sequence.Append(transform.DOMoveY(
                transform.position.y - sinkDistance,
                sinkDuration).SetEase(Ease.InQuad));

            sequence.OnComplete(() => gameObject.SetActive(false));
        }
    }
}