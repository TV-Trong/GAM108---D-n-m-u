using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
public class JsonManager : MonoBehaviour
{
    private string filePath, fileDir;

    void Start()
    {
        filePath = Application.persistentDataPath + "/Saves/" + "saveFile.sav";
        fileDir = Application.persistentDataPath + "/Saves/";
    }

    public void SaveData()
    {
        if (!Directory.Exists(fileDir))
        {
            //File.Delete(filePath);
            Directory.CreateDirectory(fileDir);
        }
        PlayerList dataList = new PlayerList();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        string json = JsonConvert.SerializeObject(dataList, settings);
        File.WriteAllText(filePath, json);
        Debug.Log("Đã lưu dữ liệu thành công vào: " + filePath);
    }

    
}

