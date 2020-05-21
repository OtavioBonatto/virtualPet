using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetController : MonoBehaviour {

    public static PetController instance;

    public float _hunger;
    public float _happiness;
    public int _bathroom;
    public float _energy;

  
    public float _weigth;
    public int _age;
    public string _health;
    public string _name;

    public int hungerTicketRate;
    public int happinessTicketRate;
    public int bathroomTicketRate;
    public int energyTicketRate;

    private bool _serverTime;

    public InventoryController inventory;

    public int money;
    public bool hungry;
    private DateTime lastDay = DateTime.MinValue;

    public GameObject heart;
    public Transform heartPosition;

    public Animator theAnim;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        theAnim = GetComponent<Animator>();

        //PlayerPrefs.SetString("then", "03/17/2020 23:26:00");
        UpdateStatus();
        if(PetUIController.instance != null) {
            PetUIController.instance.UpdateWeigth(_weigth, _age, _health, _name);
        }        
    }

    // Update is called once per frame
    void Update() {

        if(TimeManager.gameHourTimer < 0) {
            if(_hunger > 0) _hunger -= hungerTicketRate;
            if(_happiness > 0) _happiness -= happinessTicketRate;
            if(_bathroom > 0) _bathroom -= bathroomTicketRate;
            if(_energy > 0) _energy -= energyTicketRate;
        }

        //actions
        if(_bathroom < 10) {
            PoopController.instance.SpawnPoop();
            _bathroom = 100;
            _happiness -= 50;
            if(_happiness < 0) {
                _happiness = 0;
            }
        }

        //fome
        if(_hunger < 30) {
            hungry = true;
        } else {
            hungry = false;
        }

        //saúde
        if(_hunger <= 1) {
            _health = "Ruim";
        }

        //idade
        if (lastDay.AddDays(1) > DateTime.Now) {
            // increase the age
            _age++;
            // Store for next time
            lastDay = DateTime.Now;
        }

        //death
        //_weigth too low or too high
        if (_weigth >= 50) {
            gameObject.SetActive(false);
        }

        if(_weigth <= 0) {
            gameObject.SetActive(false);
        }

        //0 happiness for too long
        if(_happiness <= 0) {
            gameObject.SetActive(false);
        }

        //interface
        if (PetUIController.instance != null) {
            PetUIController.instance.UpdateImages(_hunger, _happiness, _bathroom, _energy);
            PetUIController.instance.UpdateWeigth(_weigth, _age, _health, _name);
        }
        
        if(MoneyUIController.instance != null) {
            MoneyUIController.instance.UpdateMoney(money);
        }

        //click love animation
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit != false && hit.collider != null && hit.collider.CompareTag("Pet")) {
                if(heart != null) {
                    AudioManager.instance.PlaySFX(4);
                    var heartBox = Instantiate(heart, heartPosition.transform.position, heartPosition.transform.rotation);
                    _happiness++;
                    if(_happiness > 100) {
                        _happiness = 100;
                    }
                    Destroy(heartBox, 1);
                }
                
            }
        }


        theAnim.SetBool("Hungry", hungry);
    }

    void UpdateStatus() {

        if(!PlayerPrefs.HasKey("_hunger")) {
            _hunger = 60;
            PlayerPrefs.SetFloat("_hunger", _hunger);
        } else {
            _hunger = PlayerPrefs.GetFloat("_hunger");            
        }

        if (!PlayerPrefs.HasKey("_happiness")) {
            _happiness = 90;
            PlayerPrefs.SetFloat("_happiness", _happiness);
        } else {
            _happiness = PlayerPrefs.GetFloat("_happiness");
        }

        if (!PlayerPrefs.HasKey("_bathroom")) {
            _bathroom = 100;
            PlayerPrefs.SetInt("_bathroom", _bathroom);
        } else {
            _bathroom = PlayerPrefs.GetInt("_bathroom");
        }

        if (!PlayerPrefs.HasKey("_energy")) {
            _energy = 100;
            PlayerPrefs.SetFloat("_energy", _energy);
        } else {
            _energy = PlayerPrefs.GetFloat("_energy");
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

        if (!PlayerPrefs.HasKey("_name")) {
            _name = "Buba";
            PlayerPrefs.SetString("_name", _name);
        } else {
            _name = PlayerPrefs.GetString("_name");
        }

        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then", GetStringTime());
        }

        TimeSpan ts = GetTimeSpan();

        _hunger -= (float)(ts.TotalHours * 15);
        if(_hunger < 0) {
            _hunger = 0;
        }

        _happiness -= (float)((100 - _hunger) * (ts.TotalHours / 5));
        if (_happiness < 0) {
            _happiness = 0;
        }

        _bathroom -= (int)(ts.TotalHours * 15);
        if (_bathroom < 0) {
            _bathroom = 0;
        }

        //esvazia o pote de água
        if(ts.TotalHours >= 1) {
            FillBowl.instance.EmptyBowl();
        }        

        if(LightController.instance != null) {
            if (LightController.instance.toggleLight.isOn == true) {
                _energy -= (float)(ts.TotalHours * 10);
                if (_energy < 0) {
                    _energy = 0;
                }
            } else {
                _energy += (float)(ts.TotalHours * 10);
                if (_energy > 100) {
                    _energy = 100;
                }
            }
        }

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

    public TimeSpan GetTimeSpan() {
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

    public float Hunger {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public float Happiness {
        get { return _happiness; }
        set { _happiness = value; }
    }

    public int Bathroom {
        get { return _bathroom; }
        set { _bathroom = value; }
    }

    public float Energy {
        get { return _energy; }
        set { _energy = value; }
    }  

    public void SavePet() { 
        if(!_serverTime) {
            UpdateDevice();
            PlayerPrefs.SetFloat("_hunger", _hunger);
            PlayerPrefs.SetFloat("_happiness", _happiness);
            PlayerPrefs.SetInt("_bathroom", _bathroom);
            PlayerPrefs.SetFloat("_energy", _energy);
            PlayerPrefs.SetFloat("_weigth", _weigth);
            PlayerPrefs.SetString("_health", _health);
            PlayerPrefs.SetString("_name", _name);
            UpdateMoney();
        }
    }

    public void Eat(int hungerRecover) {
        _hunger += hungerRecover;
        if(_hunger > 100) {
            _hunger = 100;
        }
    }

    public void Play() {
        //increase pet fun
        //0.4 of happiness per second
        _happiness += 0.4f * Time.deltaTime;
        if (_happiness >= 100) {
            _happiness = 100;
        }

        //increase hungry
        _hunger -= 0.2f * Time.deltaTime;
        if (_hunger <= 0) {
            _hunger = 0;
        }        

        //gastar 30 gramas em 100 segundos
        _weigth -= 0.003f * Time.deltaTime;
        if (_weigth <= 1) {
            _weigth = 1;
        }

        //decrease energy
        _energy -= 0.1f * Time.deltaTime;
        if (_energy <= 0) {
            _energy = 0;            
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
