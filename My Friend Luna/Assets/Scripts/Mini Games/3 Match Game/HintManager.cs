using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour {

    public static HintManager instance;

    public float hintDelay;
    public float hintDelaySeconds;
    public GameObject hintParticle;
    public GameObject currentHint;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        hintDelaySeconds = hintDelay;
    }

    private void Update() {
        hintDelaySeconds -= Time.deltaTime;
        if(hintDelaySeconds <= 0 && currentHint == null) {
            MarkHint();
            hintDelaySeconds = hintDelay;
        }
    }

    //find all possible matches on the board
    List<GameObject> FindAllMatches() {

        List<GameObject> possibleMoves = new List<GameObject>();

        for (int i = 0; i < Board.instance.width; i++) {
            for (int j = 0; j < Board.instance.heigth; j++) {
                if (Board.instance.allDots[i, j] != null) {
                    if (i < Board.instance.width - 1) {
                        if (Board.instance.SwitchAndCheck(i, j, Vector2.right)) {
                            possibleMoves.Add(Board.instance.allDots[i, j]);
                        }
                    }

                    if (j < Board.instance.heigth - 1) {
                        if (Board.instance.SwitchAndCheck(i, j, Vector2.up)) {
                            possibleMoves.Add(Board.instance.allDots[i, j]);
                        }
                    }
                }
            }
        }
        return possibleMoves;
    }

    //pick one match randomly
    GameObject PickOneRandomly() {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = FindAllMatches();
        if(possibleMoves.Count > 0) {
            int pieceToUse = Random.Range(0, possibleMoves.Count);
            return possibleMoves[pieceToUse];
        }

        return null;
    }

    //create the hint on match
    private void MarkHint() {
        GameObject move = PickOneRandomly();
        if(move != null) {
            currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
        }
    }

    //destroy the hint
    public void DestroyHint() {
        if(currentHint != null) {
            Destroy(currentHint);
            currentHint = null;
            hintDelaySeconds = hintDelay;
        }
    }
}
