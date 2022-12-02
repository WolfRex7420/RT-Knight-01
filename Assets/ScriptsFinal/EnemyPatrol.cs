using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint = 0;

    public Rigidbody2D Srb;
    public CapsuleCollider2D SlimeCollider;

    private float range;
    
    private float minDistance = 5f;
    private bool targetCollision = false;
    
    //private float thrust = 1.5f;
    public int health = 10;

    public Animator SlimeAnimator;

    public static EnemyPatrol Sinstance;

    void Start()
    {
        target = waypoints[0];
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

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void MobDeath()
    {
        //EnemyPatrol.Sinstance.enabled = false;
        EnemyPatrol.Sinstance.SlimeAnimator.SetTrigger("Died");
        EnemyPatrol.Sinstance.Srb.bodyType = RigidbodyType2D.Kinematic;
        EnemyPatrol.Sinstance.Srb.velocity = Vector3.zero;
        EnemyPatrol.Sinstance.SlimeCollider.enabled = false;
        Debug.Log("Enemy eliminated");
    }
}
