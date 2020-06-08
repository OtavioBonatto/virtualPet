using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayInventory : MonoBehaviour {

    public InventoryController inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    [Header("References")]
    [SerializeField] private Transform itemsContainer;

    // Start is called before the first frame update
    void Start() {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update() {
        UpdateDisplay();
    }

    public void UpdateDisplay() {
        for (int i = 0; i < inventory.Container.Count; i++) {

            InventorySlot slot = inventory.Container[i];

            if(itemsDisplayed.ContainsKey(slot)) {
                if (itemsDisplayed[slot]) {
                    itemsDisplayed[slot].gameObject.transform.GetChild(2).GetComponent<Text>().text = "Num: " + slot.amount;

                    var children = new List<GameObject>();
                    foreach (Transform child in itemsContainer) children.Add(child.gameObject);
                    children.ForEach((child) => {
                        string amountText = child.transform.GetChild(2).GetComponent<Text>().text;
                        if (amountText == "Num: 0") {
                            Debug.Log("destruir");
                            Destroy(child);
                        }
                    });
                }
            } 
        }
    }

    public void CreateDisplay() {
        for (int i = 0; i < inventory.Container.Count; i++) {

            InventorySlot slot = inventory.Container[i];
            ShopItem si = slot.item;
            GameObject itemObject = Instantiate(slot.item.prefab, itemsContainer);

            itemObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                OnButtonClick(si);
            });
            itemObject.transform.GetChild(2).GetComponent<Text>().text = "Num " + slot.amount;

            itemsDisplayed.Add(slot, itemObject);
        }
    }

    private void OnButtonClick(ShopItem item) {
        if(PetController.instance._hunger < 90) {
            AudioManager.instance.PlaySFX(1);
            PetController.instance.Eat(item.hungerRecover);
            PetController.instance._weigth += item.hungerRecover * 0.005f;
            PetController.instance.inventory.RemoveItem(item);
        } else {
            if(PlayerPrefs.HasKey("PetSelected")) {
                AudioManager.instance.PlaySFX(6);
            } else {
                AudioManager.instance.PlaySFX(7);
            }        
        }      
    }
}
