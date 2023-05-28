using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance; 
    private int CoinCount = 0;
    private TextMeshProUGUI textMeshProUGUI;

    void Awake() {
        instance = this;
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateCoinText() {
        textMeshProUGUI.text = CoinCount.ToString();
    }

    public void AddCoin()
    {
        CoinCount++;
        UpdateCoinText();
    }

    public void SetCoins(int coins)
    {
        CoinCount = coins;
        UpdateCoinText();
    }
}
