using UnityEngine;
using UnityEngine.InputSystem;

namespace _Test.Script.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveActionReference;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private bool clampPosition = false;
        [SerializeField] private float minX = -5f;
        [SerializeField] private float maxX = 5f;

        private Rigidbody playerRigidbody;
        private float moveInput;
        private InputAction moveAction;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerRigidbody.isKinematic = true;

            moveAction = moveActionReference.action;
        }

        private void OnEnable()
        {
            moveAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        private void Update()
        {
            moveInput = moveAction.ReadValue<float>();

            var pos = transform.position;
            pos.x += moveInput * moveSpeed * Time.deltaTime;

            if (clampPosition)
            {
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
            }

            transform.position = pos;
        }
    }
}