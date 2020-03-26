using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject {

    public GameObject prefab;

    public Sprite sprite;
    public string cost;
    public int hungerRecover;
}
