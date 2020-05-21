using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAccessories : MonoBehaviour {

    public static ChooseAccessories instance;

    public GameObject[] petAccessories;

    private void Awake() {
        instance = this;

        if(PlayerPrefs.GetInt("Bowl") == 1) {
            petAccessories[0].SetActive(true);
        } else if(PlayerPrefs.GetInt("Bowl") == 2) {
            petAccessories[1].SetActive(true);
        } else if (PlayerPrefs.GetInt("Bowl") == 3) {
            petAccessories[2].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Ball") == 1) {
            petAccessories[3].SetActive(true);
        } else if (PlayerPrefs.GetInt("Ball") == 2) {
            petAccessories[4].SetActive(true);
        } else if (PlayerPrefs.GetInt("Ball") == 3) {
            petAccessories[5].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Bed") == 1) {
            petAccessories[6].SetActive(true);
        } else if (PlayerPrefs.GetInt("Bed") == 2) {
            petAccessories[7].SetActive(true);
        } else if (PlayerPrefs.GetInt("Bed") == 3) {
            petAccessories[8].SetActive(true);
        }
    }
}
