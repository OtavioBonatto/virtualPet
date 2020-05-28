using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CloudController : MonoBehaviour {

    private static CloudController instance;
    private Vector3 startPosition;
    public Transform endPosition;

    private void Awake() {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(endPosition);

        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        startPosition = transform.position;
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x + 0.05f * Time.deltaTime, transform.position.y, transform.position.z);

        if(transform.position.x >= endPosition.position.x) {
            transform.position = startPosition;
        }
    }
}
