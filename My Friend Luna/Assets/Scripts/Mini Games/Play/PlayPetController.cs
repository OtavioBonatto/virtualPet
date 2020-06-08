using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayPetController : MonoBehaviour {

    public static PlayPetController instance;

    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D theRB;

    public bool onGround;
    public LayerMask ground;
    public Transform groundCheck;
    public float groundCheckRadius;

    public Text totalPointsText;
    public Text scoreText;
    public Text highscoreText;
    public int score;
    public int highscore;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        theRB = GetComponent<Rigidbody2D>();

        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        score = 0;
        scoreText.text = "Score: " + score;

        if (PlayerPrefs.HasKey("HighScoreRun")) {
            highscore = PlayerPrefs.GetInt("HighScoreRun");
        }
        highscoreText.text = "Best Score: " + highscore;
    }

    // Update is called once per frame
    void Update() {
        PetController.instance.Play();

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);

        if(transform.position.x > speedMilestoneCount) {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedMultiplier;
            moveSpeed *= speedMultiplier;
        }

        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

        if(Input.GetMouseButtonDown(0)) {
            if(onGround) {
                AudioManager.instance.soundEffects[2].pitch = Random.Range(.9f, 1.1f);
                AudioManager.instance.PlaySFX(2);
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }            
        }

        if(Input.GetMouseButton(0)) {
            if(jumpTimeCounter > 0) {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetMouseButtonUp(0)) {
            jumpTimeCounter = 0;
        }

        if(onGround) {
            jumpTimeCounter = jumpTime;
        }

        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetInt("HighScoreRun", highscore);
        }

        highscoreText.text = "Best Score: " + highscore;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("killbox")) {
            GameManager.instance.RestartGame();

            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;

            totalPointsText.text = "" + score + "$";

            score = 0;
            scoreText.text = "Score: " + score;

        }
    }
}
