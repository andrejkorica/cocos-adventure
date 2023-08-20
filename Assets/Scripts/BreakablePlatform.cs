using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float fallForce = 10f;

    private bool triggered = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void TriggerFall()
    {
        if (!triggered)
        {
            triggered = true;
            rb.isKinematic = false;
            rb.AddForce(Vector2.down * fallForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground")))
        {
            Destroy(gameObject);
        }
    }
}
