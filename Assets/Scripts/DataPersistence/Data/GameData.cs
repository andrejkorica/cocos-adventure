using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class GameData
{
    public bool[] collectedCoins;
    public bool collectedStar;
    public int bestTime;
    public bool levelPassed;
    
    public GameData()
    {
        int coinsCount = GameObject.FindObjectsOfType<CoinCollectScript>().Length;
        
        this.collectedCoins = Enumerable.Repeat(false, coinsCount).ToArray();
        this.collectedStar = false;

        this.bestTime = -1;
        this.levelPassed = false;
    }
}
