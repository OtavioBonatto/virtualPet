using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorRandom : MonoBehaviour {
    float timeLeft;
    Color targetColor;
    private Text theTxt;

    private void Start() {
        theTxt = GetComponent<Text>();
    }

    void Update() {
        if (timeLeft <= Time.deltaTime) {
            theTxt.color = targetColor;

            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 1.0f;
        } else {
            theTxt.color = Color.Lerp(theTxt.color, targetColor, Time.deltaTime / timeLeft);                        
            timeLeft -= Time.deltaTime;
        }
    }
}
