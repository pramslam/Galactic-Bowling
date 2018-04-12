using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public enum Sound { Strike, Spare, Gutter, Undefined };
    public AudioSource audioSource;
    public AudioClip spare, strike;

    private bool audioPlayed = false;
    private int minPinLimit = 3;
    private float audioTimer = 0.2f;

    // Use this for initialization
    void Start () {
        AudioSource audioSource = GetComponent<AudioSource>();
    }
    
    public bool GetAudioPlayed() { return audioPlayed; }                // Limits the audio to play once
    public float GetAudioTimer() { return audioTimer; }                 // Length of time before audio is played
    public void ResetAudio() { audioPlayed = false; }                   // Reset audio

    // Processes audio and returns depending on number of pins that fall
    public Sound ProcessAudio(int lastStanding, int standing)
    {
        Sound nextSound = Sound.Undefined;
        audioPlayed = true;

        if (standing == lastStanding)                                   // No pins hit
        {
            nextSound = Sound.Gutter;
        }
        else if (standing == 0 && lastStanding > minPinLimit)           // Special case, spare larger than limit
        {
            nextSound = Sound.Strike;
        }
        else if (lastStanding - standing > minPinLimit)                 // Hit more than limit
        {
            nextSound = Sound.Strike;
        }
        else if (lastStanding - standing <= minPinLimit)                // Hit limit or less
        {
            nextSound = Sound.Spare;
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
                audioSource.PlayOneShot(spare, 1.0f);
                break;
            case Sound.Strike:
                audioSource.PlayOneShot(strike, 1.0f);
                break;
            case Sound.Undefined:
                break;
        }
    }
}
