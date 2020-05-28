using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShopLight : MonoBehaviour {

    public Light2D lamp;

    private void Start() {
        if(GlobalLightController.instance.night == false) {
            lamp.enabled = false;
        } else {
            lamp.enabled = true;
        }
    }


}
