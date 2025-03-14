using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// �÷��� ����� Json���� �����ϰ� �ҷ����� Ŭ����
public class JsonManager : MonoBehaviour
{
    static JsonManager instance;

    const string Game_Data_Directory_Name = "GameData"; // ���� ���丮 �̸�
    const string Game_Data_File_Name = "MukChiBa_Data"; // ���� ���� �̸�

    string directoryPath; // ���� ���丮 ���
    string filePath; // ���� ���� ���

    GameSaveData saveData; // ���� ������

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

            LoadGameDataFile(); // ���� ������ ���� �ҷ�����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ������ ���� �ҷ�����
    void LoadGameDataFile()
    {
        saveData = new GameSaveData();

        directoryPath = Path.Combine(Application.persistentDataPath, Game_Data_Directory_Name); // persistentDataPath�� �� ����Ǿ ���� dataPath�� �� ����� �ʱ�ȭ

        // ���丮�� ���� ��� ���丮 ����
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        filePath = Path.Combine(directoryPath, Game_Data_File_Name + ".json");

        LoadGameData(); // ���� ������ �ҷ�����
    }

    // ���� ������ �����ϱ�
    public void SaveGameData(GamePlayData playData)
    {
        // �÷��� ������ ����
        saveData.Save(playData);

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

    // ���� ������ �ʱ�ȭ
    public void ResetGameData()
    {
        saveData = new GameSaveData();

        // ResetMessageBox.cs�� ClickYesButton()���� �ش� ó���κ��� ��ٸ��� ���� �Ʒ��ִ� �ڵ� ������ �ȵǴ� ���װ� ����
        // -> �ش� �Լ��� saveData�� ���븸 �������Ƿ� ���� �ʱ�ȭ ó���κи� �ڷ�ƾ���� ����
        StartCoroutine(ResetFile());
    }

    // ���� ���� ����
    IEnumerator ResetFile()
    {
        // Ŭ������ Json���� ��ȯ
        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonData);

        yield break;
    }
}
