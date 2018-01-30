using System.Collections;
using UnityEngine;

public class FireWizard : MonoBehaviour
{
    private float speed = 5f;
    private float movementTimer;
    private float movementCd = 1f;

    void Start()
    {

    }

    void Update()
    {
        if (movementTimer > 0)
            movementTimer -= Time.deltaTime;
        else
        {
            movementTimer = movementCd;
            Move();
        }
    }

    public void Move()
    {
        transform.Translate(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f)) * Time.deltaTime * speed);
    }
}