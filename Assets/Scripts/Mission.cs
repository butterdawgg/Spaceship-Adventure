using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    KillEnemies,
    GetScore
}

public class Mission
{
    public string description;
    public ActionType actionType;
    public float amount;

    public Mission()
    {
        int random = Random.Range(0, 1);

        if (random == 0)
        {
            actionType = ActionType.KillEnemies;
            amount = Random.Range(5, 20);
            description = "Kill " + amount + " enemies";
        }
        else if (random == 1)
        {
            actionType = ActionType.GetScore;
            amount = Random.Range(100, 500);
            description = "Get " + amount + " score points";
        }
    }
}
