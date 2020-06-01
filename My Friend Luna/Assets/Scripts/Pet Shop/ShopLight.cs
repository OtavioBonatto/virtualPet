using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShopLight : MonoBehaviour {

    public Light2D lamp;

    private void Update() {
        if(GlobalLightController.instance.night == true) {
            this.gameObject.SetActive(true);
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
