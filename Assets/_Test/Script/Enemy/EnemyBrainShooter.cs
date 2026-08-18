using System.Collections;
using System.Collections.Generic;
using _Test.Script.Data;
using _Test.Script.General;
using UnityEngine;
using VContainer;

namespace _Test.Script.Enemy
{
    public class EnemyBrainShooter : MonoBehaviour
    {
        [SerializeField] private EnemyItem[] enemies;
        [SerializeField] private float minFireDelay = 1f;
        [SerializeField] private float maxFireDelay = 4f;
        [SerializeField] private AudioClip scoreAudioClip;
        private List<EnemyItem> availableShooters;
        private Coroutine shootingRoutine;
        private GameData gameData;
        private AudioManager audioManager;

        [Inject]
        public void Construct(GameData gameData, AudioManager audioManager)
        {
            this.gameData = gameData;
            this.audioManager = audioManager;
        }

        private void Awake()
        {
            availableShooters = new List<EnemyItem>(enemies);

            foreach (var enemy in availableShooters)
            {
                if (enemy != null)
                {
                    enemy.SetBrain(this);
                }
            }
        }

        private void Start()
        {
            shootingRoutine = StartCoroutine(RandomShootingLoop());
        }

        private IEnumerator RandomShootingLoop()
        {
            while (true)
            {
                var delay = Random.Range(minFireDelay, maxFireDelay);
                yield return new WaitForSeconds(delay);
                ShootRandomEnemy();
            }
        }

        private void ShootRandomEnemy()
        {
            if (availableShooters.Count == 0)
            {
                return;
            }

            var index = Random.Range(0, availableShooters.Count);
            var shooter = availableShooters[index];

            if (shooter != null)
                shooter.Shoot();
        }

        public void RemoveEnemy(EnemyItem enemy)
        {
            availableShooters.Remove(enemy);

            audioManager.PlaySfx(scoreAudioClip);

            var scoreToGive = enemy.ScoreToGive;
            gameData?.HandleScore(scoreToGive, availableShooters.Count > 0);
        }

        private void OnDestroy()
        {
            if (shootingRoutine != null)
            {
                StopCoroutine(shootingRoutine);
            }
        }
    }
}