using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {

    public GameObject foodInventory;

    public void ButtonToggle() {
        if (foodInventory.active) {
            foodInventory.SetActive(false);
        } else {
            foodInventory.SetActive(true);
        }
    }
}
