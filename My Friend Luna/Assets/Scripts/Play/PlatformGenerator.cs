using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPoint;

    private float platformWidth;

    public float randomCoinChance;
    public float randomSpikesChance;

    // Start is called before the first frame update
    void Start() {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.x < generationPoint.position.x) {
            //transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x + (platformWidth / 2), transform.position.y, transform.position.z);

            SimplePool.Spawn(platform, transform.position, transform.rotation);

            if(Random.Range(0f, 100f) < randomCoinChance) {
                CoinGenerator.instance.SpawnCoin(new Vector3(transform.position.x + (platformWidth / 2), transform.position.y + 1f, transform.position.z));
            } else if(Random.Range(0f, 100f) < randomSpikesChance) {
                CoinGenerator.instance.SpawnSpikes(new Vector3(transform.position.x + (platformWidth / 2), transform.position.y + 1f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + (platformWidth / 2), transform.position.y, transform.position.z);
        }
    }
}
