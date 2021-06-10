using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 10;
    public float timeRemaining = 5;
    public bool timerIsRunning = false;
    public Text timeText;
    public Button PlayerButton;
    public Button Powerup4;
    public Button Powerup5;
    public Button Powerup6;
    public GameObject GameOverScreen;
    public GameObject Day;
    public GameObject Afternoon;
    public GameObject Night;
    public GameObject A1;
    public GameObject A2;
    public GameObject A3;
    public GameObject Achivements;
    public int i = 0;

    public HealthBar healthBar;

    public int Hitpower;
    public int dmgs;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        timerIsRunning = true;
        InvokeRepeating("HitDmgs", 1f, 1f);
        


        healthBar.GetComponent<HealthBar>().healtbarSet();
    }

    // Update is called once per frame
    void Update()
    {
        
        //timer
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                if(curHealth <= 0)
                {

                    NextMonster();
                }
            }
            else
            {
                    Debug.Log("Czas się skończył");
                    DisableButton();
                    //PauseGame();
                    timeRemaining = 0;
                    timerIsRunning = false;
                    GameOverScreen.SetActive(true);

            }
        }

        if (i > 2&& i<5)
        {
            Day.SetActive(false);
            Afternoon.SetActive(true);
            Achivements.SetActive(true);
            A1.SetActive(true);


        }
        else if (i>5&& i<8)
        {
            Afternoon.SetActive(false);
            Night.SetActive(true);
            A2.SetActive(true);

        }
        else if (i > 8)
        {
            Night.SetActive(false);
            Day.SetActive(true);
            A3.SetActive(true);


        }

    }



    public void DamagePlayer(int damage)
    {
        curHealth -= damage;

        healthBar.SetHealth(curHealth);
    }
    public void HitDmgs()
    {
        dmgs = (int)gameObject.GetComponent<Game>().x;
        DamagePlayer(dmgs);
    }
    public void Hit2()
    {
        Hitpower = (int)gameObject.GetComponent<Game>().hitPower;
        DamagePlayer(Hitpower);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisableButton()
    {
        PlayerButton.interactable = false;
        Powerup4.interactable = false;
        Powerup5.interactable = false;
        Powerup6.interactable = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }


    void NextMonster()
    {
        maxHealth = (int)(maxHealth * 1.5);
        timeRemaining = timeRemaining + 5;
        curHealth = maxHealth;
        healthBar.GetComponent<HealthBar>().SetHealth(maxHealth);
        healthBar.GetComponent<HealthBar>().healtbarSet();
        healthBar.GetComponent<HealthBar>().MaxValueSet(maxHealth);
        i++;
        
        Debug.Log("Zabiłeś potwora na czas, gratulacje");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
