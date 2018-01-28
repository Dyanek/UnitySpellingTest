using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5f;

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
            Fire();
        else if (animator.GetBool("Attack") == true)
            animator.SetBool("Attack", false);
    }

    void Fire()
    {
        animator.SetBool("Attack", true);
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector3(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, 2f);
    }

    void Movement()
    {
        //Get the touch pressed
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);

        animator.SetFloat("HorizontalSpeed", axisX);
        animator.SetFloat("VerticalSpeed", axisY);

        //Blocks movement at the edge of the screen
        if (transform.position.x < -4.9f)
            transform.position = new Vector2(-4.9f, transform.position.y);
        else if (transform.position.x > 4.9f)
            transform.position = new Vector2(4.9f, transform.position.y);

        if (transform.position.y < -0.8f)
            transform.position = new Vector2(transform.position.x, -0.8f);
        else if (transform.position.y > 4.8f)
            transform.position = new Vector2(transform.position.x, 4.8f);
    }
}