using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBowl : MonoBehaviour {

    public static FillBowl instance;

    public Sprite bowl;
    public Sprite bowlWater;
    private SpriteRenderer theSR;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        theSR = GetComponent<SpriteRenderer>();

        if(PlayerPrefs.GetInt("FullBowl") == 1) {
            theSR.sprite = bowlWater;
        } else {
            theSR.sprite = bowl;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit != false && hit.collider != null && hit.collider.CompareTag("Bowl")) {
                if(theSR.sprite == bowl) {
                    AudioManager.instance.PlaySFX(2);
                    theSR.sprite = bowlWater;
                    PlayerPrefs.SetInt("FullBowl", 1);
                }
            }
        }
    }

    public void EmptyBowl() {
        theSR.sprite = bowl;
    }
}
