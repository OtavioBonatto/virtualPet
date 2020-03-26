using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    [Header("List of items sold")]
    [SerializeField] public ShopItem[] shopItem;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
        
    // Start is called before the first frame update
    void Start() {
        PopulateShop();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void PopulateShop() {
        for (int i = 0; i < shopItem.Length; i++) {
            ShopItem si = shopItem[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.transform.GetChild(0).GetComponent<Image>().sprite = si.sprite;
            itemObject.transform.GetChild(1).GetComponent<Text>().text = "$:" + si.cost;
            itemObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si));
        }
    }

    private void OnButtonClick(ShopItem item) {
        PetController.instance.inventory.AddItem(item, 1);
        PetController.instance.money -= int.Parse(item.cost);
        PetController.instance.UpdateMoney();
    }
}
