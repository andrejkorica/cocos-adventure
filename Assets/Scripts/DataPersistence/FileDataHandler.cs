using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string globalDirName = "global";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(int index) 
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, index.ToString(), dataFileName);
        Debug.Log(fullPath);
        if (!File.Exists(fullPath)) 
        {
            return new GameData();
        }

        string dataToLoad = "";
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                dataToLoad = reader.ReadToEnd();
            }
        }

        return JsonUtility.FromJson<GameData>(dataToLoad);    
    }
    public AttributesData LoadGlobalData() 
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, globalDirName, dataFileName);
        Debug.Log(fullPath);
        if (!File.Exists(fullPath)) 
        {
            return new AttributesData();
        }

        string dataToLoad = "";
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                dataToLoad = reader.ReadToEnd();
            }
        }

        return JsonUtility.FromJson<AttributesData>(dataToLoad);    
    }

    public void Save(GameData data, int index) 
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, index.ToString(), dataFileName);
        string dataToStore = JsonUtility.ToJson(data, true);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream)) 
            {
                writer.Write(dataToStore);
            }
        }
    }
    public void SaveGlobalData(AttributesData data) 
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, globalDirName, dataFileName);
        string dataToStore = JsonUtility.ToJson(data, true);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream)) 
            {
                writer.Write(dataToStore);
            }
        }
    }

    public void Delete(int index) 
    {
        string fullPath = Path.Combine(dataDirPath, index.ToString(), dataFileName);
        if (File.Exists(fullPath)) 
        {
            Directory.Delete(Path.GetDirectoryName(fullPath), true);
        }
    }

    public static void DeleteProgress() 
    {
        var di = new DirectoryInfo(Path.Combine(Application.persistentDataPath));

        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true); 
        }
    }
}
