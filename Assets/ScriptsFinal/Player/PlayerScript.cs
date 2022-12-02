using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    public Rigidbody2D rb;
    public CapsuleCollider2D playerCollider;

    public Animator animator;

    public bool turnedLeft;

    // Variables concernant l'attaque
    public float attackCooldown;
    private bool isAttacking;
    private float currentCooldown;
    public float attackRange;
    public GameObject rayHit;

    public static PlayerScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rayHit = GameObject.Find("RayHit");
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
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("RunR");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
        else if (horizontal < 0)
        {
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("RunL");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            turnedLeft = true;
        }
        else if (horizontal == 0)
        {
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("Idle");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0)
        {
            currentCooldown = attackCooldown;
            isAttacking = false;
        }
    }

        // Fonction d'attaque
        public void Attack()
        {
            if (!isAttacking)
            {
               GetComponent<Animator>().Play("Attack1");

                RaycastHit hit;

                if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
                {
                    Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                    if (hit.transform.tag == "Enemy")
                    {
                        print(hit.transform.name + " detected");
                    }

                }
                isAttacking = true;
            }
        }
}
