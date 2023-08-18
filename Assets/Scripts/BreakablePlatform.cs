using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float triggerRange = 2f;
    public float fallForce = 10f;

    private bool triggered = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!triggered)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, triggerRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    TriggerPlatform();
                    break;
                }
            }
        }
    }

    private void TriggerPlatform()
    {
        triggered = true;
        rb.isKinematic = false;
        rb.AddForce(Vector2.down * fallForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground")))
        {
            Destroy(gameObject);
        }
    }
}
