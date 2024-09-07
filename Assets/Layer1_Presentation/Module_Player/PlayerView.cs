using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerView : MonoBehaviour
{
     public CharacterController characterController { get; private set; }
     public Animator animator { get; private set; }

    private void Start()
    {
        InitializeAnimator();
        characterController = GetComponent<CharacterController>();
    }

    #region Component Initilization
    private void InitializeAnimator()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }
    #endregion

    private void OnAnimatorMove()
    {
        if(characterController && animator.applyRootMotion)
        {
            //Use Animator's root motion for movement
            Vector3 movement = animator.deltaPosition;

            //Apply any gravity or additional movement adjustments here

            //Move the CharacterController using the calculated root motion
            characterController.Move(movement);
        }
    }
}
