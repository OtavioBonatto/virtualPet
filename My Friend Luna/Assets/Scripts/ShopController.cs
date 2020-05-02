using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public string shopScene;
    public string playScene;
    public string playScene2;
    public string mainScene;
    public string clinicScene;
    public string gameSelect;

    public void Start() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    IEnumerator DelaySceneLoad(string scene) {
        yield return new WaitForSeconds(AudioManager.instance.soundEffects[0].clip.length);

        PetController.instance.SavePet();
        SceneManager.LoadScene(scene);
    }

    public void GoToShop() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(shopScene));
    }

    public void GoPlay() {
        PetController.instance.Play();
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(playScene));
    }

    public void GoPlay2() {
        PetController.instance.Play();
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(playScene2));
    }

    public void GoToClinic() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(clinicScene));
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
