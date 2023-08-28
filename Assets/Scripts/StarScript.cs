using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour, IDataPersistence
{
    private bool isCollected = false;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            spriteRenderer.enabled = false;
            StarDisplay.instance.UpdateDisplay(isCollected);
        }
    }

    public void LoadData(GameData data)
    {
        this.isCollected = data.collectedStar;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = !this.isCollected;
        StarDisplay.instance.UpdateDisplay(isCollected);
    }

    public void SaveData(GameData data)
    {
        data.collectedStar = this.isCollected;
    }
}
