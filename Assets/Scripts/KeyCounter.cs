using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KeyCounter : MonoBehaviour
{

    private CollectingKeyAndChest collectingKeyAndChest;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = CollectingKeyAndChest.requiredKeys.ToString();
    }
}
