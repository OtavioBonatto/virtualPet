using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 lastPlayerPos;
    private float distanceToMove;

    // Start is called before the first frame update
    void Start() {
        lastPlayerPos = PlayPetController.instance.transform.position;
    }

    // Update is called once per frame
    void Update() {
        distanceToMove = PlayPetController.instance.transform.position.x - lastPlayerPos.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastPlayerPos = PlayPetController.instance.transform.position;
    }
}
