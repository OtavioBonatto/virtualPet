using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour {

    private static MusicClass instance;

    private AudioSource _audioSource;
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void PlayMusic() {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic() {
        _audioSource.Stop();
    }
}
