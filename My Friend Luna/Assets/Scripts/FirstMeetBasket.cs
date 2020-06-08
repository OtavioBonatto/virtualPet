using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMeetBasket : MonoBehaviour {
    public static FirstMeetBasket instance;

    public GameObject basket;
    private Animator theAnim;

    private float normal;

    private void Awake() {
        instance = this;
    }

    public void BasketAnimation() {
        theAnim = PetController.instance.GetComponent<Animator>();
        normal = theAnim.speed;
        StartCoroutine(BasketAnimationCo());
    }

    public IEnumerator BasketAnimationCo() {
        theAnim.speed = 0;
        GameObject RIPInstance = Instantiate(basket, this.gameObject.transform);
        yield return new WaitForSeconds(3f);
        theAnim.speed = normal;
        Destroy(RIPInstance);
    }
}
