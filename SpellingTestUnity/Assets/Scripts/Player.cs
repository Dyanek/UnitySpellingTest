using UnityEngine;

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
    public Transform attackParticleSpawn;

    void Start()
    {
        CurrentHealth = MaxHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (enableMovements)
            Movement();

        if (enableShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();
            else if (animator.GetBool("Attack") == true)
                animator.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        animator.SetBool("Attack", true);
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector2(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, 2f);
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

    public void PlayerHurt (int Damage)
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
            gameObject.SetActive(false);
            // Stopping enemy wizard script
            StopScript.GetComponent<FireWizard>().enabled = false;
            StopScript.GetComponent<WaterWizard>().enabled = false;
        }
    }

    void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}