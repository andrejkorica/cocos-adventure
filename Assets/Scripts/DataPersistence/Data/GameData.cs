using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class GameData
{
    public bool[] collectedCoins;
    
    // Ovo se svaki put reseta, ne bi trebalo biti ovisno o levelu
    public AttributesData playerAttributesData;

    public GameData() 
    {
        // int coinsCount = GameObject.FindObjectsOfType<CoinCollectScript>().Length;
        int coinsCount = 10;

        this.collectedCoins = Enumerable.Repeat(false, coinsCount).ToArray();
        this.playerAttributesData = new AttributesData();
    }
}
