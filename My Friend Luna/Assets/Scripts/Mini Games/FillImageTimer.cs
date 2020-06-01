using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillImageTimer : MonoBehaviour {

    public static FillImageTimer instance;

    public Image loading;
    public Text timeText;
    public int minutes;
    public int sec;
    public int totalSeconds = 00;
    int TOTAL_SECONDS = 00;

    private void Awake() {
        instance = this;
    }

    void Start() {
        Init();
    }

    void Update() {
        if (sec == 0 && minutes == 0) {
            //timeText.text = "Time's Up!";
            StopCoroutine(second());
        }
    }
    IEnumerator second() {
        yield return new WaitForSeconds(1f);
        if (sec > 0)
            sec--;
        if (sec == 0 && minutes != 0) {
            sec = 60;
            minutes--;
        }
        timeText.text = minutes + ":" + sec;
        fillLoading();
        StartCoroutine(second());
    }

    public void Init() {
        timeText.text = minutes + ":" + sec;
        if (minutes > 0)
            totalSeconds += minutes * 60;
        if (sec > 0)
            totalSeconds += sec;
        TOTAL_SECONDS = totalSeconds;
        StartCoroutine(second());
    }

    void fillLoading() {
        totalSeconds--;
        float fill = (float)totalSeconds / TOTAL_SECONDS;
        loading.fillAmount = fill;
    }
}
