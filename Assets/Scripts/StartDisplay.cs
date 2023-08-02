using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class StartDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool collected = StarDisplay.IsStarCollected();
        SetImageOpacity(GetComponent<Image>(), collected);
    }
    
    private void SetImageOpacity(Image image, bool full)
    {
        Color imageColor = image.color;
        imageColor.a = full ? 1f : .2f;
        image.color = imageColor;
    }
}
