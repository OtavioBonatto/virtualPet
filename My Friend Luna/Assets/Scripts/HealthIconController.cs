using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIconController : MonoBehaviour {

    public Sprite happyFace, normalFace, sadFace;
    private Image theImg;

    // Start is called before the first frame update
    void Start() {
        theImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if(PetController.instance._health == "Boa") {
            theImg.sprite = happyFace;
        } else if(PetController.instance._health == "Ruim") {
            theImg.sprite = sadFace;
        }
    }
}
