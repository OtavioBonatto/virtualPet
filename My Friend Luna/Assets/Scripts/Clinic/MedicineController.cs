using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineController : MonoBehaviour {
    
    public void HealPet() {

        if(PetController.instance.money >= 1000) {
            PetController.instance.money -= 1000;
            PetController.instance._health = "Boa";
            PlayerPrefs.DeleteKey("sick");
            AudioManager.instance.PlaySFX(1);
        } else {
            AudioManager.instance.PlaySFX(2);
        }
    }
}
