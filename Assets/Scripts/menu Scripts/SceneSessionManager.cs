using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneSessionManager : MonoBehaviour
{
    public static SceneSessionManager Instance;
    public string PlayerName;
    public string BestPlayer;
    public int BestScore;
    public int PreviousScore;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadNameAndScore();
    }

    [System.Serializable]
    public class SaveDatas
    {
        public string PlayerName;
        public string BestPlayer;
        public int BestScore;
        public int PreviousScore;
    }

    public void SaveNameAndScore()
    {
        SaveDatas datas = new SaveDatas();
        datas.PlayerName = PlayerName;
        datas.BestPlayer = BestPlayer;
        datas.BestScore = BestScore;
        datas.PreviousScore = PreviousScore;
        string json = JsonUtility.ToJson(datas);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDatas datas = JsonUtility.FromJson<SaveDatas>(json);
            PlayerName = datas.PlayerName;
            BestPlayer = datas.BestPlayer;
            BestScore = datas.BestScore;
            PreviousScore = datas.PreviousScore;
        }
    }
    public void UpdateBestScore(int score)
    {
        PreviousScore = score;
        if(score > BestScore)
        {
            BestScore = score;
            BestPlayer = PlayerName;
        }
        
    }

}
