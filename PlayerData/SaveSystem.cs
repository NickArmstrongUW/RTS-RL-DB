using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Saved player data to: " + SavePath);
    }

    public static PlayerData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("Save file not found. Creating new PlayerData.");
            PlayerData newAccount = new PlayerData();
            newAccount.InitFreshAccount();
            Save(newAccount);
            return newAccount;
        }

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }

    public static void Delete()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
