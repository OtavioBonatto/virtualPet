using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour {

    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;    
    
    public GameObject otherDot;
    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 tempPos;

    public float swipeAngle = 0;
    public float swipeResist = 1f;

    public bool isColorBomb;
    public bool isColumnBomb;
    public bool isRowBomb;
    public bool isAdjacentBomb;
    public GameObject adjacentMarker;
    public GameObject rowArrow;
    public GameObject columnArrow;
    public GameObject colorBomb;

    private void Start() {
        isColumnBomb = false;
        isRowBomb = false;
        isColorBomb = false;
        isAdjacentBomb = false;
        Board.instance.currentState = GameState.move;
    }

    // Update is called once per frame
    void Update() {

        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1) {
            //move towards the target
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .6f);
            if(Board.instance.allDots[column, row] != this.gameObject) {
                Board.instance.allDots[column, row] = this.gameObject;
            }
            FindMatches.instance.FindAllMatches();


        } else {
            //directly set the position
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;            
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1) {
            //move towards the target
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .6f);
            if (Board.instance.allDots[column, row] != this.gameObject) {
                Board.instance.allDots[column, row] = this.gameObject;
            }
            FindMatches.instance.FindAllMatches();

        } else {
            //directly set the position
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
        }
    }

    //for testing and debug
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
        }
    }

    public IEnumerator CheckMoveCo() {
        if (isColorBomb) {
            //This piece is a color bomb, and the other piece is the color to destroy
            FindMatches.instance.MatchPiecesOfColor(otherDot.tag);
            isMatched = true;
        } else if (otherDot.GetComponent<DotController>().isColorBomb) {
            //The other piece is a color bomb, and this piece has the color to destroy
            FindMatches.instance.MatchPiecesOfColor(this.gameObject.tag);
            otherDot.GetComponent<DotController>().isMatched = true;
        }
        yield return new WaitForSeconds(.5f);
        if (otherDot != null) {
            if (!isMatched && !otherDot.GetComponent<DotController>().isMatched) {
                otherDot.GetComponent<DotController>().row = row;
                otherDot.GetComponent<DotController>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                Board.instance.currentDot = null;
                Board.instance.currentState = GameState.move;
            } else {
                Board.instance.DestroyMatches();

            }
            //otherDot = null;
        }

    }

    private void OnMouseDown() {

        //destroy the hint
        HintManager.instance.DestroyHint();
        

        if(Board.instance.currentState == GameState.move) {
            firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }        
    }

    private void OnMouseUp() {
        if(Board.instance.currentState == GameState.move && EndGameManager.instance.endGame == false) {
            finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }        
    }

    void CalculateAngle() {
        if(Mathf.Abs(finalTouchPos.y - firstTouchPos.y) > swipeResist || Mathf.Abs(finalTouchPos.x - firstTouchPos.x) > swipeResist) {
            Board.instance.currentState = GameState.wait;
            swipeAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
            MovePieces();
            Board.instance.currentDot = this;
        } else {
            Board.instance.currentState = GameState.move;
        }  
    }

    void MovePiecesActual(Vector2 direction) {
        otherDot = Board.instance.allDots[column + (int)direction.x, row + (int)direction.y];
        previousRow = row;
        previousColumn = column;
        otherDot.GetComponent<DotController>().column += -1 * (int)direction.x;
        otherDot.GetComponent<DotController>().row += -1 * (int)direction.y;
        column += (int)direction.x;
        row += (int)direction.y;
        StartCoroutine(CheckMoveCo());
    }

    void MovePieces() {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < Board.instance.width - 1) {
            //Right Swipe
            MovePiecesActual(Vector2.right);
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < Board.instance.heigth- 1) {
            //Up Swipe
            MovePiecesActual(Vector2.up);
        } else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0) {
            //Left Swipe
            MovePiecesActual(Vector2.left);
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0) {
            //Down Swipe
            MovePiecesActual(Vector2.down);
        } else {
            Board.instance.currentState = GameState.move;
        }

        //Board.instance.currentState = GameState.move;

    }      
    
    public void MakeRowBomb() {
        isRowBomb = true;
        GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnBomb() {
        isColumnBomb = true;
        GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColorBomb() {
        isColorBomb = true;
        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
        this.gameObject.tag = "Color";
    }

    public void MakeAdjacentBomb() {
        isAdjacentBomb = true;
        GameObject marker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
        marker.transform.parent = this.transform;
    }
}
