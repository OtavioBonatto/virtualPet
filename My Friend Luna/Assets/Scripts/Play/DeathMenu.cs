using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;

    public void RestartGame() {
        GameManager.instance.Reset();
    }

    public void QuitGame() {
        PetController.instance.SavePet();
        SceneManager.LoadScene(mainMenuLevel);
    }
}
