using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private int seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddSecond", 1f, 1f);
    }

    private void AddSecond() {
        seconds++;
        gameObject.GetComponent<TextMeshProUGUI>().text = seconds.ToString();
    }
}
