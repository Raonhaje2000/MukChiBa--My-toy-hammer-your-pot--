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
    const string Game_Data_File_Name = "MukChiBa_Data"; // 저장 파일 이름

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

        directoryPath = Path.Combine(Application.persistentDataPath, Game_Data_Directory_Name); // persistentDataPath는 앱 실행되어도 유지 dataPath는 앱 실행시 초기화

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

        // ResetMessageBox.cs의 ClickYesButton()에서 해당 처리부분을 기다리는 동안 아래있는 코드 실행이 안되는 버그가 있음
        // -> 해당 함수는 saveData의 내용만 가져오므로 파일 초기화 처리부분만 코루틴으로 돌림
        StartCoroutine(ResetFile());
    }

    // 파일 내용 리셋
    IEnumerator ResetFile()
    {
        // 클래스를 Json으로 변환
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonData);

        yield break;
    }
}
