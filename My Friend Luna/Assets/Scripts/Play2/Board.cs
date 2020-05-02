using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public GameObject destroyEffect;

    private BackgroundTile[,] allTiles;
    public GameObject[,] allDots;
    public DotController currentDot;

    private int basePieceValue = 5;
    private int streakValue = 1;
    public float refillDelay = .5f;

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
                //GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                //backgroundTile.transform.parent = this.transform;
                //backgroundTile.name = "( " + i + ", " + j + " )";
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

            //play destroy sound
            AudioManager.instance.PlayRandomDestroySound();

            //destroy match particle
            GameObject particle = Instantiate(destroyEffect, allDots[column, row].transform.transform.position, Quaternion.identity);
            Destroy(particle, .5f);

            //how many dots matched from find matches
            if (FindMatches.instance.currentMatches.Count >= 4) {
                CheckToMakeBombs();
            }
            
            Destroy(allDots[column, row]);
            currentDot = null;
            ScoreManager.instance.IncreaseScore(basePieceValue * streakValue);
            allDots[column, row] = null;

            //GOLD PRO AMIGO
            PetController.instance.money += 5;
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

        yield return new WaitForSeconds(refillDelay * .5f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] == null) {
                    Vector2 tempPos = new Vector2(i, j + offset);
                    int dotToUse = Random.Range(0, dots.Length);

                    int maxIterations = 0;
                    while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100) {
                        maxIterations++;
                        dotToUse = Random.Range(0, dots.Length);
                    }

                    GameObject piece = Instantiate(dots[dotToUse], tempPos, Quaternion.identity);
                    allDots[i, j] = piece;
                    piece.GetComponent<DotController>().row = j;
                    piece.GetComponent<DotController>().column = i;
                }
            }
        }
    }

    public void RestartDots() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                Destroy(allDots[i, j]);
                //Destroy();
            }
        }

        //create all the dots again
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                Vector2 tempPosition = new Vector2(i, j + offset);
                int dotToUse = Random.Range(0, dots.Length);

                int maxIterations = 0;
                while (MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100) {
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

        yield return new WaitForSeconds(refillDelay);

        while(MatchesOnBoard()) {
            streakValue++;
            DestroyMatches();
            yield return new WaitForSeconds(refillDelay * 1.5f);           
        }
        FindMatches.instance.currentMatches.Clear();
        currentDot = null;
        
        if(IsDeadLocked()) {
            //test
            RestartDots();
        }

        yield return new WaitForSeconds(refillDelay);
        currentState = GameState.move;
        streakValue = 1;
    }

    private void SwitchPieces(int column, int row, Vector2 direction) {
        //take the second piece and save in holder
        GameObject holder = allDots[column + (int)direction.x, row + (int)direction.y] as GameObject;
        //switching the first dot to be the second position
        allDots[column + (int)direction.x, row + (int)direction.y] = allDots[column, row];
        //set the first dot to be the second dot
        allDots[column, row] = holder;
    }

    private bool CheckForMatches() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {

                //check if the dots are in the board
                if (i < width - 2) {
                    //check 1 dot to the right and 2 to the left
                    if (allDots[i + 1, j] != null && allDots[i + 2, j] != null) {
                        if (allDots[i, j].CompareTag(allDots[i + 1, j].tag) && allDots[i, j].CompareTag(allDots[i + 2, j].tag)) {
                            return true;
                        }
                    }
                }

                if (j < heigth - 2) {
                    //check dots above
                    if (allDots[i, j + 1] != null && allDots[i, j + 2] != null) {
                        if (allDots[i, j].CompareTag(allDots[i, j + 1].tag) && allDots[i, j].CompareTag(allDots[i, j + 2].tag)) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool SwitchAndCheck(int col, int row, Vector2 direction) {
        SwitchPieces(col, row, direction);
        if(CheckForMatches()) {
            SwitchPieces(col, row, direction);
            return true;
        }

        SwitchPieces(col, row, direction);
        return false;
    }

    private bool IsDeadLocked() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < heigth; j++) {
                if(allDots[i, j] != null) {
                    if(i < width - 1) {
                        if(SwitchAndCheck(i, j, Vector2.right)) {
                            return false;
                        }
                    }

                    if (j < heigth - 1) {
                        if (SwitchAndCheck(i, j, Vector2.up)) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
