using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{

    public int currentExp { get; private set; }
    private int expToLevelUp;
    public int playerLevel { get; private set; }

    private void Start()
    {

    }

    public void AddExperience(int amount)
    {
        currentExp += amount;

        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        playerLevel += 1;
        currentExp = 0;
        Debug.Log("Level Up! Current Level: " + playerLevel);
    }
}
