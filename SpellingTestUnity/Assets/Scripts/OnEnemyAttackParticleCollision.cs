using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyAttackParticleCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "EnemyAttackParticle" && collision.gameObject.tag != "EnemyWizard")
        {
            if (collision.gameObject.tag == "Player")
                collision.gameObject.GetComponent<Player>().PlayerHurt(20); // Take off health
            Destroy(gameObject);
        }
    }
}