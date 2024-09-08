using UnityEngine;

public class GroundedCommand : ICommand
{
    //Grounded Buffer Variables
    [Tooltip("Time in seconds before considering the player ungrounded")]
    private float groundBufferTime = 0.2f;
    private float ungroundedTimer;
    private bool isBufferedGrounded = true;

    //Raycast settings
    [Tooltip("The distance for the raycast ground check")]
    private float groundCheckDistance = 0.3f;
    [Tooltip("The layer(s) that should be considered as ground")]
    private LayerMask groundLayer;
    [Tooltip("The slop limit to check against")]
    private float slopeLimit = 45f;

    public void Execute(PlayerController playerController)
    {
        //Perform a Raycast to cehck for ground
        RaycastHit hit;
        bool raycastGrounded = Physics.Raycast(playerController.playerView.transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        //If the raycast hits something, check the slope angle
        if (raycastGrounded)
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            //If the slope angle is less than the slope limit, the player is grounded
            if(slopeAngle <= slopeLimit)
            {
                ungroundedTimer = 0;
                playerController.playerModel.isGrounded = true;
            }
            else
            {
                raycastGrounded = false;
            }
        }

        //Check if the character controller itself consideres the player grounded
        if (playerController.playerView.characterController.isGrounded || raycastGrounded)
        {
            ungroundedTimer = 0f;
            playerController.playerModel.isGrounded = true;
        }
        else
        {
            //If neither method finds the player grounded, start the buffer timer
            ungroundedTimer += Time.deltaTime;

            if(ungroundedTimer >= groundBufferTime)
            {
                playerController.playerModel.isGrounded = false;
            }
        }
    }
}
