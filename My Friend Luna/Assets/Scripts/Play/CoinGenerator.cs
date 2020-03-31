using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {
    public static CoinGenerator instance;

    public GameObject coin;
    public GameObject spikes;

    private void Awake() {
        instance = this;
    }

    public void SpawnCoin(Vector3 startPosition) {
        SimplePool.Spawn(coin, startPosition, coin.transform.rotation);
    }

    public void SpawnSpikes(Vector3 startPosition) {
        SimplePool.Spawn(spikes, startPosition, spikes.transform.rotation);
    }
}
