using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyRayAttackCollision : MonoBehaviour
{
    private const int PLAYER_DAMAGE = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().PlayerHurt(PLAYER_DAMAGE); // Take off health
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().PlayerHurt(PLAYER_DAMAGE); // Take off health
    }
}
