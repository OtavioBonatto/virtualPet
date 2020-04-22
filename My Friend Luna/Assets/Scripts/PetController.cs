using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetController : MonoBehaviour {

    public static PetController instance;

    public int _hunger;
    public int _happiness;
    public int _bathroom;
    public int _energy;

  
    public float _weigth;
    public int _age;
    public string _health;

    public int hungerTicketRate;
    public int happinessTicketRate;
    public int bathroomTicketRate;
    public int energyTicketRate;

    private bool _serverTime;

    public InventoryController inventory;

    public int money;
    public bool hungry;

    private Animator theAnim;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        theAnim = GetComponent<Animator>();

        //PlayerPrefs.SetString("then", "03/17/2020 23:26:00");
        UpdateStatus();
        PetUIController.instance.UpdateWeigth(_weigth, _age, _health);
    }

    // Update is called once per frame
    void Update() {

        if(TimeManager.gameHourTimer < 0) {
            if(_hunger > 0) _hunger -= hungerTicketRate;
            if(_happiness > 0) _happiness -= happinessTicketRate;
            if(_bathroom > 0) _bathroom -= bathroomTicketRate;
            if(_energy > 0) _energy -= energyTicketRate;
        }

        //day/night cycle
        GlobalLightController.instance.DayNightCycle();

        //actions
        if(_bathroom < 20) {
            PoopController.instance.SpawnPoop();
            _bathroom = 100;
            _happiness -= 40;
        }

        //fome
        if(_hunger < 50) {
            hungry = true;
        } else {
            hungry = false;
        }

        //saúde
        if(_hunger <= 1) {
            _health = "Ruim";
        } 

        PetUIController.instance.UpdateImages(_hunger, _happiness, _bathroom, _energy);
        PetUIController.instance.UpdateWeigth(_weigth, _age, _health);
        MoneyUIController.instance.UpdateMoney(money);

        theAnim.SetBool("Hungry", hungry);
    }

    void UpdateStatus() {
        if(!PlayerPrefs.HasKey("_hunger")) {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        } else {
            _hunger = PlayerPrefs.GetInt("_hunger");            
        }

        if (!PlayerPrefs.HasKey("_happiness")) {
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        } else {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

        if (!PlayerPrefs.HasKey("_bathroom")) {
            _bathroom = 100;
            PlayerPrefs.SetInt("_bathroom", _bathroom);
        } else {
            _bathroom = PlayerPrefs.GetInt("_bathroom");
        }

        if (!PlayerPrefs.HasKey("_energy")) {
            _energy = 100;
            PlayerPrefs.SetInt("_energy", _energy);
        } else {
            _energy = PlayerPrefs.GetInt("_energy");
        }

        if (!PlayerPrefs.HasKey("_money")) {
            money = 500;
            PlayerPrefs.SetInt("_money", money);
        } else {
            money = PlayerPrefs.GetInt("_money");
        }

        if (!PlayerPrefs.HasKey("_weigth")) {
            _weigth = 15;
            PlayerPrefs.SetFloat("_weigth", _weigth);
        } else {
            _weigth = PlayerPrefs.GetFloat("_weigth");
        }

        if (!PlayerPrefs.HasKey("_age")) {
            _age = 1;
            PlayerPrefs.SetInt("_age", _age);
        } else {
            _age = PlayerPrefs.GetInt("_age");
        }

        if (!PlayerPrefs.HasKey("_health")) {
            _health = "Boa";
            PlayerPrefs.SetString("_health", _health);
        } else {
            _health = PlayerPrefs.GetString("_health");
        }

        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then", GetStringTime());
        }

        TimeSpan ts = GetTimeSpan();

        _hunger -= (int) (ts.TotalHours * 5);
        if(_hunger < 0) {
            _hunger = 0;
        }

        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        if (_happiness < 0) {
            _happiness = 0;
        }

        _bathroom -= (int)(ts.TotalHours * 10);
        if (_bathroom < 0) {
            _bathroom = 0;
        }

        if(LightController.instance.toggleLight.isOn == true) {
            _energy -= (int)(ts.TotalHours * 10);
            if (_energy < 0) {
                _energy = 0;
            }
        } else {
            _energy += (int)(ts.TotalHours * 12);
            if (_energy > 100) {
                _energy = 100;
            }
        }
        

        //Debug.Log(GetTimeSpan().TotalHours);

        if (_serverTime) {
            UpdateServer();
        } else {
            UpdateDevice();
        }
    }

    public void UpdateMoney() {
        PlayerPrefs.SetInt("_money", money);
    }

    void UpdateServer() {

    }

    void UpdateDevice() {
        PlayerPrefs.SetString("then", GetStringTime());
    }

    TimeSpan GetTimeSpan() {
        if(_serverTime) {
            return new TimeSpan();
        } else {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        }
    }

    string GetStringTime() {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    public int Hunger {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int Happiness {
        get { return _happiness; }
        set { _happiness = value; }
    }

    public int Bathroom {
        get { return _bathroom; }
        set { _bathroom = value; }
    }

    public int Energy {
        get { return _energy; }
        set { _energy = value; }
    }  

    public void SavePet() { 
        if(!_serverTime) {
            UpdateDevice();
            PlayerPrefs.SetInt("_hunger", _hunger);
            PlayerPrefs.SetInt("_happiness", _happiness);
            PlayerPrefs.SetInt("_bathroom", _bathroom);
            PlayerPrefs.SetInt("_energy", _energy);
            PlayerPrefs.SetFloat("_weigth", _weigth);
            PlayerPrefs.SetString("_health", _health);
            UpdateMoney();
        }
    }

    public void Eat(int hungerRecover) {
        Debug.Log(_weigth);
        _hunger += hungerRecover;
        if(_hunger > 100) {
            _hunger = 100;
        }
    }

    public void Poop() {
        _bathroom = 100;
    }

    public void Play() {
        _happiness += 25;
        
        if (_happiness >= 100) {
            _happiness = 100;
        }

        if (_weigth > 1) {
            _weigth -= 0.5f;
        }
    }

    public void Sleep() {
        _energy = 100;
    }

    void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            SavePet();
            Application.Quit();
        }
    }

    private void OnApplicationQuit() {
        SavePet();
        Application.Quit();
    }
}
