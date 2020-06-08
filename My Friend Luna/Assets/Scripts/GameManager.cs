using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayPetController littleDog;
    private Vector3 dogStartPoint;

    private PlatformDestroyer[] platformList;

    public DeathMenu theDeathScreen;

    private void Awake() {
        instance = this;
    }
    void Start() {
        platformStartPoint = platformGenerator.position;
        dogStartPoint = littleDog.transform.position;

        theDeathScreen.gameObject.SetActive(false);
    }

    public void RestartGame() {

        littleDog.gameObject.SetActive(false);

        GameManager.instance.theDeathScreen.gameObject.SetActive(true);
    }

    public void Reset() {
        theDeathScreen.gameObject.SetActive(false);

        PetController.instance.Play();

        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++) {
            platformList[i].gameObject.SetActive(false);
        }

        littleDog.transform.position = dogStartPoint;
        platformGenerator.position = platformStartPoint;
        littleDog.gameObject.SetActive(true);
    }
}
