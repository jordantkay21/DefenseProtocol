using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerInputActions InputActions { get; private set; }

    public event Action<Vector2> OnMove; //Event triggered when movement input is recieved
    public event Action<Vector2> OnLook; //Event triggered when look input [mouse movement] is recieved
    public event Action OnJump; //Triggered when jump input is recieved
    public event Action<bool> OnSprint; //Triggered when the sprint input is pressed or released
    public event Action OnCrouch; //Triggered when the crouch input is recieved

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Ensures that this GameObject persists across scene loads
            InputActions = new PlayerInputActions();

            InputActions.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
            InputActions.Player.Look.performed += ctx => OnLook?.Invoke(ctx.ReadValue<Vector2>());
            InputActions.Player.Jump.performed += ctx => OnJump?.Invoke();
            InputActions.Player.Sprint.performed += ctx => OnSprint?.Invoke(true);
            InputActions.Player.Sprint.canceled += ctx => OnSprint?.Invoke(false);
            InputActions.Player.Crouch.performed += ctx => OnCrouch?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
    }
}
