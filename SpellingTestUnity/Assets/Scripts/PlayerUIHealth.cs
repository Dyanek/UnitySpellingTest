using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHealth : MonoBehaviour {

    public Slider HealthBar;
    public Text HealthText;
    public Player PlayerHealth;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HealthBar.maxValue = PlayerHealth.MaxHealth;
        HealthBar.value = PlayerHealth.CurrentHealth;
        HealthText.text = PlayerHealth.CurrentHealth + " / 100\nLives: " + PlayerHealth.Lives;
	}
}
