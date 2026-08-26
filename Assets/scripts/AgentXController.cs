using UnityEngine;

public class AgentXController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 2.2f;
    public float sprintSpeed = 3.2f;
    public float gravity = -15f;

    [Header("Cámara")]
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float maxLookUp = 75f;
    public float maxLookDown = -60f;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction =
            transform.right * x +
            transform.forward * z;

        float currentSpeed =
            Input.GetKey(KeyCode.LeftShift)
            ? sprintSpeed
            : moveSpeed;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        direction *= currentSpeed;
        direction.y = verticalVelocity;

        controller.Move(direction * Time.deltaTime);

        float animationSpeed = 0f;

        if (x != 0 || z != 0)
        {
            animationSpeed =
                Input.GetKey(KeyCode.LeftShift)
                ? 1f
                : 0.5f;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", animationSpeed);
        }
    }

    void Look()
    {
        if (cameraHolder == null)
            return;

        float mouseX =
            Input.GetAxis("Mouse X") * mouseSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;

        cameraPitch =
            Mathf.Clamp(
                cameraPitch,
                maxLookDown,
                maxLookUp
            );

        cameraHolder.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}