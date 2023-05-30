using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarDisplay : MonoBehaviour
{
    public static StarDisplay instance;
    private TextMeshProUGUI textMeshProUGUI;

    void Awake() {
        instance = this;
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDisplay(bool collected)
    {
        textMeshProUGUI.text = collected ? "Yes": "No";
    }
}
