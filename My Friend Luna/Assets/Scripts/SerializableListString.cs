using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableListString {

    public struct SerialItem {
        public string name;
        public int count;
    }

    //serializable object
    public List<SerialItem> serializableList = new List<SerialItem>();
}
