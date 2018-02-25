using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyAttackParticleCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "AttackParticle" && collision.gameObject.tag != "EnemyWizard")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Player>().PlayerHurt(20);   // Take off health
            // Debug.Log("basic attack hit");
        }
    }
}