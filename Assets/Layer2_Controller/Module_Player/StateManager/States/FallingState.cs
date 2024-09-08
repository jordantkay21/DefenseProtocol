
using System;
using UnityEngine;

public class FallingState : IPlayerState
{
    public void EnterState(PlayerController controller)
    {
        DebugUtility.Log(DebugTag.Module_Player, $"Player is Entering Falling State");
    }

    public void UpdateState(PlayerController controller)
    {
        if (PlayerInputManager.Instance.GetMoveInput().sqrMagnitude > 0)
            controller.TransitionToState(new WalkingState());
        else
            controller.TransitionToState(new IdleState());
    }

    public void ExitState(PlayerController controller)
    {
        DebugUtility.Log(DebugTag.Module_Player, $"Player is Exiting Falling State");
    }

}
