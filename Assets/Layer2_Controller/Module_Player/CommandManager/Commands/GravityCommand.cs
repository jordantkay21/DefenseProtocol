using UnityEngine;

public class GravityCommand : ICommand
{
    private float gravity = -9.81f;
    private float fallVelocity = 0f;

    public GravityCommand(float initialFallVelocity = 0f)
    {
        fallVelocity = initialFallVelocity;
    }
    public void Execute(PlayerController playerController)
    {
        // Check if grounded to reset the fall velocity
        if (playerController.playerView.characterController.isGrounded)
        {
            fallVelocity = 0f;  // Reset fall velocity if grounded
        }
        else
        {
            // Apply gravity if not grounded
            fallVelocity += gravity * Time.deltaTime;
        }

        // Create a Vector3 for the gravity force (applied downward)
        Vector3 gravityForce = new Vector3(0, fallVelocity, 0);

        // Move the CharacterController by applying the gravity force
        playerController.playerView.characterController.Move(gravityForce * Time.deltaTime);
    }
}
