using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoopController : MonoBehaviour {

    public static PoopController instance;

    public GameObject poop;
    public Transform poopLocation;
    public Animator theAnim;

    private void Awake() {
        instance = this;

        DontDestroyOnLoad(poop);
    }

    private void Start() {
        if(PlayerPrefs.GetInt("Poop") == 0) {
            poop.SetActive(false);
        } else {
            SimplePool.Spawn(poop, poopLocation.transform.position, poop.transform.rotation);
        }
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit != false && hit.collider != null && hit.collider.CompareTag("poop")) {
                Destroy(hit.collider.gameObject);
                PlayerPrefs.SetInt("Poop", 0);
            }
        }        
    }

    public void GoPoop() {
        if(PetController.instance._bathroom < 80) {
            theAnim.SetTrigger("Fade");
            AudioManager.instance.PlaySFX(3);
            PetController.instance._bathroom = 100;
        } else {
            if(PlayerPrefs.HasKey("PetSelected")) {
                AudioManager.instance.PlaySFX(6);
            } else {
                AudioManager.instance.PlaySFX(7);
            }          
        }        
    }

    public void SpawnPoop() {
        SimplePool.Spawn(poop, poopLocation.transform.position, poop.transform.rotation);
    }
}
