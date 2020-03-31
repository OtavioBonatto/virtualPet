using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour {

    public int coinsToGive;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            PetController.instance.money += coinsToGive;
            gameObject.SetActive(false);
        }
    }
}
