using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public string shopScene;
    public string playScene;
    public string mainScene;

    public void GoToShop() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(shopScene);
    }


    public void GoPlay() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(playScene);
    }
    public void BackToMain() {
        PetController.instance.SavePet();
        Debug.Log("main scene");
        SceneManager.LoadScene(mainScene);
    }
}
