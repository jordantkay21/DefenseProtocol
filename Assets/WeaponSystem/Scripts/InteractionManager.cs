using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractionManager : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] LayerMask interactableLayer;

    private IInteractable currentInteractable;
    private RaycastHit hitInfo;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //Configure the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInteract += HandleInteract;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteract -= HandleInteract;
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
        lineRenderer.SetPosition(0, ray.origin); //Set the starting position of the LineRenderer

        if(Physics.Raycast(ray, out hitInfo, interactionRange, interactableLayer))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable != currentInteractable)
            {
                currentInteractable = interactable;
                Debug.Log($"Interactable in Range: {hitInfo.collider.gameObject.name}");
            }

            //Set the end position of the LineRenderer to the hit point
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            //If no hit, set the end position of the LineRenderer to the max range
            lineRenderer.SetPosition(1, ray.origin + ray.direction * interactionRange);
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

    private void OnDrawGizmos()
    {
        if(hitInfo.collider != null)
        {
            //Draw a line from the ray's origin to the hit point
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Camera.main.transform.position, hitInfo.point);

            //Draw a small sphere at the hit point
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
        else
        {
            //Draw the ray to its maximum length
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * interactionRange);
        }
    }
}
