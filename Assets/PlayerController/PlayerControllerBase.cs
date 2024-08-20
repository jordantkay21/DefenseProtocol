using UnityEngine;

public abstract class PlayerControllerBase : MonoBehaviour, IMovable, ICameraControllable, IJumper, ISprinter, ICroucher
{
    protected CharacterController controller;
    [Tooltip("Stores the character's velocity which is used for gravity and other forces")]
    protected Vector3 velocity;
    protected bool isGrounded;
    [Tooltip("Tracks the player's rotation around the x-axis, used for camera control")]
    protected float xRotation;
    protected bool isCrouching;
    [Tooltip("Stores the player's current speed, which could be adjusted based on sprint/crouch state")]
    protected float currentSpeed;


    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        ApplyGravity();
    }

    protected void ApplyGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0) //Resets the downward velocity if the character is grounded to prevent continous falling
            velocity.y = -2;

        velocity.y += Physics.gravity.y * Time.deltaTime; //Applies gravity overtime
        controller.Move(velocity * Time.deltaTime); //Moves the character according to the calculated velocity
    }


    public abstract void Crouch();
    public abstract void Jump();
    public abstract void Look(Vector2 input);
    public abstract void Move(Vector2 input);
    public abstract void Sprint(bool isSprinting);
}
