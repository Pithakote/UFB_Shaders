using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static string directory = "SaveData";
    public static string fileName = "PlayerSave.UFB";

    public static void Save(SaveObject so)
    {
        if (!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);

        // create a new binary formatter
        BinaryFormatter bf = new BinaryFormatter();
        // create the filestream which will create a new file at the full path directory, if it already exists it will overwrite it.
        FileStream file = File.Create(GetFullPath());

        bf.Serialize(file, so);
        file.Close();
    }

    public static SaveObject Load()
    {
        if(SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFullPath(), FileMode.Open);
                SaveObject so = (SaveObject)bf.Deserialize(file);
                file.Close();

                return so;
            }
            catch(SerializationException)
            {
                Debug.Log("Failed to load file - Have you been tampering?");
                // tell the player off here

            }
        }
        return null;
    }

    public static bool SaveExists()
    {
        return File.Exists(GetFullPath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }

    private static string GetFullPath()
    {
        // regardless of the platform, persistent data path will always exist 
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }
}
