using UnityEngine;

public class ModularPlayerController : PlayerControllerBase
{
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 3.0f;

    public float jumpHeight = 1.5f;

    public Transform playerCamera;
    [Tooltip("Sensitivity for mouse movement, affecting how fast the player can look around")]
    public float mouseSensitivity = 100f;

    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;

    private void OnEnable()
    {
        InputManager.Instance.OnMove += Move;
        InputManager.Instance.OnLook += Look;
        InputManager.Instance.OnJump += Jump;
        InputManager.Instance.OnSprint += Sprint;
        InputManager.Instance.OnCrouch += Crouch;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMove -= Move;
        InputManager.Instance.OnLook -= Look;
        InputManager.Instance.OnJump -= Jump;
        InputManager.Instance.OnSprint -= Sprint;
        InputManager.Instance.OnCrouch -= Crouch;
    }

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    public override void Move(Vector2 input)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    public override void Look(Vector2 input)
    {
        float mouseX = input.x * mouseSensitivity * Time.deltaTime;
        float mouseY = input.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    public override void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    public override void Sprint(bool isSprinting)
    {
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
    }

    public override void Crouch()
    {
        isCrouching = !isCrouching;

        controller.height = isCrouching ? crouchHeight : standingHeight;
        currentSpeed = isCrouching ? crouchSpeed : walkSpeed;
    }




}
