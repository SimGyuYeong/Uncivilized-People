using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System;

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

public class DataManager : MonoBehaviour
{
    Dictionary<int, saveData> data = new Dictionary<int, saveData>();

    [SerializeField] private GameObject savemenuPanel;
    [SerializeField] List<GameObject> menu = new List<GameObject>();

    private int SLCheck = 1;

    private void Start()
    {
        LoadFromJson();
        SaveMenuPanelUpdate();
    }

    private void SaveMenuPanelUpdate()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                GameObject obj = menu[i].transform.GetChild(j).gameObject;
                Text text = obj.GetComponent<Text>();
                if (j == 1) text.text = data[i].playerName;
                else text.text = data[i].date;
            }
        }
    }

    public void SaveMenuPanelOpen(int num)
    {
        savemenuPanel.SetActive(true);
        SLCheck = num;
    }

    public void SaveOrLoad(int slot)
    {
        if (SLCheck == 1)
        {
            Debug.Log("Save Complete");
            data[slot].playerName = "�׽�Ʈ";
            data[slot].date = DateTime.Now.ToString("yyyy�� MM�� dd��\n HH:mm:ss");
            SaveToJson();
            SaveMenuPanelUpdate();
            savemenuPanel.SetActive(false);
        }
        else if (SLCheck == 2)
        {
            Debug.Log(data[slot].playerName);
            Debug.Log(data[slot].date);
        }
    }

    public void SaveToJson()
    {
        string saveData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Path.Combine(Application.dataPath, "saveData.json"), saveData);
    }

    public void LoadFromJson()
    {
        if (File.Exists(Path.Combine(Application.dataPath, "saveData.json"))){
            string saveData = File.ReadAllText(Path.Combine(Application.dataPath, "saveData.json"));
            data = JsonConvert.DeserializeObject<Dictionary<int, saveData>>(saveData);
        }
        else
        {
            for(int i = 0; i < 8; i++)
            {
                data[i] = new saveData("???", 0, " ");
            }
            SaveToJson();
        }
        
    }
}