using UnityEngine;

namespace LongMethod
{
    public class CharacterController : MonoBehaviour
    {
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

        private void Rotate()
        {
            horizontalRotation += rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, horizontalRotation, 0);

            verticalRotation -= rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
            cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }

        private void TryJump()
        {
            if (Input.GetButtonDown("Jump"))
                characterRigidbody.velocity = Vector3.up * jumpForce;
        }

        private void Move()
        {
            var forwardMovement = Input.GetAxis("Vertical") * transform.forward;
            var sidewaysMovement = Input.GetAxis("Horizontal") * transform.right;

            var movementDirection = (forwardMovement + sidewaysMovement).normalized;

            transform.position += movementDirection * Time.deltaTime * movementSpeed;
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            characterRigidbody = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
        }
    }
}