using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum Sound { Strike, Spare, Gutter, Undefined };

    private bool audioPlayed = false;
    private int audioLimit = 3;
    private float audioTimer = 0.2f;

    // Use this for initialization
    void Start () {
		
	}
    
    public bool GetAudioPlayed() { return audioPlayed; }                // Limits the audio to play once
    public float GetAudioTimer() { return audioTimer; }                 // Length of time before audio is played
    public void ResetAudio() { audioPlayed = false; }                   // Reset audio

    // Plays audio depending on number of pins that fall
    public Sound ProcessAudio(int lastStanding, int standing)
    {
        Sound nextSound = Sound.Undefined;
        audioPlayed = true;

        // Play audio
        if (standing == lastStanding)                                   // No pins hit
        {
            nextSound = Sound.Gutter;
        }
        else if (standing == 0 && lastStanding > audioLimit)            // Special case, spare larger than limit
        {
            nextSound = Sound.Strike;
            PlayAudio(Sound.Strike);
        }
        else if (lastStanding - standing > audioLimit)                  // Hit more than limit
        {
            nextSound = Sound.Strike;
            PlayAudio(Sound.Strike);
        }
        else if (lastStanding - standing <= audioLimit)                 // Hit limit or less
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
                Debug.Log("Gutter Audio");
                break;
            case Sound.Spare:
                //spareAudio.Play();
                Debug.Log("Spare Audio");
                break;
            case Sound.Strike:
                //strikeAudio.Play();
                Debug.Log("Strike Audio");
                break;
            case Sound.Undefined:
                Debug.Log("Undefined Audio");
                break;
        }
    }
}
