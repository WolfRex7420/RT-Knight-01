using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;

    public bool turnedLeft = false;

    public static PlayerMovement instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        turnedLeft = false;


        if (horizontal > 0)
        {
            GetComponent<Animator>().Play("RunR");
        }
        else if (horizontal < 0)
        {
            GetComponent<Animator>().Play("RunL");
            turnedLeft = true;
        }
        else if (horizontal == 0)
        {
            GetComponent<Animator>().Play("Idle");
        }


    }
}
