using UnityEngine;

namespace LongMethod
{
    public class CharacterController : MonoBehaviour
    {
        // Add serializable fields that allow better control and remove magic numbers from code
        [SerializeField] private float jumpForce = 10;
        [SerializeField] private float movementSpeed = 7;
        [SerializeField] private float rotationSpeed = 45;
        [SerializeField] private float maxVerticalRotation = 60;
        [SerializeField] private float minVerticalRotation = -30;

        private Rigidbody characterRigidbody;
        private Transform cameraTransform;

        private float horizontalRotation;
        private float verticalRotation;

        private void Update()
        {
            Move();
            TryJump();
            Rotate();
        }

        // Extract method
        private void Rotate()
        {
            horizontalRotation += rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, horizontalRotation, 0);

            verticalRotation -= rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            // Use clamp for better readability
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
            cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }

        // Extract method
        private void TryJump()
        {
            if (Input.GetButtonDown("Jump"))
                characterRigidbody.velocity = Vector3.up * jumpForce;
        }

        // Extract method
        private void Move()
        {
            // Improve local variables naming
            var forwardMovement = Input.GetAxis("Vertical") * transform.forward;
            var sidewaysMovement = Input.GetAxis("Horizontal") * transform.right;

            var movementDirection = (forwardMovement + sidewaysMovement).normalized;

            transform.position += movementDirection * Time.deltaTime * movementSpeed;
        }

        private void Awake()
        {
            // Execute this only once
            Cursor.lockState = CursorLockMode.Locked;

            // Cache static components
            characterRigidbody = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
        }

        // Remove unused Start method
    }
}