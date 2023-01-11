using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public SpriteRenderer PlayerGraphics;

    public PlayerHealth playerHealth;

    public float Speed;

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
    public GameObject Sword;

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
        horizontal = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
        vertical = Input.GetAxis("Vertical") * Speed * Time.fixedDeltaTime;

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        turnedLeft = false;

        if (horizontal > 0)
        {
            if (!isAttacking)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().Play("RunR");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
            }
        }
        else if (horizontal < 0)
        {
            //playerHealth.graphics.transform(180f, 0f, 0f);

            if (!isAttacking)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().Play("RunR");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
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
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
            }
        }

        else if (vertical == 0)
        {
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("Idle");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
            }
        }
        else if (vertical > 0)
        {
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("RunUp");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
            }
        }
        else if (vertical < 0)
        {
            if (!isAttacking)
            {
                GetComponent<Animator>().Play("RunDown");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().Play("Roll");
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
            
                Sword.SetActive(true);
                isAttacking = true;
            }
        }
}
