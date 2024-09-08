using UnityEngine;

public class SprintState : IPlayerState
{
    public void EnterState(PlayerController playerController)
    {
        playerController.playerView.animator.SetBool("isSprinting", true);
    }
    public void UpdateState(PlayerController playerController)
    {
        Vector2 movementInput = PlayerInputManager.Instance.GetMoveInput();

        if (movementInput == Vector2.zero)
            playerController.TransitionToState(new IdleState());

        if (playerController.playerModel.isSprinting == false)
            playerController.TransitionToState(new WalkingState());
    }

    public void ExitState(PlayerController playerController)
    {
        playerController.playerView.animator.SetBool("isSprinting", false);
    }

}
