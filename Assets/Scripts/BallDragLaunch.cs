using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{
    private bool dragLaunch = true;
    private float nudgeLimit = 45f;
    private float minLaunchSpeed = 1400f;
    private float startTime, endTime;
    private Vector3 dragStart, dragEnd;

    private Ball ball;

    // Use this for initialization
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    // Allows setting ball position at start
    public void NudgeAtStart(float amount)
    {
        if (ball.inPlay == false)           // Prevents nudge if ball is in play
        {
            float xPos = Mathf.Clamp(ball.transform.position.x + amount, -nudgeLimit, nudgeLimit);
            float yPos = ball.transform.position.y;
            float zPos = ball.transform.position.z;
            ball.transform.position = new Vector3(xPos, yPos, zPos);
        }
    }

    // Handles launch input
    #region LaunchInput
    public void DragStart()
    {
        if (ball.inPlay == false && ball.canLaunch == true)           // Prevents dragging if ball is in play
        {
            dragLaunch = true;

            // Captures start time and position of drag
            startTime = Time.time;
            dragStart = Input.mousePosition;
        }
    }

    public void DragEnd()
    {
        if (ball.inPlay == false && ball.canLaunch == true)           // Prevents dragging if ball is in play
        {
            ball.inPlay = true;

            // Calculates swipe duration
            endTime = Time.time;
            dragEnd = Input.mousePosition;
            float dragDuration = endTime - startTime;

            // Calculates launch speed and direction
            float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
            float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;
            Vector3 launchVelocity = new Vector3(launchSpeedX, 0, Mathf.Clamp(launchSpeedZ, minLaunchSpeed, float.MaxValue));
            ball.Launch(launchVelocity);            // Launch ball

            StartCoroutine(DragLaunchTimer());      // Timer for dragLaunch
        }
    }

    // Prevents triggering SpinDragEnd at end of LaunchDrag
    IEnumerator DragLaunchTimer()
    {
        yield return new WaitForSeconds(0.1f);      // Waits for 0.1 seconds
        dragLaunch = false;
    }
    #endregion

    // Handles spin input
    #region SpinInput
    public void SpinDragStart()
    {
        if (ball.inPlay == true && dragLaunch == false)       // Allow spin when in play and launch complete
        {
            // Captures start time and position of drag
            startTime = Time.time;
            dragStart = Input.mousePosition;
        }
    }

    public void SpinDragEnd()
    {
        if (ball.inPlay == true && dragLaunch == false)       // Allow spin when in play and launch complete
        {
            dragLaunch = true;

            // Calculates swipe duration
            endTime = Time.time;
            dragEnd = Input.mousePosition;
            float dragDuration = endTime - startTime;

            // Calculates spin direction and speed
            float dragDirection = -(dragEnd.x - dragStart.x);
            float totalSpin = (dragDirection / dragDuration);

            ball.AddSpin(totalSpin);                // Add spin to ball
        }
    }
    #endregion

    // Debug Inputs
    #region Debug
    public void StrikeLaunch()
    {
        ball.inPlay = true;
        dragLaunch = false;
        ball.Launch(new Vector3(10, 0, 2000));
    }

    public void SpareLaunch()
    {
        ball.inPlay = true;
        dragLaunch = false;
        ball.Launch(new Vector3(5, 0, 2000));
    }

    public void SparePickupLaunch()
    {
        ball.inPlay = true;
        dragLaunch = false;
        ball.Launch(new Vector3(-40, 0, 2000));
    }
    #endregion
}
