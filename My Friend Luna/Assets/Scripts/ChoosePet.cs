using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePet : MonoBehaviour {

    public static ChoosePet instance;

    public Image loadingScreen;

    public GameObject[] pets;
    public GameObject[] cats;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        if(PlayerPrefs.HasKey("PetSelected")) {
            if (pets[PlayerPrefs.GetInt("PetSelected")]) {
                pets[PlayerPrefs.GetInt("PetSelected")].SetActive(true);
            }
        } else {
            cats[PlayerPrefs.GetInt("CatSelected")].SetActive(true);
        }
    }

    public void ChoosePetPrefab() {
        if(GetPetName.instance.petNameInput.text != "") {
            if (!PlayerPrefs.HasKey("PetSelected")) {
                loadingScreen.gameObject.SetActive(true);
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
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

     
    }

    public void ChooseCatPrefab() {
        if (GetPetName.instance.petNameInput.text != "") {
            if (!PlayerPrefs.HasKey("CatSelected")) {
                loadingScreen.gameObject.SetActive(true);
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
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
