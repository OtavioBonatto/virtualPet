using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineController : MonoBehaviour {
    
    public void HealPet() {

        if(PetController.instance.money >= 1000) {
            PetController.instance.money -= 1000;
            PetController.instance._health = "Boa";
        }
    }
}
