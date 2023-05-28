using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class GameData
{
    public bool[] collectedCoins;
    public bool collectedStar;

    // Ovo se svaki put reseta, ne bi trebalo biti ovisno o levelu
    public AttributesData playerAttributesData;

    public GameData() 
    {
        int coinsCount = GameObject.FindObjectsOfType<CoinCollectScript>().Length;
        
        this.collectedCoins = Enumerable.Repeat(false, coinsCount).ToArray();
        this.playerAttributesData = new AttributesData();
    }
}
