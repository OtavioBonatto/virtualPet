using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

    public string gameSelect;

    public void RestartGame() {
        GameManager.instance.Reset();
    }

    public void QuitGame() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(gameSelect);
    }
}
