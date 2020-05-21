using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OpenBuyMenu : MonoBehaviour {

    public Text priceText;
    public Image buyMenu;
    public Image itemMenuImage;
    public Sprite bowlRed, bowlBlue, bowlGreen;
    public Sprite football, cricketBall, basketBall;
    public Sprite bed, bedPurple, bedRed;
    public Button buyButton;
    private bool menuActive = false;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Red") && menuActive == false) {
                BuyBowlMenu(bowlRed, 1);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Blue") && menuActive == false) {
                BuyBowlMenu(bowlBlue, 2);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl Green") && menuActive == false) {
                BuyBowlMenu(bowlGreen, 3);
            }

            if(hit != false && hit.collider != null && hit.collider.CompareTag("Cricket Ball") && menuActive == false) {
                BuyBalllMenu(cricketBall, 1);
            } else if(hit != false && hit.collider != null && hit.collider.CompareTag("Basket Ball") && menuActive == false) {
                BuyBalllMenu(basketBall, 2);
            } else if(hit != false && hit.collider != null && hit.collider.CompareTag("Football") && menuActive == false) {
                BuyBalllMenu(football, 3);
            }

            if (hit != false && hit.collider != null && hit.collider.CompareTag("Bed") && menuActive == false) {
                BuyBedMenu(bed, 1);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bed Purple") && menuActive == false) {
                BuyBedMenu(bedPurple, 2);
            } else if (hit != false && hit.collider != null && hit.collider.CompareTag("Bed Red") && menuActive == false) {
                BuyBedMenu(bedRed, 3);
            }
        }        
    }

    public void BuyBowlMenu(Sprite bowl, int bowlNumber) {
        menuActive = true;
        buyMenu.gameObject.SetActive(true);
        itemMenuImage.GetComponent<Image>().sprite = bowl;
        priceText.text = 500 + "$";
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyBowl(bowlNumber));
    }

    public void BuyBalllMenu(Sprite ball, int ballNumber) {
        menuActive = true;
        buyMenu.gameObject.SetActive(true);
        itemMenuImage.GetComponent<Image>().sprite = ball;
        priceText.text = 250 + "$";
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyBall(ballNumber));
    }

    public void BuyBedMenu(Sprite bed, int bedNumber) {
        menuActive = true;
        buyMenu.gameObject.SetActive(true);
        itemMenuImage.GetComponent<Image>().sprite = bed;
        priceText.text = 1000 + "$";
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyBed(bedNumber));
    }

    public void BuyBowl(int bowlNumber) {        
        if(PetController.instance.money >= 500) {
            AudioManager.instance.PlaySFX(1);
            PetController.instance.money -= 500;
            PlayerPrefs.SetInt("Bowl", bowlNumber);
            buyMenu.gameObject.SetActive(false);
            DisableMenu();
        }        
    }

    public void BuyBall(int ballNumber) {        
        if(PetController.instance.money >= 250) {
            AudioManager.instance.PlaySFX(1);
            PetController.instance.money -= 250;
            PlayerPrefs.SetInt("Ball", ballNumber);
            buyMenu.gameObject.SetActive(false);
            DisableMenu();
        }        
    }

    public void BuyBed(int bedNumber) {        
        if (PetController.instance.money >= 1000) {
            AudioManager.instance.PlaySFX(1);
            PetController.instance.money -= 1000;
            PlayerPrefs.SetInt("Bed", bedNumber);
            buyMenu.gameObject.SetActive(false);
            DisableMenu();
        }
    }


    public void DisableMenu() {
        menuActive = false;
    }
}
