using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private float standingThreshold = 3.0f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Determines if pin is standing, accounts for pin wobbling
    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        // DeltaAngle fixes false flag from wobbling Pins by returning an angle that has been calculated with 2*pi
        float tiltInX = Mathf.Abs(Mathf.DeltaAngle(rotationInEuler.x, 0));
        float tiltInZ = Mathf.Abs(Mathf.DeltaAngle(rotationInEuler.z, 0));

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold) { return true; }
        else { return false; }
    }

    // Raises pin for tidying
    public void RaiseIfStanding(float distanceToRaise)
    {
        if (IsStanding())
        {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            // Disable movement, otherwise pins will drift when raised
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.freezeRotation = true;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;     // Reset collision detection for standing pins
        }
    }

    // Lowers pin
    public void Lower(float distanceToRaise)
    {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        rigidBody.useGravity = true;
        rigidBody.freezeRotation = false;
    }

    // Setup parameters for new pin
    public void RenewNewPin()
    {
        rigidBody = GetComponent<Rigidbody>();              // Required for new pins
        rigidBody.useGravity = false;
        rigidBody.freezeRotation = true;
    }

    // Used for dynamic collision detection
    public void SetCollisionContinousDynamic()
    {
        rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}
