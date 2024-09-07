using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public PlayerModel playerModel { get; private set; }
    public PlayerView playerView { get; private set; }

    private IPlayerState currentState;

    private void Awake()
    {
        playerView = (PlayerView)FindFirstObjectByType(typeof(PlayerView));
    }

    void Start()
    {
        DebugUtility.Log(DebugTag.Module_Player, "PlayerController Start");
        playerModel = new PlayerModel();
        currentState = new WalkingState();
        currentState.EnterState(this);
    }

    void Update()
    {
        //Update current state (state-specific behavior)
        currentState.UpdateState(this);
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
}
