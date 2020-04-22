using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightController : MonoBehaviour {

    public static GlobalLightController instance;

    public Light2D globalLight;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void DayNightCycle() {
        if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 6) {
            globalLight.intensity = 0.1f;
        } else {
            globalLight.intensity = 1;
        }
    }
}
