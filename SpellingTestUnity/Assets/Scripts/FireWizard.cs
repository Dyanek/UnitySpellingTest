using System.Collections;
using UnityEngine;

public class FireWizard : MonoBehaviour
{
    private float speed = 5f;
    private float movementTimer;
    private float movementCd = 1f;

    [SerializeField] public float LeftEdge;
    [SerializeField] public float RightEdge;
    [SerializeField] public float BottomEdge;
    [SerializeField] public float TopEdge;

    private Vector2 movementVector;

    public GameObject attackParticlePrefab;
    public Transform attackParticleSpawn;

    private bool isAttacking = false;
    private float attackTimer;
    private float attackCd = 1f;

    void Start()
    {
        movementTimer = movementCd;
        DefineMovementVector();

        attackTimer = attackCd;
    }

    void Update()
    {
        if (!isAttacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                Movements();
            }
            else
                isAttacking = true;
        }
        else
        {
            //for (float i = -2; i <= 2; i++)
                Attack();
            isAttacking = false;
            attackTimer = attackCd;
        }
    }

    public void Attack()
    {
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        //attackParticle.transform.Translate(new Vector2(-0.5f, 0));

        //GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, 2f);
    }

    public void Movements()
    {
        if (movementTimer > 0)
        {
            movementTimer -= Time.deltaTime;
            transform.Translate(movementVector * Time.deltaTime * speed);

            //Blocks movement at the edge of the screen
            if (transform.position.x < LeftEdge)
            {
                transform.position = new Vector2(LeftEdge, transform.position.y);
                movementVector.x = -movementVector.x;
            }
            else if (transform.position.x > RightEdge)
            {
                transform.position = new Vector2(RightEdge, transform.position.y);
                movementVector.x = -movementVector.x;
            }

            if (transform.position.y < BottomEdge)
            {
                transform.position = new Vector2(transform.position.x, BottomEdge);
                movementVector.y = -movementVector.y;
            }
            else if (transform.position.y > TopEdge)
            {
                transform.position = new Vector2(transform.position.x, TopEdge);
                movementVector.y = -movementVector.y;
            }
        }
        else
        {
            movementTimer = movementCd;
            DefineMovementVector();
        }
    }

    public void DefineMovementVector()
    {
        movementVector = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f));
    }
}