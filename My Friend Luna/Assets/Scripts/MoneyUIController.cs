using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUIController : MonoBehaviour {

    public static MoneyUIController instance;

    public Text moneyText;

    private void Awake() {
        instance = this;
    }

    public void UpdateMoney(int money) {
        moneyText.text = "$: " + money;
    }
}
