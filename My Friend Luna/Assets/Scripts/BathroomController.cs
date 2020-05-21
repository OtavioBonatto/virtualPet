using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomController : MonoBehaviour {
    
    public void PlayBathroomSound() {
        AudioManager.instance.PlaySFX(3);
    }
}
