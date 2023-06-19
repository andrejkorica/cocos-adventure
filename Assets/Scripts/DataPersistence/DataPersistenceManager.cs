using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    private string fileName = "data";
    private int currentIndex;
    private GameData gameData;
    private AttributesData globalData;
    private List<IDataPersistence> dataPersistenceObjects;
    private IGlobalDataPersistance globalDataPersistenceObject;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (FindObjectsOfType<MonoBehaviour>(true).OfType<IGlobalDataPersistance>().Count() > 0) {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            this.globalDataPersistenceObject = FindObjectsOfType<MonoBehaviour>(true).OfType<IGlobalDataPersistance>().First();
            LoadGame();
        }    
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load(this.currentIndex);
        this.globalData = dataHandler.LoadGlobalData();
        CoinCounter.instance.SetCoins(gameData.collectedCoins.Count(c => c));

        if (this.gameData == null) 
        {
            this.gameData = new GameData();
        }

        if (this.globalData == null) 
        {
            this.globalData = new AttributesData();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
        globalDataPersistenceObject.LoadData(globalData);
    }

    public void SaveGame()
    {
        if (this.gameData == null) 
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        gameData.levelPassed = true;
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(gameData);
        }
        globalDataPersistenceObject.SaveData(globalData);

        dataHandler.Save(gameData, this.currentIndex);
        dataHandler.SaveGlobalData(globalData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
