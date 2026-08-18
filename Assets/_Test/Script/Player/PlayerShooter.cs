using _Test.Script.General;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace _Test.Script.Player
{
    public class PlayerShooter : MonoBehaviour
    {

        [SerializeField] private InputActionReference fireActionReference;
        [SerializeField] private AudioClip shootAudioClip;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.2f;

        private InputAction fireAction;
        private float lastFireTime = -999f;

        private AudioManager audioManager;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        private void Awake()
        {
            fireAction = fireActionReference.action;
        }

        private void OnEnable()
        {
            fireAction.Enable();
            fireAction.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            fireAction.performed -= OnFirePerformed;
            fireAction.Disable();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            if (Time.time - lastFireTime < fireRate)
            {
                return;
            }

            lastFireTime = Time.time;
            Shoot();
        }

        private void Shoot()
        {
            if (bulletPrefab == null || firePoint == null)
            {
                return;
            }

            audioManager.PlaySfx(shootAudioClip);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}