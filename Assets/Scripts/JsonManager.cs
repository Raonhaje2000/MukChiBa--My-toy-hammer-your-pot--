using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// �÷��� ����� Json���� �����ϰ� �ҷ����� Ŭ����
public class JsonManager : MonoBehaviour
{
    public static JsonManager instance;

    const string Game_Data_Directory_Name = "GameData"; // ���� ���丮 �̸�
    const string Game_Data_File_Name = "ProjectM_Data"; // ���� ���� �̸�

    string directoryPath; // ���� ���丮 ���
    string filePath; // ���� ���� ���

    GameSaveData saveData; // ���� ������

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

        // ���丮�� ���� ��� ���丮 ����
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        filePath = Path.Combine(directoryPath, Game_Data_File_Name + ".json");
        saveData = new GameSaveData();

        LoadGameData(); // ���� ������ �ҷ�����
    }


    // ���� ������ �����ϱ�
    public void SaveGameData(GamePlayData playData)
    {
        // �÷��� ������ ����
        playData.dataIndex = saveData.saveCount;
        saveData.Save(playData);

        // ���� ������ �Ϻ� ���� (���ʿ��� �����͸� �������μ� ó���� ������� �� ����)
        //if (saveData.playDataList.Count + 1 > saveData.MaxData)
        //{
        //    List<GamePlayData> timeSort = saveData.playDataList.OrderBy(x => x.playTime).ToList<GamePlayData>();
        //    List<GamePlayData> winSort = saveData.playDataList.OrderBy(x => x.winningPercentage).ToList<GamePlayData>();

        //    // ������ ��Ұ� ����Ǵ� ��� ����

        //}

        // Ŭ������ Json���� ��ȯ
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonData);
    }

    // ���� ������ �ҷ�����
    public void LoadGameData()
    {
        if (File.Exists(filePath)) // ���� ������ �ִ� ���
        {
            // Json�� Ŭ������ ��ȯ
            string jsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<GameSaveData>(jsonData);
        }
    }
}
