using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1 : MonoBehaviour
{
    public GameObject slimeEnemy;

    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision;

    public float invincibilityTimeAfterHit = 2f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint = 0;

    public Rigidbody2D Srb;
    public CapsuleCollider2D SlimeCollider;

    private float range;
    
    private float minDistance = 5f;
    private bool targetCollision = false;
    
    //private float thrust = 1.5f;
    public int health;

    public Animator SlimeAnimator;

    public static Mob1 instance;

    void Start()
    {
        target = waypoints[0];
    }
    
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Enemy Patrol1 dans la scène");
            return;
        }

        instance = this;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // if the enemy is almost at destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;

        }

        range = Vector2.Distance(transform.position, target.position);
        if (range < minDistance)
        {
            if (!targetCollision)
            {
                // Get the position of the player
                transform.LookAt(target.position);

                // Correct the rotation
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !targetCollision)
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);

            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (right) GetComponent<Rigidbody2D>().AddForce(transform.right, ForceMode2D.Impulse);
            if (left) GetComponent<Rigidbody2D>().AddForce(-transform.right, ForceMode2D.Impulse);
            if (top) GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Impulse);
            if (bottom) GetComponent<Rigidbody2D>().AddForce(-transform.up, ForceMode2D.Impulse);
            Invoke("FalseCollision", 0.35f);
        }
    }

    void FalseCollision()
    {
        targetCollision = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            //AudioManager.instance.PlayClipAt(hitSound, transform.position);
            health -= damage;

            if (health <= 0)
            {
                MobDeath();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void MobDeath()
    {
        Mob1.instance.enabled = false;
        Mob1.instance.SlimeAnimator.SetTrigger("BDeath");
        Mob1.instance.Srb.bodyType = RigidbodyType2D.Kinematic;
        Mob1.instance.Srb.velocity = Vector3.zero;
        Mob1.instance.SlimeCollider.enabled = false;
        Mob1.instance.graphics.enabled = false;
        Debug.Log("Mob eliminated");
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
