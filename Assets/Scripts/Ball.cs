using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool inPlay = false;

    private float spin = 0;
    private Vector3 startPos;
    private Quaternion startRot;

    private AudioSource audioSource;
    private PinCounter pinCounter;
    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        // Capture starting position
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Adds spin if there is any
        if (spin != 0) { AddSpin(spin); }

        // Prevents non moving ball from being stuck on lane
        if (inPlay)
        {
            if (IsMoving() == false)
            {
                StartCoroutine(StuckTimer());       // Start stuckTimer
            }
        }
    }

    // Resets ball if stuck on lane
    IEnumerator StuckTimer()
    {
        yield return new WaitForSeconds(3.0f);      // Waits for 3 seconds
        inPlay = false;
        pinCounter.SetBallOutOfPlay();
    }

    // Check if ball is moving
    public bool IsMoving()
    {
        if (rigidBody.velocity == Vector3.zero) { return false; }
        else { return true; }
    }

    // Launches ball
    public void Launch(Vector3 velocity)
    {
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;

        audioSource.Play();         // Start audio
    }

    // Reset ball position and parameters
    public void Reset()
    {
        inPlay = false;

        // Return ball to starting position
        transform.position = startPos;
        transform.rotation = startRot;

        // Reset ball movement
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;

        audioSource.Stop();         // Stop audio
    }

    // Adds spin to ball
    public void AddSpin(float totalSpin)
    {
        spin = Mathf.Clamp(totalSpin, -1, 1);
        Vector3 oldAngularVelocity = rigidBody.angularVelocity;
        rigidBody.angularVelocity = oldAngularVelocity + new Vector3(0, 0, spin);
        spin = spin * 0.95f;        // Spin amount degrades over time
    }
}
