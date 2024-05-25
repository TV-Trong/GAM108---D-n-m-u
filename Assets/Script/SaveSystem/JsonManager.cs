using UnityEngine;
using System.IO;
using System;

public class JsonManager : Singleton<JsonManager>
{
    private string filePath;

    void Start()
    {
        filePath = @"Assets/savefile.json";
    }

    public void SaveData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        PlayerList dataList = new PlayerList();
        string json = JsonUtility.ToJson(dataList);
        File.WriteAllText(filePath, json);
        Debug.Log("Đã lưu dữ liệu thành công vào: " + filePath);
    }

    public PlayerList LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerList dataList = JsonUtility.FromJson<PlayerList>(json);
            Debug.Log("Lấy dữ liệu thành công!");
            return dataList;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy dữ liệu!");
            return null;
        }
    }
}

