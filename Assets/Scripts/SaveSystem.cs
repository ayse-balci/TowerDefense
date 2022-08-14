using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SaveGameState(GameState gameState)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/towerdefense.game";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameState);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameState()
    {
        string path = Application.persistentDataPath + "/towerdefense.game";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError(("Save file not found in " + path));
            return null;
        }
    }
}
