using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackParticleCollision : MonoBehaviour {

    public int PlayerDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "AttackParticle"  && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);

            // Damage player gives
            PlayerDamage = 50;
            collision.gameObject.GetComponent<EnemyHealth>().EnemyHurt(PlayerDamage);
            Debug.Log("enemy hit");
        }
            
    }
}