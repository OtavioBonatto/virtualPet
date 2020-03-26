using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryController : ScriptableObject {

    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ShopItem _item, int _amount) {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++) {
            if(Container[i].item == _item) {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if(!hasItem) {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void RemoveItem(ShopItem _item) {
        for (int i = 0; i < Container.Count; i++) {
            if(Container[i].item == _item) {
                Container[i].RemoveAmount();
            }

            if(Container[i].amount < 0) {
                Container.Remove(Container[i]);
            }
        }
    }
    
}

[System.Serializable]
public class InventorySlot {
    public ShopItem item;
    public int amount;
    public InventorySlot(ShopItem _item, int _amount) {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }

    public void RemoveAmount() {
        amount--;
    }
}
