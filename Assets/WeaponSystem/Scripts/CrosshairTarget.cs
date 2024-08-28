using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    new Camera camera;
    Ray ray;
    RaycastHit hitInfo;

    public float defaultDistance = 50f; //Set the distance you want when no object is hit

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        ray.origin = camera.transform.position;
        ray.direction = camera.transform.forward;

        //Perform the raycast and check if it hits something
        if (Physics.Raycast(ray, out hitInfo)) 
            //If it hits something, set the position to the hit point
            transform.position = hitInfo.point; 
        else 
            //If it doesn't hit anything, set the position to a point in front og the camera at the default distance
            transform.position = ray.origin + ray.direction * defaultDistance;
    }
}
