using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will draw a line showing the predicted trajectory based on force, angle, and gravity.

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lineRenderer;
    public Transform launchPoint;

    [Header("Settings")]
    public int lineSegmentCount = 30; // The amount of points to draw
    public float timeStep = 0.1f; // Time between points
    private float gravity;

    public float maxDistance = 10f; // Maximum distance before trajectory line stops.

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        gravity = Mathf.Abs(Physics2D.gravity.y);
    }

    public void ShowTrajectory(Vector2 startPos, Vector2 startVelocity)
    {
        List<Vector3> points = new List<Vector3>();

        float t = 0f;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float x = startPos.x + startVelocity.x * t;
            float y = startPos.y + startVelocity.y * t - 0.5f * gravity * t * t;
            Vector3 newPoint = new Vector3(x, y, 0);

            points.Add(newPoint);

            if (Vector2.Distance(startPos, newPoint) >= maxDistance)
                break;
            t += timeStep;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
        // Hides the trajectory line when player releases the mouse.
    }
}
