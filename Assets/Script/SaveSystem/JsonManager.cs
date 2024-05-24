using UnityEngine;
using System.IO;
using System;

public class JsonManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = @"Assets/savefile.json";
    }

    public void SaveData(PlayerFile data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("Đã lưu dữ liệu thành công vào: " + filePath);
    }

    public PlayerFile LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerFile data = JsonUtility.FromJson<PlayerFile>(json);
            Debug.Log("Lấy dữ liệu thành công!");
            return data;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy dữ liệu!");
            return null;
        }
    }
}

