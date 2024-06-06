using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public void GameOver()
    {
        DataManager.Instance.currentPlayer.isWon = true;
    }
}
