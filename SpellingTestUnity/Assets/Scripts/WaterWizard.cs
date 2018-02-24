using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterWizard : MonoBehaviour
{

    private float speed = 5f;
    private float movementTimer;
    private float movementCd = 1f;

    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;
    [SerializeField] private float bottomEdge;
    [SerializeField] private float topEdge;

    //private Animator animator;

    private Vector2 movementVector;

    //Basic attack particles
    public GameObject attackParticlePrefab;
    public Transform attackParticleSpawn;

    //Unique attack mark (before the real attack is launched)
    public GameObject uniqueAttackMarkPrefab;
    public Transform uniqueAttackMarkSpawn;

    //Unique attack particle
    public GameObject uniqueAttackParticlePrefab;
    public Transform uniqueAttackParticleSpawn;

    private bool isAttacking = false;
    private float attackTimer;
    private float attackCd = 1f;
    private float basicAttacksCount = 0;
    private float uniqueAttackTimer;
    private float uniqueAttackCd = 0.3f;
    private float uniqueAttackCount = 0;

    private List<KeyValuePair<GameObject, Vector2>> attackParticlesList;

    void Start()
    {
        movementTimer = movementCd;
        movementVector = DefineMovementVector();

        attackTimer = attackCd;

        attackParticlesList = new List<KeyValuePair<GameObject, Vector2>>();

        //animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //Deletes every destroyed particle from the list
        attackParticlesList = attackParticlesList.Where(kvp => kvp.Key != null).ToList();

        foreach (KeyValuePair<GameObject, Vector2> kvp in attackParticlesList)
            kvp.Key.transform.Translate(kvp.Value * Time.deltaTime * 5f);

        if (!isAttacking && attackTimer > 0)
        {
            //if (animator.GetBool("Attack"))
            //    animator.SetBool("Attack", false);

            Movements();
            attackTimer -= Time.deltaTime;
        }
        else
        {
            if (!isAttacking && basicAttacksCount < 3)
            {
                BasicAttack();
                attackTimer = attackCd;
            }
            else
            {
                basicAttacksCount = 0;

                if (!isAttacking)
                    isAttacking = true;

                if (uniqueAttackTimer > 0)
                    uniqueAttackTimer -= Time.deltaTime;
                else
                {
                    UniqueAttack();
                    uniqueAttackTimer = uniqueAttackCd;
                }

                if (uniqueAttackCount == 3)
                {
                    uniqueAttackCount = 0;
                    attackTimer = attackCd;
                    isAttacking = false;
                }
            }
        }
    }

    public void BasicAttack()
    {
        //animator.SetBool("Attack", true);

        basicAttacksCount++;

        //The basic attack particle direction is defined by [player's position - firewizard position]. The particle's speed is defined in the Update function
        Vector2 particleVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        particleVector.x /= 3;
        particleVector.y /= 3;

        attackParticlesList.Add(CreateAttackParticle(2f, particleVector));
    }

    public void UniqueAttack()
    {
        //animator.SetBool("Attack", true);

        uniqueAttackCount++;

        Vector2 particleVector = new Vector2(0, 0);

        GameObject attackParticle = Instantiate(uniqueAttackMarkPrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        //attackParticle.transform.Rotate(new Vector3(0, 0, -36f));
        //*12.75

        //float xDifference;
        //if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        //    xDifference = GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x;
        //else
        //    xDifference = transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x;

        float xDifference;

        xDifference  =/* (GameObject.FindGameObjectWithTag("Player").transform.position.y - transform.position.y) /*/ (GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x);

        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            xDifference = -xDifference;

        attackParticle.transform.Rotate(new Vector3(0, 0, xDifference));

        Destroy(attackParticle, 2f);

        attackParticlesList.Add(new KeyValuePair<GameObject, Vector2>(attackParticle, particleVector));
    }

    public KeyValuePair<GameObject, Vector2> CreateAttackParticle(float lifeSpan, Vector2 particleVector)
    {
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, lifeSpan);

        return new KeyValuePair<GameObject, Vector2>(attackParticle, particleVector);
    }

    public void Movements()
    {
        if (movementTimer > 0)
        {
            //animator.SetFloat("HorizontalSpeed", movementVector.x);
            //animator.SetFloat("VerticalSpeed", movementVector.y);

            movementTimer -= Time.deltaTime;
            transform.Translate(movementVector * Time.deltaTime * speed);

            //Blocks movement at the edge of the screen
            if (transform.position.x < leftEdge)
            {
                transform.position = new Vector2(leftEdge, transform.position.y);
                movementVector.x = -movementVector.x;
            }
            else if (transform.position.x > rightEdge)
            {
                transform.position = new Vector2(rightEdge, transform.position.y);
                movementVector.x = -movementVector.x;
            }

            if (transform.position.y < bottomEdge)
            {
                transform.position = new Vector2(transform.position.x, bottomEdge);
                movementVector.y = -movementVector.y;
            }
            else if (transform.position.y > topEdge)
            {
                transform.position = new Vector2(transform.position.x, topEdge);
                movementVector.y = -movementVector.y;
            }
        }
        else
        {
            movementTimer = movementCd;
            movementVector = DefineMovementVector();
        }
    }

    public Vector2 DefineMovementVector()
    {
        return new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f));
    }
}
