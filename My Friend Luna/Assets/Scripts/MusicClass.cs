using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour {
    
    public AudioClip mainMusic;
    public AudioClip gameSelectMusic;
    public AudioSource source;


    public static MusicClass instance;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
            return;
        }
    }

    void Start() {
        PlayMainMusic();
    }


    public void PlayMainMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.mainMusic;
                instance.source.Play();
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }


    public void PlayGameSelectMusic() {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.gameSelectMusic;
                instance.source.Play();
            }
        } else {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
}
