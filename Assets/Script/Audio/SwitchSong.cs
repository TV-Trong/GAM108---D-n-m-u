using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSong : MonoBehaviour
{
    public void MainMenuSong()
    {
        BGMusic.Instance.PlayASong(0);
    }

    public void GamePlaySong()
    {
        BGMusic.Instance.PlayASong(1);
    }
}
