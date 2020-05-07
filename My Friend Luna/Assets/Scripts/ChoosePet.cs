using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChoosePet : MonoBehaviour {

    public static ChoosePet instance;

    public GameObject[] pets;
    public GameObject[] cats;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        //ChoosePetPrefab();
        //ChooseCatPrefab();
        if(PlayerPrefs.HasKey("PetSelected")) {
            if (pets[PlayerPrefs.GetInt("PetSelected")]) {
                pets[PlayerPrefs.GetInt("PetSelected")].SetActive(true);
                Debug.Log("cachorro");
            }
        } else {
            cats[PlayerPrefs.GetInt("CatSelected")].SetActive(true);
            Debug.Log("gato");
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            //PlayerPrefs.DeleteKey("PetSelected");
            //PlayerPrefs.DeleteKey("CatSelected");
            PlayerPrefs.DeleteAll();
            Debug.Log("saves deletados.");
        }
    }

    public void ChoosePetPrefab() {
        if(!PlayerPrefs.HasKey("PetSelected")) {
            var pet = Random.Range(0, pets.Length);
            if (pets[pet]) {
                pets[pet].SetActive(true);
            }

            PetController.instance._name = GetPetName.instance.petName;

            PetController.instance.SavePet();

            PlayerPrefs.SetInt("PetSelected", pet);
        } else {
            if (pets[PlayerPrefs.GetInt("PetSelected")]) {
                pets[PlayerPrefs.GetInt("PetSelected")].SetActive(true);
            }
        }        
    }

    public void ChooseCatPrefab() {
        if (!PlayerPrefs.HasKey("CatSelected")) {
            var cat = Random.Range(0, cats.Length);
            if (cats[cat]) {
                cats[cat].SetActive(true);
            }

            PetController.instance._name = GetPetName.instance.petName;
            PetController.instance._weigth = 5;

            PetController.instance.SavePet();

            PlayerPrefs.SetInt("CatSelected", cat);
        } else {
            if (cats[PlayerPrefs.GetInt("CatSelected")]) {
                cats[PlayerPrefs.GetInt("CatSelected")].SetActive(true);
            }
        }
    }
}
