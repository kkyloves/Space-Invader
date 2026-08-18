using UnityEngine;

namespace _Test.Script.Enemy
{
    public class ExplosionEffect : MonoBehaviour
    {
        private ParticleSystem ps;

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            var main = ps.main;
            var totalDuration = main.duration + main.startLifetime.constantMax;

            Destroy(gameObject, totalDuration);
        }
    }
}