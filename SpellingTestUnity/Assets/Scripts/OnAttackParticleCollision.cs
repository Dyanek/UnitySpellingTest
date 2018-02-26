using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackParticleCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "AttackParticle" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyHealth>().EnemyHurt(50);     // Take off health
            Debug.Log("enemy hit");
        }
            
    }
}