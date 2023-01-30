using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerData playerData;
    private string persitentPath;

    private const string filename = "SavedData.json";

    private void Start()
    {
        SetPaths();
    }

    private void SetPaths()
    {
        persitentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + filename;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);

        StreamWriter writer = new StreamWriter(persitentPath);
        writer.Write(json);
    }

    public void LoadData()
    {
        StreamReader reader = new StreamReader(persitentPath);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
    }
}
