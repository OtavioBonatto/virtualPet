using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static float gameHourTimer;
    public float hourLength;

    // Update is called once per frame
    void Update() {
        if(gameHourTimer <= 0) {
            gameHourTimer = hourLength;
        } else {
            gameHourTimer -= Time.deltaTime;
        }
    }
}
