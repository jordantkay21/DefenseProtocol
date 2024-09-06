using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractionManager : MonoBehaviour, IEventListener<NewWeaponEvent>
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] Transform crossHairTarget;
    [SerializeField] Transform rightHand;
    [SerializeField] LayerMask interactableLayer;

    private IInteractable currentInteractable;
    private RaycastHit hitInfo;

    //Event Managers
    private EventManager weaponEvents;
    private EventManager interactionEvents;

    private WeaponBase equippedWeapon;
    private Ray interactionRay;

    private void Awake()
    {
        weaponEvents = GameManager.Instance.WeaponEvents;
        interactionEvents = GameManager.Instance.InteractionEvents;
    }
    private void OnEnable()
    {
        InputManager.Instance.OnInteract += HandleInteract;
        weaponEvents.Subscribe<NewWeaponEvent>(OnEvent);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteract -= HandleInteract;
        weaponEvents.Unsubscribe<NewWeaponEvent>(OnEvent);
    }
    public void OnEvent(NewWeaponEvent eventArgs)
    {
        // Normal event handling logic
        equippedWeapon = eventArgs.NewWeapon;
        DebugUtility.Log(DebugTag.InteractionSystem,$"New Weapon Equipped: {eventArgs.NewWeapon.gameObject.name}");

    }

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        if (!equippedWeapon)
        {
            interactionRay = Raycasthandler.Instance.CreateRay(rightHand.position, crossHairTarget.position);
        }
        else
        {
            interactionRay = Raycasthandler.Instance.CreateRay(equippedWeapon.muzzleTransform.position, crossHairTarget.position);
        }


        if(Physics.Raycast(interactionRay, out hitInfo, interactionRange, interactableLayer))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable != currentInteractable)
            {
                currentInteractable = interactable;
                interactionEvents.Publish(new InteractableEvent(Color.green));
                DebugUtility.Log(DebugTag.InteractionSystem, $"Interactable in Range: {hitInfo.collider.gameObject.name}");
            }

        }
        else
        {
            //If no hit, set the end position of the LineRenderer to the max range
            currentInteractable = null;
            interactionEvents.Publish(new InteractableEvent(Color.blue));
        }
    }

    private void HandleInteract()
    {
        if(currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

}
