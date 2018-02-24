using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void EnemyHurt(int Damage)
    {
        CurrentHealth -= Damage;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
