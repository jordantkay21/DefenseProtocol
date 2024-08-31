using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractionManager : MonoBehaviour, IEventListener<NewWeaponEvent>
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] Transform crossHairTarget;
    [SerializeField] LayerMask interactableLayer;

    private IInteractable currentInteractable;
    private RaycastHit hitInfo;
    private WeaponBase equippedWeapon;
    protected EventManager weaponEvents;

    private void Awake()
    {
        weaponEvents = GameManager.Instance.WeaponEvents;
    }
    private void OnEnable()
    {
        InputManager.Instance.OnInteract += HandleInteract;
        weaponEvents.Subscribe<NewWeaponEvent>(OnEvent);
        Debug.Log($"OnEvent() is subscribed to newWeaponEvent");
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteract -= HandleInteract;
        weaponEvents.Unsubscribe<NewWeaponEvent>(OnEvent);
    }

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Visualize the ray in the scene view
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green);

        if(Physics.Raycast(ray, out hitInfo, interactionRange, interactableLayer))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable != currentInteractable)
            {
                currentInteractable = interactable;
                Debug.Log($"Interactable in Range: {hitInfo.collider.gameObject.name}");
            }

        }
        else
        {
            //If no hit, set the end position of the LineRenderer to the max range
            currentInteractable = null;
        }
    }

    private void HandleInteract()
    {
        if(currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    public void OnEvent(NewWeaponEvent eventArgs)
    {
            equippedWeapon = eventArgs.NewWeapon;
            Debug.Log($"New Weapon was picked up: {equippedWeapon}");
    }
}
