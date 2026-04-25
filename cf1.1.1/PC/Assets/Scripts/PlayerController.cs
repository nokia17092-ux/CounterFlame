using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 90f;

    [Header("Shooting Settings")]
    public float fireRate = 0.1f; // Time between shots
    public GameObject bulletPrefab;
    public Transform firePoint;

    private CharacterController controller;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private float nextFireTime = 0f;
    private bool isCrouching = false;
    private float originalHeight;
    private Vector3 originalCenter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        if (controller != null)
        {
            originalHeight = controller.height;
            originalCenter = controller.center;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleShooting();
        HandleActions();
    }

    void HandleMovement()
    {
        float moveSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y = controller.velocity.y + Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // Weapon switching (1-5)
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SwitchWeapon(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowScoreboard();
        }
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        if (isCrouching)
        {
            controller.height = originalHeight * 0.5f;
            controller.center = originalCenter * 0.5f;
        }
        else
        {
            controller.height = originalHeight;
            controller.center = originalCenter;
        }
    }

    void Shoot()
    {
        // Instantiate bullet or handle shooting logic
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        // Add shooting sound, animation, etc.
    }

    void Reload()
    {
        // Handle reload logic
        Debug.Log("Reloading");
    }

    void SwitchWeapon(int weaponIndex)
    {
        // Handle weapon switching
        Debug.Log("Switching to weapon " + weaponIndex);
    }

    void ShowScoreboard()
    {
        // Handle scoreboard display
        Debug.Log("Showing scoreboard");
    }
}