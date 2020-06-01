using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RIPController : MonoBehaviour {

    public static RIPController instance;

    public GameObject RIP;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    public void RIPAnimation() {
        StartCoroutine(RIPAnimationCo());
    }

    public IEnumerator RIPAnimationCo() {
        GameObject RIPInstance = Instantiate(RIP, this.gameObject.transform);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Start Menu");
        Destroy(RIPInstance);
    }
}
