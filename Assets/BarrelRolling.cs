using UnityEngine;

public class BarrelRolling : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float rollForce = 10f;
    [SerializeField] private float maxSpeed = 5f; // Maximum rolling speed
    [SerializeField] private float raycastDistance = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Roll();
    }

    private void Roll()
    {
        Vector2 rollDirection = Vector2.left; // You can adjust this direction as needed

        // Apply rolling force
        rb.AddForce(rollDirection * rollForce, ForceMode2D.Force);

        // Limit rolling velocity to max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Apply gravity
        rb.AddForce(Vector2.down * rb.mass * Physics2D.gravity.magnitude, ForceMode2D.Force);

        // Check for obstacles and adjust rotation
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rollDirection, raycastDistance);
        if (hit.collider != null)
        {
            Vector2 normal = hit.normal;
            Vector2 adjustedDirection = Vector2.Perpendicular(normal).normalized;
            float angle = Mathf.Atan2(adjustedDirection.y, adjustedDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
