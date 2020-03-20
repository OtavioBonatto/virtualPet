using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public string GameLevel;

    public void GoToShop() {
        SceneManager.LoadScene(GameLevel);
        PetController.instance.SavePet();
    }

    public void BackToMain() {
        SceneManager.LoadScene(GameLevel);
        PetController.instance.SavePet();
    }

    public void BuyFood() {
        Debug.Log("Comprou");
    }
}
