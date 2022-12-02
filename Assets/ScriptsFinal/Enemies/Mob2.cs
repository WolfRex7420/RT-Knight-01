using UnityEngine;

public class Mob2 : MonoBehaviour
{
    public float speed=4;
    public Transform[] waypoints;

    public int damageOnCollision=8;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint = 0;

    public Rigidbody2D Srb;
    public CapsuleCollider2D SlimeCollider;

    private float range;
    
    private float minDistance = 5f;
    private bool targetCollision = false;
    
    //private float thrust = 1.5f;
    public int health = 9;

    public Animator SlimeAnimator;

    public static Mob2 S2instance;

    void Start()
    {
        target = waypoints[0];
    }
    
    public void Awake()
    {
        if (S2instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Enemy Patrol2 dans la scène");
            return;
        }

        S2instance = this;
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
        if (health <= 0)
        {
            MobDeath();
            return;
        }
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

    public void TakeDamage2(int amount)
    {
        health -= amount;
    }

    public void MobDeath()
    {
        //Mob2.Sinstance.enabled = false;
        Mob2.S2instance.SlimeAnimator.SetTrigger("Died");
        Mob2.S2instance.Srb.bodyType = RigidbodyType2D.Kinematic;
        Mob2.S2instance.Srb.velocity = Vector3.zero;
        Mob2.S2instance.SlimeCollider.enabled = false;
        Debug.Log("Mob2 eliminated");
    }
}
