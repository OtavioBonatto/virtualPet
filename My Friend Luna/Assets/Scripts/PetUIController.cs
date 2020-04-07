using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUIController : MonoBehaviour {

    public static PetUIController instance;

    public Image foodBar, happinessBar, bathroomBar, energyBar;
    public Text weigthText, ageText, healthText;

    private void Awake() {
        instance = this;
    }

    public void UpdateImages(int food, int happiness, int bathroom, int energy) {
        foodBar.fillAmount = (float) food / 100;
        happinessBar.fillAmount = (float) happiness / 100;
        bathroomBar.fillAmount = (float) bathroom / 100;
        energyBar.fillAmount = (float) energy / 100;
    }

    public void UpdateWeigth(float weigth) {
        weigthText.text = "Peso: " + weigth.ToString("F2");
    }
}
