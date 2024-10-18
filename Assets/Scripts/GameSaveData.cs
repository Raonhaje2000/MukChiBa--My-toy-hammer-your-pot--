using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    const int MAX_DATA = 10; // ����Ǵ� ������ �ִ�ġ (�����Ͱ� ���� ���� ó���� ���� �ɸ��� �� ������)

    public int saveCount;                   // ����� Ƚ��
    public List<GamePlayData> playDataList; // ����� ���� ������

    public int MaxData
    {
        get { return MAX_DATA; }
    }

    public GameSaveData()
    {
        saveCount = 0;
        playDataList = new List<GamePlayData>();
    }

    public void Save(GamePlayData data)
    {
        saveCount++;
        playDataList.Add(data);
    }
}
