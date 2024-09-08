using UnityEngine;

public class WalkingState : IPlayerState
{
    public void EnterState(PlayerController playerController)
    {
        DebugUtility.Log(DebugTag.Module_Player, "Player has Entered Walking State");
    }
    public void UpdateState(PlayerController playerController)
    {
        Vector2 movementInput = PlayerInputManager.Instance.GetMoveInput();
        playerController.ExecuteCommand(new MoveCommand(movementInput));

        if (playerController.playerModel.isSprinting == true)
            playerController.TransitionToState(new SprintState());

        if (PlayerInputManager.Instance.GetMoveInput() == Vector2.zero)
            playerController.TransitionToState(new IdleState());
        
    }

    public void ExitState(PlayerController playerController)
    {
        DebugUtility.Log(DebugTag.Module_Player, "Player has Exited the Walking State");
    }

}
