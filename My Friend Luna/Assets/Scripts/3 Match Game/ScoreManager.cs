using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public Text scoreText;
    public int score;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Pontuação: " + score;
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
    }
}
