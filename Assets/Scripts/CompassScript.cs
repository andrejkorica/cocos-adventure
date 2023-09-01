using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour, IGlobalDataPersistance
{
    public bool isCollected;
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            spriteRenderer.enabled = false;
            CompassController.hasCompass = true;
            CompassController.image.enabled = true;

        }
    }

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void LoadData(AttributesData data)
    {
        isCollected = data.hasCompass;
    }

    public void SaveData(AttributesData data)
    {
    }
}
