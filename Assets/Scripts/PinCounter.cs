using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    public enum Sound { Strike, Spare, Gutter, Undefined };

    public Text standingDisplay;

    private bool audioPlayed = false;
    private bool ballOutOfPlay = false;
    private int audioLimit = 3;
    private int lastStandingCount = -1;         // -1 indicates pins have settled
    private int lastSettledCount = 10;
    private float audioTime = 0.2f;
    private float lastChangeTime = 0.0f;
    private float pinSettleTime = 3.0f;

    private AudioSource[] audioSource;
    private AudioSource spareAudio;
    private AudioSource strikeAudio;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        spareAudio = audioSource[0];
        strikeAudio = audioSource[1];
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

    // Plays audio depending on number of pins that fall
    public Sound ProcessAudio(int lastStanding, int standing)
    {
        Sound nextSound = Sound.Undefined;
        audioPlayed = true;

        // Play audio
        if (standing == lastStanding)                                 // No pins hit
        {
            nextSound = Sound.Gutter;
        }
        else if (standing == 0 && lastStanding > audioLimit)          // Special case, spare larger than limit
        {
            nextSound = Sound.Strike;
            PlayAudio(Sound.Strike);
        }
        else if (lastStanding - standing > audioLimit)                // Hit more than limit
        {
            nextSound = Sound.Strike;
            PlayAudio(Sound.Strike);
        }
        else if (lastStanding - standing <= audioLimit)               // Hit limit or less
        {
            nextSound = Sound.Spare;
            PlayAudio(Sound.Spare);
        }
        return nextSound;
    }

    // Plays audio
    public void PlayAudio(Sound audio)
    {
        switch (audio)
        {
            case Sound.Gutter:
                break;
            case Sound.Spare:
                //spareAudio.Play();
                break;
            case Sound.Strike:
                //strikeAudio.Play();
                break;
            case Sound.Undefined:
                break;
        }
    }

    // Used by ball to set out of play
    public void SetBallOutOfPlay() { ballOutOfPlay = true; }

    // Used by pinSetter to reset last settled count
    public void Reset() { lastSettledCount = 10; }

    // Updates standing pins, plays audio, and resets when pins have settled
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

        // Wait for initial count and play audio once
        if ((Time.time - lastChangeTime) > audioTime && audioPlayed == false)
        {
            ProcessAudio(lastStandingCount, CountStanding());
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

        // Reset audio
        audioPlayed = false;

        gameManager.Bowl(pinFall);              // Pass data to GameManager

        lastStandingCount = -1;
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }
}
