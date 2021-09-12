using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;
    public string curPlayerName;
    public string playerName;
    public int oldHighScore;
    public int highScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        loadHighScore();
    }

    public class HighScore
    {
        public int highScore;
        public string playerName;
    }

    public void saveHighScore()
    {
        if (highScore > oldHighScore)
        {
            HighScore data = new HighScore();
            data.highScore = highScore;
            data.playerName = curPlayerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
        }
    }

    public void loadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            highScore = data.highScore;
            oldHighScore = highScore;
            playerName = data.playerName;
        }
    }
}
