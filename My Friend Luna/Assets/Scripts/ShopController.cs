using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public static ShopController instance;

    public string shopScene;
    public string petShopScene;
    public string playScene;
    public string playScene2;
    public string playScene3;
    public string mainScene;
    public string clinicScene;
    public string gameSelect;
    public string startScene;

    private void Awake() {
        instance = this;
        if (SceneManager.GetActiveScene().name == startScene && PlayerPrefs.HasKey("PetSelected") || SceneManager.GetActiveScene().name == startScene && PlayerPrefs.HasKey("CatSelected")) {
            SceneManager.LoadScene(mainScene);
        }
    }

    public void Start() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    IEnumerator DelaySceneLoad(string scene) {
        yield return new WaitForSeconds(AudioManager.instance.soundEffects[0].clip.length);

        PetController.instance.SavePet();
        SceneManager.LoadScene(scene);
    }

    public void GoToMain() {
        if(SceneManager.GetActiveScene().name == startScene) {
            SceneManager.LoadScene(mainScene);
        }        
    }

    public void GoToShop() {
        AudioManager.instance.PlaySFX(0);
        StartCoroutine(DelaySceneLoad(shopScene));
    }

    public void GoPlay() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(playScene));
    }

    public void GoPlay2() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(playScene2));
    }

    public void GoMemoryGame() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(playScene3));
    }

    public void GoToClinic() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(clinicScene));
    }

    public void GoToPetShop() {
        AudioManager.instance.PlaySFX(0);
        Debug.Log("petshop scene");

        StartCoroutine(DelaySceneLoad(petShopScene));
    }

    public void ClickSound() {
        AudioManager.instance.PlaySFX(0);
    }

    public void BackToMain() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(mainScene));
    }

    public void GameSelect() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(gameSelect));
    }
}
