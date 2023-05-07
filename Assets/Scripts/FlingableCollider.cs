using UnityEngine;

public class FlingableCollider : MonoBehaviour
{
    public float flingForce = 10f;
    public float flingForceMultiplier = 2f;
    [SerializeField] int directionChangeUpDown = 1;
    [SerializeField] int directionChangeLeftRight = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        var playerRb = other.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 flingDirection = GetFlingDirection();

            float flingMagnitude = flingForce * flingForceMultiplier;

            playerRb.AddForce(flingDirection * flingMagnitude, ForceMode2D.Impulse);
        }
    }

    private Vector2 GetFlingDirection()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        float width = collider.size.x;
        float height = collider.size.y;

        if (width > height)
        {
            return transform.right * directionChangeLeftRight;
        }
        else
        {
            return transform.up * directionChangeUpDown;
        }
    }
}
