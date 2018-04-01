using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame, Undefined };

    public static Action NextAction(List<int> rolls)
    {
        Action nextAction = Action.Undefined;

        for (int i = 0; i < rolls.Count; i++)               // Step through rolls
        {
            if (i == 20)                                    // End game at bonus frame
            {
                nextAction = Action.EndGame;
            }
            else if (i >= 18 && rolls[i] == 10)             // Strike on frame 19
            {
                nextAction = Action.Reset;
            }
            else if (i == 19)                               // Last frame special cases
            {
                if (rolls[18] == 10 && rolls[19] == 0)      // Frame 18 strike, second roll
                {
                    nextAction = Action.Tidy;
                }
                else if (rolls[18] + rolls[19] == 10)       // Frame 18 + 19, spare roll
                {
                    nextAction = Action.Reset;
                }
                else if (rolls[18] + rolls[19] >= 10)       // Bonus frame awarded
                {
                    nextAction = Action.Tidy;
                }
                else
                {
                    nextAction = Action.EndGame;
                }
            }
            else if (i % 2 == 0)                            // First bowl of frame
            {
                if (rolls[i] == 10)
                {
                    rolls.Insert(i, 0);                     // Insert zero roll after strike
                    nextAction = Action.EndTurn;
                }
                else
                {
                    nextAction = Action.Tidy;
                }
            }
            else                                            // Second bowl of frame
            {
                nextAction = Action.EndTurn;
            }
        }

        return nextAction;
    }
}