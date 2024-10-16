using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// �÷��� ����� �����ϴ� Ŭ����
public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    const string Game_Data_File_Name = "ProjectM_Data"; // ���� ���� �̸�
    const int Max_Data = 5;

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
        filePath = Application.persistentDataPath + "/GameData/" + Game_Data_File_Name + ".json";
        saveData = new GameSaveData();

        LoadGameData(); // ���� ������ �ҷ�����
    }


    // ���� ������ �����ϱ�
    public void SaveGameData()
    {
        GamePlayData playData = GameManager.instance.PlayData;

        // GameData Ŭ������ Json ���·� ��ȯ
        //string jsonData = JsonUtility.ToJson(saveData, true);

        //File.WriteAllText(filePath, jsonData);
    }

    // ���� ������ �ҷ�����
    public void LoadGameData()
    {
        if(File.Exists(filePath)) // ���� ������ �ִ� ���
        {
            string jsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<GameSaveData>(jsonData);
        }
    }
}
