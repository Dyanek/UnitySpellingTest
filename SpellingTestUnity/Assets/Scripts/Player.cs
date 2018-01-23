using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 7;

    public GameObject attackParticlePrefab;
    public Transform attackParticleSpawn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    void Fire()
    {
        GameObject attackParticle = Instantiate(attackParticlePrefab, new Vector3(transform.position.x, transform.position.y), new Quaternion());
        Destroy(attackParticle, 2f);
    }

    void Movement()
    {
        //Get the touch pressed
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);

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