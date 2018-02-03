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

    void Start()
    {
        DefineMovementVector();
    }

    void Update()
    {
        if (movementTimer > 0)
        {
            movementTimer -= Time.deltaTime;
            transform.Translate(movementVector * Time.deltaTime * speed);

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
        else
        {
            movementTimer = movementCd;
            DefineMovementVector();
        }
    }

    public void DefineMovementVector()
    {
        movementVector = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
    }

}