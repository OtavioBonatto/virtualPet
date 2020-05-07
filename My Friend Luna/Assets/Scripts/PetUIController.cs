using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUIController : MonoBehaviour {

    public static PetUIController instance;

    public Image foodBar, happinessBar, bathroomBar, energyBar;
    public Text weigthText, ageText, healthText, nameText;

    private void Awake() {
        instance = this;
    }

    public void UpdateImages(int food, int happiness, int bathroom, int energy) {
        if(foodBar != null) {
            foodBar.fillAmount = (float)food / 100;
            happinessBar.fillAmount = (float)happiness / 100;
            bathroomBar.fillAmount = (float)bathroom / 100;
            energyBar.fillAmount = (float)energy / 100;
        }
        
    }

    public void UpdateWeigth(float weigth, int age, string health, string name) {
        weigthText.text = "Peso: " + weigth.ToString("F2") + "Kg";
        ageText.text = "Idade: " + age.ToString() + " ano";
        healthText.text = "Saúde: " + health;
        nameText.text = "Nome: " + name;
    }
}
