using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var cocoTransform = player.transform.Find("Coco");
        var coco = cocoTransform.gameObject;

        if (coco != null)
        {
            Debug.Log("DANGER SPIKES!");
        }
    }
}
