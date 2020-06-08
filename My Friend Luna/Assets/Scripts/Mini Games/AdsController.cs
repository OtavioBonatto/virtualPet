using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsController : MonoBehaviour {

    private readonly string video_id = "3637319";
    private float timer;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        Advertisement.Initialize(video_id, false);    
        if(PlayerPrefs.HasKey("AdTimer")) {
            timer = PlayerPrefs.GetFloat("AdTimer");
        } else {
            timer = 90f;
        }
    }

    private void Update() {
        timer -= Time.deltaTime;
        PlayerPrefs.SetFloat("AdTimer", timer);
        Debug.Log(timer);
        ShowAd();
    }

    public void ShowAd() {
        if (Advertisement.IsReady() && timer <= 0 && SceneManager.GetActiveScene().name == "Game Select") {
            Advertisement.Show();
            timer = 80f;
        }
    }
}
