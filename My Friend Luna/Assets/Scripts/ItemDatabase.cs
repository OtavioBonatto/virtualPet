using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ItemDatabse")]
public class ItemDatabase : ScriptableObject {
    public List<ShopItem> items = new List<ShopItem>();

    public ShopItem GetItem(string iteName) {
        for (int i = 0; i < items.Count; i++) {
            if(items[i].name == iteName) {
                return items[i];
            }
        }

        return null;
    }
}
