using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 7;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get the touch pressed
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
        
        //Blocks movement at the edge of the screen
        if (transform.position.x <= -10.6f)
            transform.position = new Vector2(-10.6f, transform.position.y);
        else if (transform.position.x >= 10.6f)
            transform.position = new Vector2(10.6f, transform.position.y);

        if (transform.position.y <= -4.4f)
            transform.position = new Vector2(transform.position.x, -4.4f);
        else if (transform.position.y >= 4.4f)
            transform.position = new Vector2(transform.position.x, 4.4f);       
    }
}
