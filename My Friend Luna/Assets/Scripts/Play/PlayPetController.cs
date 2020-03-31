using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update() {

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);

        if(transform.position.x > speedMilestoneCount) {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedMultiplier;
            moveSpeed *= speedMultiplier;
        }

        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

        if(Input.GetMouseButtonDown(0)) {
            if(onGround) {
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
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "killbox") {
            GameManager.instance.RestartGame();

            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
        }
    }
}
