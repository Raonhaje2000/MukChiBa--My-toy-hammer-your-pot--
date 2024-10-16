using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 플레이 기록을 관리하는 클래스
public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    const string Game_Data_File_Name = "ProjectM_Data"; // 저장 파일 이름
    const int Max_Data = 5;

    string filePath; // 저장 파일 경로

    GameSaveData saveData; // 저장 데이터

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/GameData/" + Game_Data_File_Name + ".json";
        saveData = new GameSaveData();

        LoadGameData(); // 게임 데이터 불러오기
    }


    // 게임 데이터 저장하기
    public void SaveGameData()
    {
        GamePlayData playData = GameManager.instance.PlayData;

        // GameData 클래스를 Json 형태로 변환
        //string jsonData = JsonUtility.ToJson(saveData, true);

        //File.WriteAllText(filePath, jsonData);
    }

    // 게임 데이터 불러오기
    public void LoadGameData()
    {
        if(File.Exists(filePath)) // 저장 파일이 있는 경우
        {
            string jsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<GameSaveData>(jsonData);
        }
    }
}
