using UnityEngine;
public class MoveCommand : ICommand
{
    private Vector2 moveInput;

    public MoveCommand(Vector2 input)
    {
        moveInput = input;
    }
    public void Execute(PlayerController playerController)
    {
        playerController.playerView.animator.SetFloat("InputX", moveInput.x);
        playerController.playerView.animator.SetFloat("InputZ", moveInput.y);
    }
}
