using DG.Tweening;
using UnityEngine;

namespace _Test.Script.Enemy
{
    public class EnemyBrainMovement : MonoBehaviour
    {
        [SerializeField] private float minX = -5f;
        [SerializeField] private float maxX = 5f;
        [SerializeField] private float moveSpeed = 3f;
        private Tween moveTween;
        private Vector3 startPosition;

        private void Awake()
        {
            startPosition = transform.position;
        }

        private void Start()
        {
            StartPatrolling();
        }

        private void StartPatrolling()
        {
            transform.position = startPosition;

            var fullDistance = Mathf.Abs(maxX - minX);
            var fullDuration = fullDistance / moveSpeed;

            var firstLegDistance = Mathf.Abs(maxX - startPosition.x);
            var firstLegDuration = firstLegDistance / moveSpeed;

            moveTween = transform
                .DOMoveX(maxX, firstLegDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    moveTween = transform
                        .DOMoveX(minX, fullDuration)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1, LoopType.Yoyo);
                });
        }
    }
}