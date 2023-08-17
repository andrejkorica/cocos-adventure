using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class SharkTimerScript : MonoBehaviour
{
    private static double seconds = 0;
    public static void SetSeconds(double newSeconds)
    {
        seconds = newSeconds;
    }

    void Update() {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        
        int secondsValue = timeSpan.Seconds;
        int millisecondsValue = timeSpan.Milliseconds;

        gameObject.GetComponent<TextMeshProUGUI>().text = secondsValue + "." + millisecondsValue;
    }
}
