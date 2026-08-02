using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInfo(Inventario inventario)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(inventario);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadInfo()
    {
        string path = Application.persistentDataPath + "/save.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
