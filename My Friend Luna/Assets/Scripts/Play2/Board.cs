using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    wait,
    move
}

public class Board : MonoBehaviour {

    public static Board instance;

    public GameState currentState = GameState.move;
    public int width;
    public int heigth;
    public int offset;
    public GameObject tilePrefab;
    public GameObject[] dots;
    private BackgroundTile[,] allTiles;
    public GameObject[,] allDots;
    public DotController currentDot;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        allTiles = new BackgroundTile[width, heigth];
        allDots = new GameObject[width, heigth];
        SetUp();
    }

    private void SetUp() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                Vector2 tempPosition = new Vector2(i, j + offset);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";
                int dotToUse = Random.Range(0, dots.Length);

                int maxIterations = 0;
                while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100) {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.GetComponent<DotController>().row = j;
                dot.GetComponent<DotController>().column = i;
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                allDots[i, j] = dot;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece) {

        if(column > 1 && row > 1) {
            if(piece.CompareTag(allDots[column - 1, row].tag) && piece.CompareTag(allDots[column - 2, row].tag)) {
                return true;
            }
            if(piece.CompareTag(allDots[column, row - 1].tag) && piece.CompareTag(allDots[column, row - 2].tag)) {
                return true;
            }
        } else if(column <= 1 || row <= 1) {
            if(row > 1) {
                if(piece.CompareTag(allDots[column, row - 1].tag) && piece.CompareTag(allDots[column, row - 2].tag)) {
                    return true;
                }
            }
            if (column > 1) {
                if (piece.CompareTag(allDots[column - 1, row].tag) && piece.CompareTag(allDots[column - 2, row].tag)) {
                    return true;
                }
            }
        }
        return false;
    }

    private bool ColumnOrRow() {
        int numberHorizontal = 0;
        int numberVertical = 0;
        DotController firstPiece = FindMatches.instance.currentMatches[0].GetComponent<DotController>();

        if(firstPiece != null) {
            foreach (GameObject currentPiece in FindMatches.instance.currentMatches) {
                DotController dot = currentPiece.GetComponent<DotController>();
                if(dot.row == firstPiece.row) {
                    numberHorizontal++;
                }
                if(dot.column == firstPiece.column) {
                    numberVertical++;
                }
            }
        }

        return (numberVertical == 5 || numberHorizontal == 5);
    } 

    private void CheckToMakeBombs() {
        if(FindMatches.instance.currentMatches.Count == 4 || FindMatches.instance.currentMatches.Count == 7) {
            FindMatches.instance.CheckBombs();
        }

        if(FindMatches.instance.currentMatches.Count == 5 || FindMatches.instance.currentMatches.Count == 8) {
            if(ColumnOrRow()) {
                //make a color bomb
                if(currentDot != null) {
                    if(currentDot.isMatched) {
                        if(!currentDot.isColorBomb) {
                            currentDot.isMatched = false;
                            currentDot.MakeColorBomb();
                        }
                    } else {
                        if(currentDot.otherDot != null) {
                            DotController otherDot = currentDot.otherDot.GetComponent<DotController>();
                            if(otherDot.isMatched) {
                                if(!otherDot.isColorBomb) {
                                    otherDot.isMatched = true;
                                    otherDot.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
            } else {
                //make a adjacent bomb
                if (currentDot != null) {
                    if (currentDot.isMatched) {
                        if (!currentDot.isAdjacentBomb) {
                            currentDot.isMatched = false;
                            currentDot.MakeAdjacentBomb();
                        }
                    } else {
                        if (currentDot.otherDot != null) {
                            DotController otherDot = currentDot.otherDot.GetComponent<DotController>();
                            if (otherDot.isMatched) {
                                if (!otherDot.isAdjacentBomb) {
                                    otherDot.isMatched = true;
                                    otherDot.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }
            }

        }
    }

    private void DestroyMatchesAt(int column, int row) {
        if(allDots[column, row].GetComponent<DotController>().isMatched) {
            //how many dots matched from find matches
            if(FindMatches.instance.currentMatches.Count >= 4) {
                CheckToMakeBombs();
            }
            
            Destroy(allDots[column, row]);
            currentDot = null;
            allDots[column, row] = null;
        }
    }

    public void DestroyMatches() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] != null) {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        FindMatches.instance.currentMatches.Clear();
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo() {

        int nullCount = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] == null) {
                    nullCount++;
                } else if(nullCount > 0){
                    allDots[i, j].GetComponent<DotController>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }

        yield return new WaitForSeconds(.3f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] == null) {
                    Vector2 tempPos = new Vector2(i, j + offset);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPos, Quaternion.identity);
                    allDots[i, j] = piece;
                    piece.GetComponent<DotController>().row = j;
                    piece.GetComponent<DotController>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard() {

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] != null) {
                    if(allDots[i, j].GetComponent<DotController>().isMatched) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo() {
        RefillBoard();

        yield return new WaitForSeconds(.3f);
        while(MatchesOnBoard()) {
            yield return new WaitForSeconds(.3f);
            DestroyMatches();
        }
        FindMatches.instance.currentMatches.Clear();

        yield return new WaitForSeconds(.3f);
        currentState = GameState.move;
    }
}
