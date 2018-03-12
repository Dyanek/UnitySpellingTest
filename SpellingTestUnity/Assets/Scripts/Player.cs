using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float speed = 5f;

    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;
    [SerializeField] private float bottomEdge;
    [SerializeField] private float topEdge;

    [SerializeField] private bool enableMovements;
    [SerializeField] private bool enableShoot;

    // Player Health
    [SerializeField] public int MaxHealth;
    [SerializeField] public int CurrentHealth;
    [SerializeField] public int Lives;
    [SerializeField] private Transform RespawnPoint;
    public GameObject StopScript;

    private Animator animator;

    public GameObject attackParticlePrefab;

    public int authorizedAttacks;

    private float uniqueFireAttackTimer;
    private float uniqueFireAttackCd = 1f;

    public GameObject uniqueWaterAttackMarkPrefab;
    public GameObject uniqueWaterAttackParticlePrefab;

    private float uniqueWaterAttackTimer = 0;
    private float uniqueWaterAttackCd = 2f;

    private float uniqueWaterAttackParticleTimer = 0.3f;
    private float uniqueWaterAttackParticleCd = 0.3f;

    private int uniqueWaterAttackCount = 0;
    private float attackAngle;

    private List<KeyValuePair<GameObject, Vector2>> attackParticlesList;

    public string Dead;

    void Start()
    {
        CurrentHealth = MaxHealth;

        attackParticlesList = new List<KeyValuePair<GameObject, Vector2>>();

        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //Deletes every destroyed particle from the list
        attackParticlesList = attackParticlesList.Where(kvp => kvp.Key != null).ToList();

        foreach (KeyValuePair<GameObject, Vector2> kvp in attackParticlesList)
            kvp.Key.transform.Translate(kvp.Value * Time.deltaTime * 5f);

        if (enableMovements)
            Movement();

        if (enableShoot)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Attack();
            else if (animator.GetBool("Attack") == true)
                animator.SetBool("Attack", false);

            if (authorizedAttacks > 0)
            {
                if (uniqueFireAttackTimer > 0)
                    uniqueFireAttackTimer -= Time.deltaTime;
                else if (Input.GetKeyDown(KeyCode.X))
                    FireAttack();

                if (authorizedAttacks > 1)
                {
                    if (uniqueWaterAttackTimer > 0)
                        uniqueWaterAttackTimer -= Time.deltaTime;
                    else if (Input.GetKeyDown(KeyCode.C))
                        WaterAttack();
                }
            }
        }

        if(uniqueWaterAttackCount > 0)
        {
            WaterAttack();
        }
    }

    void Attack()
    {
        animator.SetBool("Attack", true);

        Vector2 particleVector = new Vector2(0, 1.5f);

        attackParticlesList.Add(CreateAttackParticle(2f, particleVector));
    }

    void FireAttack()
    {
        animator.SetBool("Attack", true);

        uniqueFireAttackTimer = uniqueFireAttackCd;

        for (float f = -0.6f; f <= 0.6f; f += 0.2f)
        {
            Vector2 particleVector = new Vector2(f, 1.5f);

            attackParticlesList.Add(CreateAttackParticle(4f, particleVector));
        }
    }

    public KeyValuePair<GameObject, Vector2> CreateAttackParticle(float lifeSpan, Vector2 particleVector)
    {
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, lifeSpan);

        return new KeyValuePair<GameObject, Vector2>(attackParticle, particleVector);
    }

    void WaterAttack()
    {
        if (uniqueWaterAttackCount == 0)
        {
            animator.SetBool("Attack", true);

            enableMovements = false;
            enableShoot = false;

            uniqueWaterAttackTimer = uniqueWaterAttackCd;
            uniqueWaterAttackCount = 1;

            GameObject attackParticle = Instantiate(uniqueWaterAttackMarkPrefab, new Vector2(transform.position.x, transform.position.y + 0.15f), new Quaternion());

            Vector3 enemyPosition = GameObject.FindGameObjectWithTag("EnemyWizard").transform.position;

            float opposite = Mathf.Abs(enemyPosition.y - transform.position.y);
            float adjacent = Mathf.Abs(enemyPosition.x - transform.position.x);
            float hypothenuse = Mathf.Sqrt((adjacent * adjacent) + (opposite * opposite));

            attackAngle = Mathf.Atan2(opposite, adjacent) * Mathf.Rad2Deg;

            if (transform.position.x > enemyPosition.x)
                attackAngle = -attackAngle + 180;

            attackParticle.transform.Rotate(new Vector3(0, 0, attackAngle + 90));
            Destroy(attackParticle, 0.3f);
        }
        else
        {
            if (uniqueWaterAttackParticleTimer > 0)
                uniqueWaterAttackParticleTimer -= Time.deltaTime;
            else
            {
                animator.SetBool("Attack", true);

                GameObject attackParticle = Instantiate(uniqueWaterAttackParticlePrefab, new Vector2(transform.position.x, transform.position.y + 0.15f), new Quaternion());

                attackParticle.transform.Rotate(new Vector3(0, 0, attackAngle + 90));      
                Destroy(attackParticle, 0.4f);

                enableMovements = true;
                enableShoot = true;

                uniqueWaterAttackParticleTimer = uniqueWaterAttackParticleCd;
                uniqueWaterAttackCount = 0;
            }
        }
    }

    void Movement()
    {
        //Get the touch pressed
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(axisX, axisY) * Time.deltaTime * speed);

        animator.SetFloat("HorizontalSpeed", axisX);
        animator.SetFloat("VerticalSpeed", axisY);

        //Blocks movement at the edge of the screen
        if (transform.position.x < leftEdge)
            transform.position = new Vector2(leftEdge, transform.position.y);
        else if (transform.position.x > rightEdge)
            transform.position = new Vector2(rightEdge, transform.position.y);

        if (transform.position.y < bottomEdge)
            transform.position = new Vector2(transform.position.x, bottomEdge);
        else if (transform.position.y > topEdge)
            transform.position = new Vector2(transform.position.x, topEdge);
    }

    public void PlayerHurt(int Damage)
    {
        CurrentHealth -= Damage;

        if (CurrentHealth <= 0)
        {
            Lives--;
            transform.position = RespawnPoint.transform.position;
            CurrentHealth = MaxHealth;
        }

        if (Lives <= 0)
        {
            //gameObject.SetActive(false);
            //Stopping enemy wizard script
            //StopScript.GetComponent<FireWizard>().enabled = false;
            //StopScript.GetComponent<WaterWizard>().enabled = false;
            //StopScript.GetComponent<GrandWizard>().enabled = false;

            // Load Death Scene
            SceneManager.LoadScene(Dead);
        }
    }

    void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}