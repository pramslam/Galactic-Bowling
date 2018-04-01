using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text[] rollTexts, frameTexts;

    // Fills bowl boxes with rolls
    public void FillRolls(List<int> rolls)
    {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++)
        {
            rollTexts[i].text = scoresString[i].ToString();
        }
    }

    // Fills score boxes with cumulative score
    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameTexts[i].text = frames[i].ToString();
        }
    }

    // Formats rolls in a string
    public static string FormatRolls(List<int> rolls)
    {
        string output = "";
        for (int i = 0; i < rolls.Count; i++)
        {
            int box = output.Length + 1;                                                // Scorebox 1 to 21

            if (rolls[i] == 0)                                                          // Gutter "-"
            {
                output += "-";
            }
            else if ((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10 && output[output.Length - 1].ToString() != "/")      // Spare "/" and Special Case, frames 20 + 21 not spare
            {
                output += "/";
            }
            else if (box >= 19 && rolls[i] == 10)                                       // Strike "X" for frame 10
            {
                output += "X";
            }
            else if (rolls[i] == 10)                                                    // Strike "X " for frames 1-9
            {
                output += "X ";
            }
            else                                                                        // Normal bowl for frames 1-9
            {
                output += rolls[i].ToString();
            }
        }

        return output;
    }
}
