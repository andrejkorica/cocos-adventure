using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour, IGlobalDataPersistance
{
    public bool isCollected;
    private SpriteRenderer spriteRenderer;
    private Light2D CompasLight;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        CompasLight = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            spriteRenderer.enabled = false;
            CompassController.hasCompass = true;
            CompasLight.enabled = false;

            foreach (Image image in CompassController.images)
            {
                image.enabled = true;
            }

        }
    }

    public void LoadData(AttributesData data)
    {
        isCollected = data.hasCompass;
        spriteRenderer.enabled = !data.hasCompass;
        CompasLight.enabled = !data.hasCompass; ;

    }

    public void SaveData(AttributesData data)
    {
        data.hasCompass = isCollected;
    }
}
