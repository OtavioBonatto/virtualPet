using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using Leguar.TotalJSON;

public class InventorySaver : MonoBehaviour {
    [SerializeField] private InventoryController inventory;
    [SerializeField] private ItemDatabase itemDB;

    public SerializableListString SL;

    private void OnDisable() {
        //clear the SL
        SL.serializableList.Clear();
        //build our save data from current game state
        BuildSaveData();
        //save out the save data
        SaveScriptables();
    }

    private void OnEnable() {
        inventory.Container.Clear();
        Debug.Log("items no inventario: " + inventory.Container.Count);

        SL.serializableList.Clear();
        LoadScriptables();
        ImportSaveData();
    }

    private void BuildSaveData() {
        for (int i = 0; i < inventory.Container.Count; i++) {
            SerializableListString.SerialItem SI = new SerializableListString.SerialItem();
            SI.name = inventory.Container[i].item.name;
            SI.count = inventory.Container[i].amount;

            SL.serializableList.Add(SI);
        }
    }

    public void SaveScriptables() {
        Debug.Log("salvo para: " + Application.persistentDataPath);

        string filepath = Application.persistentDataPath + "/newsave.json";
        StreamWriter sw = new StreamWriter(filepath);
        JSON jsonObject = JSON.Serialize(SL);
        string json = jsonObject.CreatePrettyString();
        sw.WriteLine(json);
        sw.Close();
    }

    public void LoadScriptables() {
        Debug.Log("IS: carregando de: " + Application.persistentDataPath);

        string filepath = Application.persistentDataPath + "/newsave.json";

        if(File.Exists(filepath)) {
            string json = File.ReadAllText(filepath);
            JSON jsonObject = JSON.ParseString(json);
            SL = jsonObject.Deserialize<SerializableListString>();
        }
    }

    private void ImportSaveData() {
        for (int i = 0; i < SL.serializableList.Count; i++) {
            string name = SL.serializableList[i].name;
            int count = SL.serializableList[i].count;

            ShopItem item = itemDB.GetItem(name);
            if(item) {
                inventory.AddItem(item, count);
                Debug.Log("Adicionado " + item.sprite + "Quantidade " + count + " ao invetário.");
            } else {
                Debug.LogError("item não encontrado: " + SL.serializableList[i].name);
            }
        }
    }
}
