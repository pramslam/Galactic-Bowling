using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    public Text standingDisplay;

    private bool ballOutOfPlay = false;
    private int lastStandingCount = -1;         // -1 indicates pins have settled
    private int lastSettledCount = 10;
    private float lastChangeTime = 0.0f;
    private float pinSettleTime = 3.0f;

    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            UpdateStandingCountandSettled();
        }
    }

    // Set ball out of play when exiting lane box collider
    void OnTriggerExit(Collider collider)
    {
        GameObject ball = collider.gameObject;

        if (ball.GetComponent<Ball>())
        {
            ballOutOfPlay = true;
            standingDisplay.color = Color.red;
        }
    }

    // Used by ball to set out of play
    public void SetBallOutOfPlay() { ballOutOfPlay = true; }

    // Used by pinSetter to reset last settled count
    public void Reset() { lastSettledCount = 10; }

    // Updates standing pins and resets when pins have settled
    void UpdateStandingCountandSettled()
    {
        int currentStanding = CountStanding();

        // Update lastStandingCount
        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        // Waits for pins to settle before resetting
        if ((Time.time - lastChangeTime) > pinSettleTime)
        {
            PinsHaveSettled();
        }
    }

    // Counts standing pins
    int CountStanding()
    {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding() == true)
            {
                standing++;
            }
        }

        return standing;
    }

    // Resets ball and flags when pins have settled
    void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);              // Pass data to GameManager

        lastStandingCount = -1;
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }
}
