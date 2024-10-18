using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// 플레이 기록을 Json으로 저장하고 불러오는 클래스
public class JsonManager : MonoBehaviour
{
    public static JsonManager instance;

    const string Game_Data_Directory_Name = "GameData"; // 저장 디렉토리 이름
    const string Game_Data_File_Name = "ProjectM_Data"; // 저장 파일 이름

    string directoryPath; // 저장 디렉토리 경로
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
        directoryPath = Path.Combine(Application.dataPath, Game_Data_Directory_Name);

        // 디렉토리가 없는 경우 디렉토리 생성
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        filePath = Path.Combine(directoryPath, Game_Data_File_Name + ".json");
        saveData = new GameSaveData();

        LoadGameData(); // 게임 데이터 불러오기
    }


    // 게임 데이터 저장하기
    public void SaveGameData(GamePlayData playData)
    {
        // 플레이 데이터 저장
        playData.dataIndex = saveData.saveCount;
        saveData.Save(playData);

        // 기존 데이터 일부 삭제 (불필요한 데이터를 지움으로서 처리가 길어지는 것 방지)
        //if (saveData.playDataList.Count + 1 > saveData.MaxData)
        //{
        //    List<GamePlayData> timeSort = saveData.playDataList.OrderBy(x => x.playTime).ToList<GamePlayData>();
        //    List<GamePlayData> winSort = saveData.playDataList.OrderBy(x => x.winningPercentage).ToList<GamePlayData>();

        //    // 최하위 요소가 공통되는 경우 삭제

        //}

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
}
