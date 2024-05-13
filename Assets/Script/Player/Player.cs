using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string iD { get; set; }
    private string userName { get; set; }
    private int score { get; set; }

    public string returnNameScore(int score)
    {
        return "0";
    }

    public Player(string iD, string userName, int score)
    {
        this.iD = iD;
        this.userName = userName;
        this.score = score;
    }

}
