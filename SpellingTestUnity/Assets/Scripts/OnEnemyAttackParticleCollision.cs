using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyAttackParticleCollision : MonoBehaviour {

    public int FireDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "AttackParticle" && collision.gameObject.tag != "EnemyWizard")
        {
            Destroy(gameObject);

            // Damage
            FireDamage = 20;
            collision.gameObject.GetComponent<PlayerHealth>().PlayerHurt(FireDamage);
            Debug.Log("fire hit");
        }
    }
}