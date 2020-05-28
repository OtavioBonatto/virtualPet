using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GetPetName : MonoBehaviour {

    public static GetPetName instance;

    public InputField petNameInput;
    public string petName;
    private TouchScreenKeyboard mobileKeys;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        mobileKeys = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

        if(mobileKeys != null) {
            mobileKeys.active = false;
        }        
    }

    private void OnGUI() {
        if(petNameInput.isFocused && petNameInput.text != "" && Input.GetKey(KeyCode.Return)) {
            petName = petNameInput.text;
        }

        if (petNameInput.text != "" && mobileKeys != null && mobileKeys.status == TouchScreenKeyboard.Status.Done) {
            petName = petNameInput.text;
        }
    }

    public void GetName(string name) {
        PetController.instance._name = name;
        PetController.instance.SavePet();
    }
}
