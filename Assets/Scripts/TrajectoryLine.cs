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

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        gravity = Mathf.Abs(Physics2D.gravity.y);

        // Create a color gradient that fades out along the line.
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.cyan, 0.0f), // Start color
                new GradientColorKey(Color.blue, 1.0f) // End color
                },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
                }
        );
        lineRenderer.colorGradient = gradient;
    }

    public void ShowTrajectory(Vector2 startPos, Vector2 startVelocity)
    {
        Vector3[] points = new Vector3[lineSegmentCount];

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeStep;

            float x = startPos.x + startVelocity.x * t;
            float y = startPos.y + startVelocity.y * t - 0.5f * gravity * t * t;

            points[i] = new Vector3(x, y, 0);
        }

        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.SetPositions(points);
    }

    public void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
        // Hides the trajectory line when player releases the mouse.
    }
}
