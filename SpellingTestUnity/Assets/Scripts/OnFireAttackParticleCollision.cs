using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFireAttackParticleCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "attackParticle")
            Destroy(gameObject);
    }
}
