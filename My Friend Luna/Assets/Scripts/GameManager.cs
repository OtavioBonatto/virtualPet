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

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        platformStartPoint = platformGenerator.position;
        dogStartPoint = littleDog.transform.position;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void RestartGame() {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo() {
        PetController.instance.Play();
        littleDog.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++) {
            platformList[i].gameObject.SetActive(false);
        }

        littleDog.transform.position = dogStartPoint;
        platformGenerator.position = platformStartPoint;
        littleDog.gameObject.SetActive(true);
    }
}
