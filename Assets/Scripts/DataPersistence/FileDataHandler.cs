using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
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

    public void Delete(int index) 
    {
        string fullPath = Path.Combine(dataDirPath, index.ToString(), dataFileName);
        if (File.Exists(fullPath)) 
        {
            Directory.Delete(Path.GetDirectoryName(fullPath), true);
        }
    }

    public Dictionary<string, GameData> LoadAllLevels() 
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // Loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos) 
        {
            string index = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, index, dataFileName);
            if (!File.Exists(fullPath))
            {
                continue;
            }

            GameData profileData = Load(int.Parse(index));
            if (profileData != null) 
            {
                profileDictionary.Add(index, profileData);
            }
        }

        return profileDictionary;
    }
}
