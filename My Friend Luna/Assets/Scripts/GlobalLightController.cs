using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightController : MonoBehaviour {

    public static GlobalLightController instance;

    public Light2D globalLight;
    public bool night;

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
            ChangeToNight();
        }
    }

    public void DayNightCycle() {
        if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 6) {
            globalLight.intensity = 0.1f;
            night = true;
        } else {
            globalLight.intensity = 1;
            night = false;
        }
    }

    public void ChangeToNight() {
        night = true;
        globalLight.intensity = 0.1f;
    }
}
