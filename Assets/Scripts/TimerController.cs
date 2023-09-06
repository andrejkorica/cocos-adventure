using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour, IDataPersistence
{
    private static int seconds = 0;
    private int bestTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddSecond", 1f, 1f);
    }

    private void AddSecond() {
        seconds++;
        var timeSpan = TimeSpan.FromSeconds(seconds);
        gameObject.GetComponent<TextMeshProUGUI>().text = timeSpan.ToString(@"mm\:ss");
    }

    public static string GetTimeString() {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return timeSpan.ToString(@"mm\:ss");
    }

    public void LoadData(GameData data) {

        seconds = 0;

        if (data.bestTime > -1) {
            bestTime = data.bestTime;
        }
    }

    public void SaveData(GameData data) {
        int bestTime = this.bestTime == -1
            ? seconds 
            : (int) Mathf.Min(seconds, this.bestTime);

        data.bestTime = bestTime;
        this.bestTime = bestTime;
    }
}
