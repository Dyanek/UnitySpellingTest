using UnityEngine;

public class OnPlayerRayAttackCollision : MonoBehaviour
{
    private const int PLAYER_DAMAGE = 4;

    private float attackTimer = 0.01f;

    public AudioClip wizardHitAudio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyWizard")
        {
            collision.gameObject.GetComponent<EnemyHealth>().EnemyHurt(PLAYER_DAMAGE); // Take off health
            SoundManager.instance.PlaySingle(wizardHitAudio);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyWizard")
        {
            if (attackTimer > 0)
                attackTimer -= Time.fixedDeltaTime;
            else
            {
                attackTimer = 0.01f;
                collision.gameObject.GetComponent<EnemyHealth>().EnemyHurt(PLAYER_DAMAGE); // Take off health
            }
        }
    }
}