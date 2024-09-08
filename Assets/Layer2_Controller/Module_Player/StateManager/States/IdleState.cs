
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

        //Check if the player is grounded, if not, transition to FallingState
        if (!playerController.IsGroundedWithRaycast())
        {
            playerController.TransitionToState(new FallingState());
        }
    }

    public void ExitState(PlayerController playerController)
    {
        DebugUtility.Log(DebugTag.Module_Player, "Player has Exited Idle State");
    }

}
