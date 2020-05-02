using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraScaler : MonoBehaviour {

    public float cameraOffset;
    public float aspectRatio = 0.625f;
    public float padding = 2;

    // Start is called before the first frame update
    void Start() {
        if(Board.instance != null) {
            RepositionCamera(Board.instance.width - 1, Board.instance.heigth - 1);
        }
    }

    void RepositionCamera(float x, float y) {
        Vector3 tempPosition = new Vector3(x / 2, y / 2, cameraOffset);
        transform.position = tempPosition;
        if(Board.instance.width >= Board.instance.heigth) {
            Camera.main.orthographicSize = (Board.instance.width / 2 + padding) / aspectRatio;
        } else {
            Camera.main.orthographicSize = Board.instance.heigth / 2 + padding;
        }
        
    }
}
