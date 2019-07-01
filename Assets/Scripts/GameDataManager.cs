using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;

public class GameDataManager : MonoBehaviour
{

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private GameObject images;
    
    public GameData GameData { get;  }
    // Start is called before the first frame update
    private void Awake()
    {
        Load();
    }

    public void AddImage(Vector3 localPosition, Sprite sprite)
    {
//        gameData.pictures.Add(new Picture(localPosition, sprite));
        Save();
    }

    private void Save()
    {
        var bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        var file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, gameData);
        file.Close();
        Debug.Log(Application.persistentDataPath);
    }

    private void Load() {
        Debug.Log(Application.persistentDataPath + "/savedGames.gd");
        if (!File.Exists(Application.persistentDataPath + "/savedGames.gd")) return;
        var bf = new BinaryFormatter();
        var file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
        gameData = (GameData)bf.Deserialize(file);
        file.Close();
    }

}
