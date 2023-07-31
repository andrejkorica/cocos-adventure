using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static int LevelsCompleted = 0;
    public int FirstLevelIndex = 1;

    public void NewGame() {
        FileDataHandler.DeleteProgress();
        SceneManager.LoadScene(FirstLevelIndex);
    }

    public void Continue() {
        Debug.Log("Continue");
        Debug.Log("");
        SceneManager.LoadScene(LevelsCompleted < 6 ? LevelsCompleted + 1 : 6);
    }

    public void Options() {
        Debug.Log("Continue not implemented yet...");
        throw new System.Exception("Continue logic is not implemented yet");
    }

    public void LevelSelect(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
