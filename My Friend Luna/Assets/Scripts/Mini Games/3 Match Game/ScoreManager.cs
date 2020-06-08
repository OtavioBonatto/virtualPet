using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;
    public int score;
    private int highscore;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("HighScore3Dot")) {
            highscore = PlayerPrefs.GetInt("HighScore3Dot");
        }
        highscoreText.text = "Best Score: " + highscore;
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Score: " + score;

        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetInt("HighScore3Dot", highscore);
        }

        highscoreText.text = "Best Score: " + highscore;
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
    }
}
