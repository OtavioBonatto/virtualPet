using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyHeartsController : MonoBehaviour {

    public static FriendlyHeartsController instance;

    public Image[] hearts;
    public Sprite heart, halfHeart, emptyHeart;
    public int heartsNumber = 0;

    private void Awake() {
        instance = this;
    }

    void Start() {

        if (PlayerPrefs.HasKey("Bowl")) {
            heartsNumber++;
        }
        if (PlayerPrefs.HasKey("Ball")) {
            heartsNumber++;
        }
        if (PlayerPrefs.HasKey("Bed")) {
            heartsNumber++;
        }

        RefreshHearts();
    }

    public void RefreshHearts() {
        switch (heartsNumber) {
            case 0:
                hearts[0].sprite = emptyHeart;
                hearts[1].sprite = emptyHeart;
                hearts[2].sprite = emptyHeart;
                break;

            case 1:
                hearts[0].sprite = halfHeart;
                hearts[1].sprite = emptyHeart;
                hearts[2].sprite = emptyHeart;
                break;

            case 2:
                hearts[0].sprite = heart;
                hearts[1].sprite = emptyHeart;
                hearts[2].sprite = emptyHeart;
                break;

            case 3:
                hearts[0].sprite = heart;
                hearts[1].sprite = halfHeart;
                hearts[2].sprite = emptyHeart;
                break;

            case 4:
                hearts[0].sprite = heart;
                hearts[1].sprite = heart;
                hearts[2].sprite = emptyHeart;
                break;

            case 5:
                hearts[0].sprite = heart;
                hearts[1].sprite = heart;
                hearts[2].sprite = halfHeart;
                break;

            case 6:
                hearts[0].sprite = heart;
                hearts[1].sprite = heart;
                hearts[2].sprite = heart;
                break;
        }
    }
}
