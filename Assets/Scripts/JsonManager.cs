using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// 플레이 기록을 Json으로 저장하고 불러오는 클래스
public class JsonManager : MonoBehaviour
{
    static JsonManager instance;

    const string Game_Data_Directory_Name = "GameData"; // 저장 디렉토리 이름
    const string Game_Data_File_Name = "ProjectM_Data"; // 저장 파일 이름

    string directoryPath; // 저장 디렉토리 경로
    string filePath; // 저장 파일 경로

    GameSaveData saveData; // 저장 데이터

    public static JsonManager Instance
    {
        get { return instance; }
    }

    public GameSaveData SaveData
    {
        get { return saveData; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            LoadGameDataFile(); // 게임 데이터 파일 불러오기
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 게임 데이터 파일 불러오기
    void LoadGameDataFile()
    {
        saveData = new GameSaveData();

        directoryPath = Path.Combine(Application.dataPath, Game_Data_Directory_Name);

        // 디렉토리가 없는 경우 디렉토리 생성
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        filePath = Path.Combine(directoryPath, Game_Data_File_Name + ".json");

        LoadGameData(); // 게임 데이터 불러오기
    }

    // 게임 데이터 저장하기
    public void SaveGameData(GamePlayData playData)
    {
        // 플레이 데이터 저장
        saveData.Save(playData);

        // 클래스를 Json으로 변환
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonData);
    }

    // 게임 데이터 불러오기
    public void LoadGameData()
    {
        if (File.Exists(filePath)) // 저장 파일이 있는 경우
        {
            // Json을 클래스로 변환
            string jsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<GameSaveData>(jsonData);
        }
    }

    // 게임 데이터 초기화
    public void ResetGameData()
    {
        saveData = new GameSaveData();

        // 클래스를 Json으로 변환
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonData);
    }
}
