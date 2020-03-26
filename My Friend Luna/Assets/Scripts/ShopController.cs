using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public string GameLevel;

    public void GoToShop() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(GameLevel);
    }

    public void BackToMain() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(GameLevel);
    }

    public void BuyFood() {
        Debug.Log("Comprou");
    }
}
