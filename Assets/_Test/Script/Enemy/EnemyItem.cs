using UnityEngine;

namespace _Test.Script.Enemy
{
    public class EnemyItem : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private int scoreToGive = 30;
        [SerializeField] private GameObject explosionEffectPrefab;

        public int ScoreToGive => scoreToGive;
        private EnemyBrainShooter brain;

        public void SetBrain(EnemyBrainShooter enemyBrain)
        {
            brain = enemyBrain;
        }

        public void Shoot()
        {
            if (bulletPrefab == null || firePoint == null)
            {
                return;
            }

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        public void HandleDestroy()
        {
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }

            brain?.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}