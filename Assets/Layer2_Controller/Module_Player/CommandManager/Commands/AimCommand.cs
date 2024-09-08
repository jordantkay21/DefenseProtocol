using UnityEngine;

public class AimCommand : ICommand
{
    public void Execute(PlayerController playerController)
    {
        float yawCamera = playerController.playerView.mainCamera.transform.rotation.eulerAngles.y;
        Transform characterTransform = playerController.playerView.transform;

        characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, Quaternion.Euler(0, yawCamera, 0), playerController.playerModel.turnSpeed * Time.deltaTime);
    }
}
