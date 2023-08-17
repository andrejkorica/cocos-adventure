using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int MainMenuIndex = 0;
    public GameObject MainMenuPanel;
    public GameObject GuidebookPanel;
    public GameObject HUDPanel;

    private bool IsMenuOpen = false;
    private bool IsGuidebookOpen = false;

    public void Start() {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // If guidebook is open close it
            if (IsGuidebookOpen) {
                Time.timeScale = 1;
                GuidebookPanel.SetActive(false);
                HUDPanel.SetActive(!HUDPanel.activeSelf);
                IsGuidebookOpen = false;
            }

            // If guidebook is closed toggle the menu
            else {
                Time.timeScale = MainMenuPanel.activeSelf ? 1 : 0;
                MainMenuPanel.SetActive(!MainMenuPanel.activeSelf);
                HUDPanel.SetActive(!HUDPanel.activeSelf);
                IsMenuOpen = !IsMenuOpen;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            // If main menu is open do nothing
            if (IsMenuOpen) {
                
            }

            // If main menu is closed toggle the guidebook
            else {
                Time.timeScale = GuidebookPanel.activeSelf ? 1 : 0;
                GuidebookPanel.SetActive(!GuidebookPanel.activeSelf);
                HUDPanel.SetActive(!HUDPanel.activeSelf);
                IsGuidebookOpen = !IsGuidebookOpen;
            }
        }
    }

    public void Continue() {
        Time.timeScale = 1;
        MainMenuPanel.SetActive(false);
        HUDPanel.SetActive(true);
        IsMenuOpen = false;
    }

    public void Retry() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() {
        Time.timeScale = 1;

        MainMenu.LevelsCompleted = 0;
        SceneManager.LoadScene(MainMenuIndex);
    }
}
