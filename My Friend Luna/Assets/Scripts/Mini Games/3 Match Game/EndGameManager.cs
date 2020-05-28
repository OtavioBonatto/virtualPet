using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {

    public static EndGameManager instance;

    public GameObject timeLabel;
    public Text counter;
    public DeathMenu theDeathScreen;

    public int counterValue;
    public int currentCounterValue;
    private float timerSeconds;
    public bool endGame;

    public Text pointsText;
    public Text matchScore;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        endGame = false;
        currentCounterValue = counterValue;
        counter.text = "00:" + currentCounterValue;
        timerSeconds = 1;
    }

    // Update is called once per frame
    void Update() {
        if(currentCounterValue > 0) {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0) {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }

    public void DecreaseCounterValue() {
        currentCounterValue--;
        counter.text = "00:" + currentCounterValue;
        if(currentCounterValue == 0) {
            endGame = true;
            theDeathScreen.gameObject.SetActive(true);
            if(GameController.instance != null) {
                pointsText.text = "Você ganhou: " + GameController.instance.memoryGameScore + "$";
            }

            if(ScoreManager.instance != null) {
                Debug.Log(ScoreManager.instance.score);
                matchScore.text = "Você ganhou: " + ScoreManager.instance.score + "$";
            }            
            
            currentCounterValue = 0;
            counter.text = "00:" + currentCounterValue;
        }
    }

    public void RestartGame() {
        theDeathScreen.gameObject.SetActive(false);
        endGame = false;
        Board.instance.RestartDots();        
        currentCounterValue = counterValue;
        counter.text = "00:" + currentCounterValue;
        timerSeconds = 1;
        ScoreManager.instance.score = 0;
    }

    public void RestartMemoryGame() {
        //theDeathScreen.gameObject.SetActive(false);
        //currentCounterValue = counterValue;
        //counter.text = "" + currentCounterValue;
        //timerSeconds = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
