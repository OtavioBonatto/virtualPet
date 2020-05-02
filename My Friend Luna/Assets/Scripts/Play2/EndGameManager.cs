using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        endGame = false;
        currentCounterValue = counterValue;
        counter.text = "" + currentCounterValue;
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
        counter.text = "" + currentCounterValue;
        if(currentCounterValue == 0) {
            endGame = true;
            theDeathScreen.gameObject.SetActive(true);
            currentCounterValue = 0;
            counter.text = "" + currentCounterValue;
        }
    }

    public void RestartGame() {
        theDeathScreen.gameObject.SetActive(false);
        endGame = false;
        Board.instance.RestartDots();        
        currentCounterValue = counterValue;
        counter.text = "" + currentCounterValue;
        timerSeconds = 1;
        ScoreManager.instance.score = 0;
    }
}
