using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCoins : MonoBehaviour {

    public int coinsToGive;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            AudioManager.instance.PlaySFX(1);
            PetController.instance.money += coinsToGive;
            PlayPetController.instance.score += coinsToGive;
            PlayPetController.instance.scoreText.text = "Pontuação: " + PlayPetController.instance.score;
            gameObject.SetActive(false);
        }
    }
}
