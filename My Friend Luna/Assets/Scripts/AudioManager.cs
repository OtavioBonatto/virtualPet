using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioSource[] soundEffects;
    public AudioSource[] destroyDotSound;

    private void Awake() {
        instance = this;
    }

    public void PlaySFX(int soundToPlay) {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].Play();
    }

    public void PlayRandomDestroySound() {
        int soundToPlay = Random.Range(0, destroyDotSound.Length);
        destroyDotSound[soundToPlay].Stop();
        destroyDotSound[soundToPlay].Play();
    }
}
