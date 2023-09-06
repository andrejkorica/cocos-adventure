using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TotalCoinsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var totalCoinsOnLevels = new int[] { 35, 50, 51, 147, 69, 100 };
        var currentIndex = SceneManager.GetActiveScene().buildIndex - 1;
        gameObject.GetComponent<TextMeshProUGUI>().text = totalCoinsOnLevels[currentIndex].ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
