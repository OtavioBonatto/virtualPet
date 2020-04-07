using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoopController : MonoBehaviour {

    public static PoopController instance;

    public GameObject poop;

    private void Awake() {
        instance = this;

        DontDestroyOnLoad(poop);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider.gameObject.CompareTag("poop")) {
                //Debug.Log(hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void SpawnPoop() {
        SimplePool.Spawn(poop, poop.transform.position, poop.transform.rotation);
    }
}
