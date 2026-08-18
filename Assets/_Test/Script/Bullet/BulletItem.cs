using _Test.Script.Enemy;
using _Test.Script.Player;
using DG.Tweening;
using UnityEngine;

namespace _Test.Script.Bullet
{
    public class BulletItem : MonoBehaviour
    {
        [SerializeField] private Vector3 direction = Vector3.forward;
        [SerializeField] private float speed = 15f;
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private int damage = 10;
        [SerializeField] private LayerMask hitLayers;

        private Vector3 dirNormalized;
        private Tween moveTween;

        private void Awake()
        {
            dirNormalized = direction.normalized;
        }

        private void Start()
        {
            var targetPos = transform.position + dirNormalized * speed * lifeTime;

            moveTween = transform
                .DOMove(targetPos, lifeTime)
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            moveTween?.Kill();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInHitLayers(other.gameObject.layer))
            {
                return;
            }

            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.HandleDamage();
                Destroy(gameObject);
            }
            else if (other.TryGetComponent(out EnemyItem enemy))
            {
                enemy.HandleDestroy();
                Destroy(gameObject);
            }
        }

        private bool IsInHitLayers(int layer)
        {
            return (hitLayers.value & (1 << layer)) != 0;
        }
    }
}