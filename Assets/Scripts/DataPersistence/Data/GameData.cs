using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class GameData
{
    public bool[] collectedCoins;
    public bool collectedStar;
    
    public GameData()
    {
        int coinsCount = GameObject.FindObjectsOfType<CoinCollectScript>().Length;
        
        this.collectedCoins = Enumerable.Repeat(false, coinsCount).ToArray();
        this.collectedStar = false;
    }
}
