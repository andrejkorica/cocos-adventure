using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int MainMenuIndex = 0;
    public GameObject MainMenuPanel;
    public GameObject HUDPanel;

    public void Start() {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = MainMenuPanel.activeSelf ? 1 : 0;
            MainMenuPanel.SetActive(!MainMenuPanel.activeSelf);
            HUDPanel.SetActive(!HUDPanel.activeSelf);
        }
    }

    public void Continue() {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
    }

    public void Exit() {
        Time.timeScale = 1;

        MainMenu.LevelsCompleted = 0;
        // TODO: Treba unloadat scenu da ne ostane spremljen state
        SceneManager.LoadScene(MainMenuIndex);
    }
}
