using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }
    public PlayerInputActions InputActions { get; private set; }

    public event Action<Vector2> OnMove; //Event triggered when movement input is recieved
    public event Action<Vector2> OnLook; //Event triggered when look input [mouse movement] is recieved
    public event Action OnJump; //Triggered when jump input is recieved
    public event Action<bool> OnSprint; //Triggered when the sprint input is pressed or released
    public event Action OnCrouch; //Triggered when the crouch input is recieved
    public event Action OnFire; //Triggered when the Fire input is recieved
    public event Action OnFireStarted;
    public event Action OnFireStopped;
    public event Action OnReload; //Triggered when the reload input is recieved
    public event Action OnInteract; //Triggered when the interact input is recieved

    private Vector2 moveInput; //Stores the current mvoement input

    private void OnEnable()
    {
        InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Ensures that this GameObject persists across scene loads
            InputActions = new PlayerInputActions();

            InputActions.Player.Move.performed += ctx => SetMoveInput(ctx.ReadValue<Vector2>());
            InputActions.Player.Move.canceled += ctx => SetMoveInput(Vector2.zero);
            InputActions.Player.Look.performed += ctx => OnLook?.Invoke(ctx.ReadValue<Vector2>());
            InputActions.Player.Jump.performed += ctx => OnJump?.Invoke();
            InputActions.Player.Sprint.performed += ctx => OnSprint?.Invoke(true);
            InputActions.Player.Sprint.canceled += ctx => OnSprint?.Invoke(false);
            InputActions.Player.Crouch.performed += ctx => OnCrouch?.Invoke();


            InputActions.Player.Fire.performed += ctx => OnFire?.Invoke();
            InputActions.Player.Fire.started += ctx => OnFireStarted?.Invoke();
            InputActions.Player.Fire.canceled += ctx => OnFireStopped?.Invoke();
            InputActions.Player.Reload.performed += ctx => OnReload?.Invoke();
            
            
            InputActions.Player.Interact.performed += ctx => OnInteract?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (moveInput != Vector2.zero)
            OnMove?.Invoke(moveInput);
    }

    private void SetMoveInput(Vector2 input)
    {
        moveInput = input;
        DebugUtility.Log(DebugTag.Component_InputManager, $"Movement Input Detected. moveInput = {moveInput}");
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
