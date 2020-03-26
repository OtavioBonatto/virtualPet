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

    public int hungerTicketRate;
    public int happinessTicketRate;
    public int bathroomTicketRate;
    public int energyTicketRate;

    private bool _serverTime;

    public InventoryController inventory;

    public int money;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        //PlayerPrefs.SetString("then", "03/17/2020 23:26:00");
        UpdateStatus();
    }

    // Update is called once per frame
    void Update() {

        if(TimeManager.gameHourTimer < 0) {
            if(_hunger > 0) _hunger -= hungerTicketRate;
            if(_happiness > 0) _happiness -= happinessTicketRate;
            if(_bathroom > 0) _bathroom -= bathroomTicketRate;
            if(_energy > 0) _energy -= energyTicketRate;
        }

        PetUIController.instance.UpdateImages(_hunger, _happiness, _bathroom, _energy);
        MoneyUIController.instance.UpdateMoney(money);
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

        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then", GetStringTime());
        }

        TimeSpan ts = GetTimeSpan();

        _hunger -= (int) (ts.TotalHours * 20);
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

        _energy -= (int)(ts.TotalHours * 10);
        if (_energy < 0) {
            _energy = 0;
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
        }
    }

    public void Eat() {
        _hunger += 10;
        if(_hunger >= 100) {
            _hunger = 100;
        }    
    }

    public void Poop() {
        _bathroom += 10;
        if(_bathroom >= 100) {
            _bathroom = 100;
        }     
    }

    public void Play() {
        _happiness += 10;
        if (_happiness >= 100) {
            _happiness = 100;
        } 
    }

    public void Sleep() {
        _energy += 50;
        if(_energy >= 100) {
            _energy = 100;
        }   
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
