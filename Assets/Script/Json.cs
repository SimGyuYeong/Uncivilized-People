using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

class saveData
{
    public string playerName;
    public int id;
    public string date;

    public saveData(string name, int id, string date)
    {
        playerName = name;
        this.id = id;
        this.date = date;
    }
}

public class Json : MonoBehaviour
{
    Dictionary<int, saveData> data = new Dictionary<int, saveData>();

    [SerializeField]
    private Image image;

    private void Start()
    {
        LoadFromJson();
    }

    public void SaveToJson()
    {
        string saveData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Path.Combine(Application.dataPath, "saveData.json"), saveData);
    }

    public void LoadFromJson()
    {
        string saveData = File.ReadAllText(Path.Combine(Application.dataPath, "saveData.json"));
        data = JsonConvert.DeserializeObject <Dictionary<int, saveData>> (saveData);
    }
}