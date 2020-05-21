using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public List<Button> btns = new List<Button>();
    public Sprite bgImage;

    public Sprite[] cards;
    public List<Sprite> gameCards = new List<Sprite>();

    private bool firstGuess;
    private bool secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex;
    private int secondGuessIndex;
    private string firstGuessCard;
    private string secondGuessCard;

    public int memoryGameScore;
    public Text memogryGameScoreText;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        GetButtons();
        AddListner();
        AddGameCards();
        Shuffle(gameCards);
        gameGuesses = gameCards.Count / 2;
        memoryGameScore = 0;
        memogryGameScoreText.text = "Pontuação: " + memoryGameScore;
    }

    private void Update() {
        PetController.instance.Play();
    }

    private void GetButtons() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++) {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    private void AddGameCards() {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++) {
            if(index == looper / 2) {
                index = 0;
            }

            gameCards.Add(cards[index]);
            index++;
        }
    }

    private void AddListner() {
        foreach (Button btn in btns) {
            btn.onClick.AddListener(() => PickACard());
        }
    }

    public void PickACard() {
        
        if(!firstGuess) {

            AudioManager.instance.PlaySFX(1);
            firstGuess = true;
            firstGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            firstGuessCard = gameCards[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gameCards[firstGuessIndex];

        } else if(!secondGuess) {

            AudioManager.instance.PlaySFX(1);
            secondGuess = true;
            secondGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            secondGuessCard = gameCards[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gameCards[secondGuessIndex];

            StartCoroutine(CheckIfCardsMatch());
        }
    }

    IEnumerator CheckIfCardsMatch() {
        yield return new WaitForSeconds(0.7f);

        if (firstGuessCard.Equals(secondGuessCard) && firstGuessIndex != secondGuessIndex) {

            AudioManager.instance.PlaySFX(2);
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            memoryGameScore += 50;
            PetController.instance.money += 50;
            memogryGameScoreText.text = "Pontuação: " + memoryGameScore;

            CheckIfGameIsFinished();
        } else {
            yield return new WaitForSeconds(.4f);

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.3f);

        firstGuess = secondGuess = false;
    }

    private void CheckIfGameIsFinished() {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses) {
            Debug.Log("Fim do jogo.");
            btns.Clear();
            GetButtons();
            for (int i = 0; i < btns.Count; i++) {
                btns[i].interactable = true;
                btns[i].image.sprite = bgImage;
                btns[i].image.color = new Color(255, 255, 255);
            }

            Shuffle(gameCards);

            gameGuesses = gameCards.Count / 2;
            countCorrectGuesses = 0;
        }
    }

    private void Shuffle(List<Sprite> list) {
        for (int i = 0; i < list.Count; i++) {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
