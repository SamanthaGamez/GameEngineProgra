using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMove))]
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    private CharacterController characterController;

    public float baseMoveSpeed;
    public float currentSpeed;
    public float cameraSensitivity;
    private bool isRunning;

    public float gravityValue = -9.81f;
    private float verticalVelocity = 0f;
    public float groundRayRange;
    public float jumpStrength;
    public LayerMask groundMask;

    [SerializeField] private Transform playerCamera;

    private float rotationY;
    private float rotationX;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandlePlayerMovement();
        HandlePlayerControl();
        ApplyGravity();

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        verticalVelocity = jumpStrength;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundRayRange, groundMask);
    }
    private void HandlePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;
        characterController.Move(direction * currentSpeed * Time.deltaTime);
    }

    private void HandlePlayerControl()
    {
        UpdateCameraMovement();
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravityValue * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void UpdateCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX - mouseY, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);

        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private void OnDrawGizmos()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (characterController == null) return;

        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRayRange);
    }
}