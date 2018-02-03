using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5f;

    [SerializeField] public float LeftEdge;
    [SerializeField] public float RightEdge;
    [SerializeField] public float BottomEdge;
    [SerializeField] public float TopEdge;

    private Animator animator;

    public GameObject attackParticlePrefab;
    public Transform attackParticleSpawn;



    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        else if (animator.GetBool("Attack") == true)
            animator.SetBool("Attack", false);
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
        if (transform.position.x < LeftEdge)
            transform.position = new Vector2(LeftEdge, transform.position.y);
        else if (transform.position.x > RightEdge)
            transform.position = new Vector2(RightEdge, transform.position.y);

        if (transform.position.y < BottomEdge)
            transform.position = new Vector2(transform.position.x, BottomEdge);
        else if (transform.position.y > TopEdge)
            transform.position = new Vector2(transform.position.x, TopEdge);
    }
}