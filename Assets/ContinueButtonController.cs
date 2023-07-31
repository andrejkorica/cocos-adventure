using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonController : MonoBehaviour
{
    public static int LevelsCompleted = 0;
    public GameObject LevelStatLoaderGameObject;

    void Start() {
        LevelStatLoaderGameObject.SetActive(true);

        Debug.Log("Set active");
        // TODO Treba pokrenut manualno start na svakom childu 
    }
    void Update()
    {
        LevelStatLoaderGameObject.SetActive(false);
        if (LevelsCompleted > 0) {
            GetComponent<Button>().interactable = true;
        }
    }
}
