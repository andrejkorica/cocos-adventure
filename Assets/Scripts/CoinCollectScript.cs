using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class CoinCollectScript : MonoBehaviour, IDataPersistence
{
    private bool isCollected = false;
    private int currentIndex;
    private SpriteRenderer spriteRenderer;
    private Light2D coinLight;

    void Awake() {
        this.currentIndex = transform.GetSiblingIndex();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        
        
    }

    private void Start()
    {
        coinLight = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            spriteRenderer.enabled = false;
            coinLight.enabled = false;
            CoinCounter.instance.AddCoin();
        }
    }

    public void LoadData(GameData data) {
        if (this.currentIndex >= data.collectedCoins.Length) {
            return;
        }

        this.isCollected = data.collectedCoins[this.currentIndex];
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = !this.isCollected;

        coinLight = GetComponent<Light2D>();
        coinLight.enabled = !this.isCollected;
    }

    public void SaveData(GameData data) {
        if (this.currentIndex >= data.collectedCoins.Length) {
            return;
        }

        data.collectedCoins[this.currentIndex] = this.isCollected;
    }
}
