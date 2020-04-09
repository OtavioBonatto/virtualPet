using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class LightController : MonoBehaviour {

    public static LightController instance;

    public Light2D lamp;
    public GameObject lampObj;
    public Toggle toggleLight;
    public Light2D globalLight;

    private void Awake() {

        instance = this;

        if (PlayerPrefs.GetInt("LampOn") == 1) {
            lamp.enabled = true;
            toggleLight.isOn = true;
        } else {
            lamp.enabled = false;
            toggleLight.isOn = false;
        }
    }

    private void Update() {
        if (!lampObj.activeSelf) {
            PlayerPrefs.SetInt("LampOn", 0);
            toggleLight.isOn = false;
        }

        if (lampObj.activeSelf) {
            PlayerPrefs.SetInt("LampOn", 1);
            toggleLight.isOn = true;
        }
    }

    public void DayNightCycle() {
        if(DateTime.Now.Hour >= 20 && DateTime.Now.Hour <= 6) {
            globalLight.intensity = 0.1f;
        } else {
            globalLight.intensity = 1;
        }
    }

    //private void OnApplicationQuit() {
    //    if(!lampObj.activeSelf) {
    //        PlayerPrefs.SetInt("LampOn", 0);
    //        toggleLight.isOn = false;
    //    } 

    //    if(lampObj.activeSelf) {
    //        PlayerPrefs.SetInt("LampOn", 1);
    //        toggleLight.isOn = true;
    //    }
    //}
}
