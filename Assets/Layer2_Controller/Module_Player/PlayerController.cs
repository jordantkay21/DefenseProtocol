using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set;}


    public PlayerModel playerModel { get; private set; }
    public PlayerView playerView { get; private set; }

    private IPlayerState currentState;

    private ICommand gravityCommand;
    private ICommand aimCommand;


    private void OnEnable()
    {
        PlayerInputManager.Instance.OnSprint += IsSprinting;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.OnSprint -= IsSprinting;
    }

    private void IsSprinting(bool isSprinting)
    {
        playerModel.isSprinting = isSprinting;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerView = (PlayerView)FindFirstObjectByType(typeof(PlayerView));
    }

    void Start()
    {
        DebugUtility.Log(DebugTag.Module_Player, "PlayerController Start");
        playerModel = new PlayerModel();
        currentState = new WalkingState();
        gravityCommand = new GravityCommand();
        aimCommand = new AimCommand();
        currentState.EnterState(this);
        
    }

    void Update()
    {
        //Update current state (state-specific behavior)
        currentState.UpdateState(this);

        // Apply gravity in all states
        ExecuteCommand(gravityCommand);

        //Apply AimCommand to all states EXCEPT
        ExecuteCommand(aimCommand);
            

        if (playerView.characterController.isGrounded)
            playerModel.isGrounded = true;
        else
            playerModel.isGrounded = false;
    }

    public void TransitionToState(IPlayerState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute(this);
    }

    #region MediationMethods
    public IPlayerState RetrieveCurrentState()
    {
        return currentState;
    }

    public int RetrieveHealthStat()
    {
        return playerModel.health;
    }

    public bool RetrieveIsGrounded()
    {
        return playerModel.isGrounded;
    }
    #endregion

    #region PlayerLogic

    public bool IsGroundedWithRaycast()
    {
        float distanceToGround = .2f; //Adjust based on your player's height and ground detection 
        RaycastHit hit;
        Vector3 localDown = playerView.transform.TransformDirection(Vector3.down);
        Vector3 startPos = playerView.transform.GetChild(0).transform.position;


        //Visualize the ray in the Scene View
        Debug.DrawLine(startPos, playerView.transform.position + localDown * 0.1f , Color.red);

        if (Physics.Raycast(startPos, playerView.transform.position + localDown, out hit, distanceToGround))
            return true;
        
        return false;
    }

    #endregion
}
