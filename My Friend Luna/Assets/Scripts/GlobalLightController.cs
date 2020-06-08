using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightController : MonoBehaviour {

    public static GlobalLightController instance;

    public Light2D globalLight;
    public bool night;
    public SpriteRenderer nightBackground;
    public SpriteRenderer dayBackground;
    public GameObject stars;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        DayNightCycle();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            if(night == true) {
                ChangeToDay();
            } else {
                ChangeToNight();
            }
        }
    }

    public void DayNightCycle() {
        if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 6) {
            ChangeToNight();
        } else {
            ChangeToDay();
        }
    }

    public void ChangeToNight() {
        night = true;
        globalLight.intensity = 0.20f;
        nightBackground.gameObject.SetActive(true);
        dayBackground.gameObject.SetActive(false);
        stars.SetActive(true);
    }

    public void ChangeToDay() {
        night = false;
        globalLight.intensity = 1;
        dayBackground.gameObject.SetActive(true);
        nightBackground.gameObject.SetActive(false);
        stars.SetActive(false);
    }
}
