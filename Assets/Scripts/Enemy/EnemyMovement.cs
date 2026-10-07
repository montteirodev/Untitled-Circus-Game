using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Vision")]
    public float visionDistance = 5f;
    public LayerMask playerLayer;
    private float patrolDirection = 1f;
    public LayerMask groundLayer;
    private bool lookingAtRigh;

    [Header("Movement")]
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isPlayerInRange;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        VerifyPlayerInRange();
    }
    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
    }
    void VerifyPlayerInRange()
    {
        Vector2 direction = player.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionDistance, playerLayer);

        isPlayerInRange = hit.collider != null;
    }
    void MoveTowardsPlayer()
    {
        float targetX = player.position.x;
        float myX = transform.position.x;
        float direction = Mathf.Sign(targetX - myX);

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(direction * moveSpeed, rb.linearVelocity.y), 3f * Time.fixedDeltaTime);

    }
    void Patrol()
    {
        if (moveSpeed > 0)
        {
            lookingAtRigh = patrolDirection > 0;
            transform.localScale = new Vector3(lookingAtRigh ? 1 : -1, 1, 1);
        }
        rb.linearVelocity = new Vector2(patrolDirection * moveSpeed, rb.linearVelocity.y);
        RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right * patrolDirection, 0.5f, groundLayer);
        if (wall.collider != null)
        {
            patrolDirection *= -1f;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (player == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (player.position - transform.position).normalized * visionDistance);
    }

}
