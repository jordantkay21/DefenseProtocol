using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailDebugger : MonoBehaviour
{
    public float trailLifetime = 2f; //Time in seconds before the trail disappears
    public float trailWidth = 0.05f; //Width of the trail

    private LineRenderer lineRenderer;
    private Queue<Vector3> positions;
    private Queue<float> timestamps;

    private void Start()
    {
        //Set up the Linerenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = trailWidth;
        lineRenderer.endWidth = trailWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;

        //Initialize the previous position to the starting position
        positions = new Queue<Vector3>();
        timestamps = new Queue<float>();
    }


    private void Update()
    {
        //Update the current position and timeStamp
        positions.Enqueue(transform.position);
        timestamps.Enqueue(Time.time);

        //Update the LineRenderer positions
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        //Remove old positions that exceed the trail lifetime
        while (timestamps.Count > 0 && Time.time - timestamps.Peek() > trailLifetime)
        {
            positions.Dequeue();
            timestamps.Dequeue();
        }
    }
}
