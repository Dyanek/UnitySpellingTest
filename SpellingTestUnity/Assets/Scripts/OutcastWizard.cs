using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutcastWizard : MonoBehaviour
{
    private float speed = 5f;
    private float movementTimer;
    private float movementCd = 1f;

    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;
    [SerializeField] private float bottomEdge;
    [SerializeField] private float topEdge;

    private Animator animator;

    private Vector2 movementVector;

    public GameObject attackParticlePrefab;

    private List<KeyValuePair<GameObject, Vector2>> attackParticlesList;

    void Start()
    {
        movementTimer = movementCd;
        movementVector = DefineMovementVector();

        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Movements();
    }


    public void Movements()
    {
        if (movementTimer > 0)
        {
            animator.SetFloat("HorizontalSpeed", movementVector.x);
            animator.SetFloat("VerticalSpeed", movementVector.y);

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