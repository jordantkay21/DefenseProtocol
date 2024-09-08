
public class IdleState : IPlayerState
{
    public void EnterState(PlayerController playerController)
    {
        playerController.playerView.animator.SetFloat("InputX", 0);
        playerController.playerView.animator.SetFloat("InputZ", 0);

        DebugUtility.Log(DebugTag.Module_Player, "Player has Entered Idle State");
    }
    public void UpdateState(PlayerController playerController)
    {
        // Handle input to transition to WalkingState
        if (PlayerInputManager.Instance.GetMoveInput().sqrMagnitude > 0)
            playerController.TransitionToState(new WalkingState());

        //Handle input to transition to SprintingState
        if (playerController.playerModel.isSprinting == true && PlayerInputManager.Instance.GetMoveInput() != UnityEngine.Vector2.zero)
            playerController.TransitionToState(new SprintState());
    }

    public void ExitState(PlayerController playerController)
    {
        DebugUtility.Log(DebugTag.Module_Player, "Player has Exited Idle State");
    }

}
