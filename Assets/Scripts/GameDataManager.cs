using UnityEngine;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour
{

    [SerializeField]
    public GameData gameData;

        // Start is called before the first frame update
    private void Awake()
    {
        Load();
    }
    

    public void Save(Portal portal)
    {
          Debug.Log(JsonUtility.ToJson(gameData));
          gameData.portals.Add(portal);
          Debug.Log(JsonUtility.ToJson(gameData));

          //Save portal on API

//        var bf = new BinaryFormatter();
//        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
//        var file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
//        bf.Serialize(file, gameData);
//        file.Close();
//        Debug.Log(Application.persistentDataPath);
    }

    public void Load()
    {
        gameData = new GameData();
        //Recover data from API
        Debug.Log(JsonUtility.ToJson(gameData));
        
//        Debug.Log(Application.persistentDataPath + "/savedGames.gd");
//        if (!File.Exists(Application.persistentDataPath + "/savedGames.gd")) return;
//        var bf = new BinaryFormatter();
//        var file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
//        gameData = (GameData)bf.Deserialize(file);
//        file.Close();
    }

}
