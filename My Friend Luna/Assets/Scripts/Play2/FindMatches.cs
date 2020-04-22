using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour {

    public static FindMatches instance;

    public List<GameObject> currentMatches = new List<GameObject>();

    private void Awake() {
        instance = this;
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsAdjacentBomb(DotController dot1, DotController dot2, DotController dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
        }

        if (dot2.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
        }

        if (dot3.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
        }

        return currentDots;
    }

    private List<GameObject> IsRowBomb(DotController dot1, DotController dot2, DotController dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot1.row));
        }

        if (dot2.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot2.row));
        }

        if (dot3.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot3.row));
        }

        return currentDots;
    }

    private List<GameObject> IsColumnBomb(DotController dot1, DotController dot2, DotController dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot1.column));
        }

        if (dot2.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot2.column));
        }

        if (dot3.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot3.column));
        }

        return currentDots;
    }

    private void AddToListAndMatch(GameObject dot) {
        if (!currentMatches.Contains(dot)) {
            currentMatches.Add(dot);
        }
        dot.GetComponent<DotController>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3) {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo() {
        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < Board.instance.width; i++) {
            for (int j = 0; j < Board.instance.heigth; j++) {

                GameObject currentDot = Board.instance.allDots[i, j];  
                if (currentDot != null) {
                    DotController currentDotDot = currentDot.GetComponent<DotController>();

                    if (i > 0 && i < Board.instance.width - 1) {
                        GameObject leftDot = Board.instance.allDots[i - 1, j];
                        GameObject rightDot = Board.instance.allDots[i + 1, j];

                        if (leftDot != null && rightDot != null) {
                            DotController leftDotDot = leftDot.GetComponent<DotController>();
                            DotController rightDotDot = rightDot.GetComponent<DotController>();
                            if (currentDot.CompareTag(leftDot.tag) && currentDot.CompareTag(rightDot.tag)) {
                                currentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.Union(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.Union(IsAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));

                                GetNearbyPieces(leftDot, currentDot, rightDot);
                            }
                        }
                    }

                    if (j > 0 && j < Board.instance.heigth - 1) {
                        GameObject upDot = Board.instance.allDots[i, j + 1];
                        GameObject downDot = Board.instance.allDots[i, j - 1];

                        if (upDot != null && downDot != null) {
                            DotController upDotDot = upDot.GetComponent<DotController>();
                            DotController downDotDot = downDot.GetComponent<DotController>();
                            if (currentDot.CompareTag(upDot.tag) && currentDot.CompareTag(downDot.tag)) {
                                currentMatches.Union(IsColumnBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.Union(IsRowBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.Union(IsAdjacentBomb(upDotDot, currentDotDot, downDotDot));

                                GetNearbyPieces(upDot, currentDot, downDot);
                            }
                        }
                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(string color) {
        for (int i = 0; i < Board.instance.width; i++) {
            for (int j = 0; j < Board.instance.heigth; j++) {
                //check if that piece exists
                if(Board.instance.allDots[i, j] != null) {
                    //check the tag of that dot
                    if(Board.instance.allDots[i, j].CompareTag(color)) {
                        //set that dot to be matched
                        Board.instance.allDots[i, j].GetComponent<DotController>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row) {
        List<GameObject> dots = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i++) {
            for (int j = row - 1; j <= row + 1; j++) {
                //check if the piece is inside the board
                if (i >= 0 && i < Board.instance.width && j >= 0 && j < Board.instance.heigth) {
                    dots.Add(Board.instance.allDots[i, j]);
                    Board.instance.allDots[i, j].GetComponent<DotController>().isMatched = true;
                }
            }
        }
        return dots;
    }

    List<GameObject> GetColumnPieces(int column) {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < Board.instance.heigth; i++) {
            if(Board.instance.allDots[column, i] != null) {
                dots.Add(Board.instance.allDots[column, i]);
                Board.instance.allDots[column, i].GetComponent<DotController>().isMatched = true;
            }
        }
        return dots;
    }

    List<GameObject> GetRowPieces(int row) {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < Board.instance.width; i++) {
            if (Board.instance.allDots[i, row] != null) {
                dots.Add(Board.instance.allDots[i, row]);
                Board.instance.allDots[i, row].GetComponent<DotController>().isMatched = true;
            }
        }
        return dots;
    }

    public void CheckBombs() {
        //did the player move something
        if(Board.instance.currentDot != null) {
            //is the piece matched
            if (Board.instance.currentDot.isMatched) {
                Board.instance.currentDot.isMatched = false;
                //decide what kind of bomb
                if((Board.instance.currentDot.swipeAngle > -45 && Board.instance.currentDot.swipeAngle <= 45) || (Board.instance.currentDot.swipeAngle < -135 || Board.instance.currentDot.swipeAngle >= 135)) {
                    Board.instance.currentDot.MakeRowBomb();
                } else {
                    Board.instance.currentDot.MakeColumnBomb();
                }
            } else if(Board.instance.currentDot.otherDot != null) {
                DotController otherDot = Board.instance.currentDot.otherDot.GetComponent<DotController>();
                //other dot matched
                if(otherDot.isMatched) {
                    otherDot.isMatched = false;
                    //decide what kind of bomb
                    if ((Board.instance.currentDot.swipeAngle > -45 && Board.instance.currentDot.swipeAngle <= 45) || (Board.instance.currentDot.swipeAngle < -135 || Board.instance.currentDot.swipeAngle >= 135)) {
                        otherDot.MakeRowBomb();
                    } else {
                        otherDot.MakeColumnBomb();
                    }
                }
            }
        }
    }
}
