using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // This variables are persistent between scenes
    public string userName;
    public string userWithHighScore;
    public int highScore;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
    }

    // Implement data persistence between sessions
    [System.Serializable]
    class SaveData
    {
        public string userWithHighScore;
        public int highScore;
    }

    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.userWithHighScore = userWithHighScore;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userWithHighScore = data.userWithHighScore;
            highScore = data.highScore;
        }
    }
}
