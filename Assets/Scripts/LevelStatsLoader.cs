using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class LevelStatsLoader : MonoBehaviour
{
    private string fileName = "data";
    private FileDataHandler dataHandler;
    private GameData gameData;

    public TextMeshProUGUI coins;
    public TextMeshProUGUI bestTime;
    public GameObject stars;

    public int[] totalCoinsOnLevels;
    public int[] requiredTimesForStar;

    void Awake()
    {
        Debug.Log("Start");
        // Set coins and timer requirements for all levels
        this.totalCoinsOnLevels = new int[] { 34, 64, 51, 130, 69, 100 };
        this.requiredTimesForStar = new int[] { 45, 160, 140, 180, 200, 120 };

        // Load the stats from the specific level
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        int currentIndex = transform.GetSiblingIndex() + 1;
        this.gameData = dataHandler.Load(currentIndex);

        // Get coins and timer requirements for second and last star
        int coinReq = this.totalCoinsOnLevels[currentIndex - 1];
        int timerReq = this.requiredTimesForStar[currentIndex - 1];

        // Set coins text
        int coins = this.gameData.collectedCoins.Count(c => c);
        this.coins.text = coins.ToString();

        // Set time text
        int bestTime = gameData.bestTime;
        var timeSpan = TimeSpan.FromSeconds(bestTime);
        this.bestTime.text = bestTime != -1 ? timeSpan.ToString(@"mm\:ss") : "-";

        // Get 3 star images
        Image starCollectable = stars.transform.GetChild(0).GetComponent<Image>();
        Image starCoins = stars.transform.GetChild(1).GetComponent<Image>();
        Image starTimer = stars.transform.GetChild(2).GetComponent<Image>();

        // Display the status of those images (collected or not)
        SetImageOpacity(starCollectable, gameData.collectedStar);
        SetImageOpacity(starCoins, coinReq != -1 && coinReq == coins);
        SetImageOpacity(starTimer, timerReq != -1 && bestTime != -1 && bestTime <= timerReq);

        // Disable the button if level hasnt been passed
        transform.GetChild(0).GetComponent<Button>().interactable = gameData.levelPassed;
        if (gameData.levelPassed) {
            Invoke("IncLevels", .1f);
            ContinueButtonController.LevelsCompleted++;
        }
    }

    private void IncLevels() {
        Debug.Log("Inc scenes");
        MainMenu.LevelsCompleted++;
    }

    private void SetImageOpacity(Image image, bool full)
    {
        Color imageColor = image.color;
        imageColor.a = full ? 1f : .5f;
        image.color = imageColor;
    }
}
