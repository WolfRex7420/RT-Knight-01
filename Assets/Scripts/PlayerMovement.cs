using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.0f;
  
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontal;
    private float vertical;

    public bool turnedLeft = false;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        vertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime;

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        //animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate()
    {
        MovePlayer(horizontal, vertical);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
