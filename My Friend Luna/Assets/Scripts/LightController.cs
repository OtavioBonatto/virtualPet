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
