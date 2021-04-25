using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider slider;
    public float lightHealth = 100;
    public PlayerMovement movement;

    public GameObject deathScreen;
    public GameObject endingScreen;
    public Text endingText;

    DateTime startTime;

    public TimeSpan timeSince;
    public Text timeText;
    public Text endingTime;

    private GameManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        startTime = DateTime.Now;
    }

    private void Update()
    {
        if(!manager.gameEnded)
        {
            timeSince = DateTime.Now - startTime;
            string text = String.Format("{0:00}:{1:00}", timeSince.Minutes, timeSince.Seconds);

            text += "." + timeSince.Milliseconds;
            timeText.text = text;
        }
        if (manager.gameStart && !manager.gameEnded)
        {
            lightHealth -= Time.deltaTime * 4;
            slider.value = lightHealth;
            if (lightHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        deathScreen.SetActive(true);
        movement.enabled = false;
        manager.gameEnded = true;
    }

    public void Ending(int ending, string name)
    {
        movement.enabled = false;
        manager.gameEnded = true;

        endingScreen.SetActive(true);

        bool newEnding = PlayerPrefs.GetString("Ending_" + ending) != "T";
        endingTime.text = "Time: " + timeText.text;
        if (newEnding)
        {
            PlayerPrefs.SetString("Ending_" + ending, "T");
            int endingsFound = PlayerPrefs.GetInt("endingsFound", 0);
            PlayerPrefs.SetInt("endingsFound", endingsFound + 1);
            
            endingText.text = "New ending! " + name + " (" + endingsFound + "/1)!";
        }
        else
        {
            endingText.text = name + " ending, already unlocked!";
        }
    }

    public void AddLight(float light)
    {
        lightHealth += light;

        if (lightHealth > 100)
            lightHealth = 100f;

        slider.value = lightHealth;
    }
}
