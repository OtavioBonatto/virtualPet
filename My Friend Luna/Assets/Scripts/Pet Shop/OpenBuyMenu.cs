using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class OpenBuyMenu : MonoBehaviour {

    public Image buyMenu;
    public Image itemMenuImage;
    public Sprite bowlRed, bowlBlue, bowlGreen;
    public Sprite football, cricketBall, basketBall;
    public Button buyButton;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Red")) {
                BuyBowlMenu(bowlRed, 1);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Blue")) {
                BuyBowlMenu(bowlBlue, 2);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Green")) {
                BuyBowlMenu(bowlGreen, 3);
            }

            if(hit != false && hit.collider != null && hit.collider.CompareTag("Cricket Ball")) {
                BuyBalllMenu(cricketBall, 1);
            } else if(hit != false && hit.collider != null && hit.collider.CompareTag("Basket Ball")) {
                BuyBalllMenu(basketBall, 2);
            } else if(hit != false && hit.collider != null && hit.collider.CompareTag("Football")) {
                BuyBalllMenu(football, 3);
            }
        }        
    }

    public void BuyBowlMenu(Sprite bowl, int bowlNumber) {
        buyMenu.gameObject.SetActive(true);
        itemMenuImage.GetComponent<Image>().sprite = bowl;
        itemMenuImage.transform.position = new Vector3(itemMenuImage.transform.position.x, 1050, itemMenuImage.transform.position.z);
        itemMenuImage.transform.localScale = new Vector3(1, 1, 1);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyBowl(bowlNumber));
    }

    public void BuyBalllMenu(Sprite ball, int ballNumber) {
        buyMenu.gameObject.SetActive(true);
        itemMenuImage.GetComponent<Image>().sprite = ball;
        itemMenuImage.transform.position = new Vector3(itemMenuImage.transform.position.x, 1000, itemMenuImage.transform.position.z);
        itemMenuImage.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyBall(ballNumber));
    }

    public void BuyBowl(int bowlNumber) {
        AudioManager.instance.PlaySFX(1);
        PetController.instance.money -= 500;
        PlayerPrefs.SetInt("Bowl", bowlNumber);
        buyMenu.gameObject.SetActive(false);
    }

    public void BuyBall(int ballNumber) {
        AudioManager.instance.PlaySFX(1);
        PetController.instance.money -= 500;
        PlayerPrefs.SetInt("Ball", ballNumber);
        buyMenu.gameObject.SetActive(false);
    }

}
