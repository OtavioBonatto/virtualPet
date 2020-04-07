using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

    public string shopScene;
    public string playScene;
    public string mainScene;

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

    public void ClickSound() {
        AudioManager.instance.PlaySFX(0);
    }

    public void BackToMain() {
        AudioManager.instance.PlaySFX(0);

        StartCoroutine(DelaySceneLoad(mainScene));
    }
}
