using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackground : MonoBehaviour {

    public SpriteRenderer[] backgrounds;

    // Start is called before the first frame update
    void Start() {
        var background = Random.Range(0, backgrounds.Length);
        backgrounds[background].gameObject.SetActive(true);

    }
}
