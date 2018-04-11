using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEnd gameEnd;

    private List<int> rolls = new List<int>();
    private List<int> boxes = new List<int>();
    private List<int> totalScore = new List<int>();

    private Ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        gameEnd = GameObject.FindObjectOfType<GameEnd>();
    }

    // Processes scoring
    public void Bowl(int pinFall)
    {
        // Process bowl
        rolls.Add(pinFall);
        boxes.Add(pinFall);
        ball.Reset();

        // Pass animation trigger
        pinSetter.ProcessAction(ActionMaster.NextAction(boxes));

        // Fill scorecard
        totalScore = ScoreMaster.ScoreCumulative(rolls);
        scoreDisplay.FillRolls(rolls);
        scoreDisplay.FillFrames(totalScore);
        TotalScore();
    }

    // Passes total score to EndGame
    public void TotalScore()
    {
        int count = totalScore.Count;
        if (count != 0)                                 // Required or savedRolls could equal -1
        {
            int cumulative = totalScore[count - 1];
            gameEnd.UpdateScore(cumulative);
        }
    }
}
