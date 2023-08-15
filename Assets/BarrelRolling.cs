using UnityEngine;

public class BarrelRolling : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isRolling = false;

    public float destroyDelay = 10f; // Adjust the time before destruction
    public float rollingForce = 10f; // Adjust the rolling force
    public float maxRollingSpeed = 5f; // Maximum rolling speed

    public bool rollRightByDefault = true; // Set this to control the initial rolling direction

    private Vector2 initialPosition; // Store the initial position of the barrel

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Set gravity scale to 0 to keep the barrel stuck to the ground

        // Store the initial position of the barrel
        initialPosition = rb.position;

        // Start the initial respawn and rolling routines
        InvokeRepeating("RespawnBarrel", 0f, 2f);
    }

    private void Update()
    {
        if (isRolling)
        {
            RollBarrel();
        }
    }

    private void RespawnBarrel()
    {
        // Reset the barrel's position and rotation
        rb.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0; // Reset gravity scale to 0 before rolling
        transform.rotation = Quaternion.identity;

        // Start the rolling routine after respawning
        isRolling = true;
        Invoke("StopRolling", 3f);

        // Destroy the barrel after the specified delay
        Invoke("DestroyBarrel", destroyDelay);
    }

    private void RollBarrel()
    {
        // Determine the rolling direction based on the rollRightByDefault setting
        Vector2 rollingDirection = rollRightByDefault ? Vector2.right : Vector2.left;

        // Limit the barrel's velocity to the maximum rolling speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxRollingSpeed);

        // Apply rolling force to the barrel based on the current rolling speed
        rb.AddForce(rollingDirection * (rollingForce - rb.velocity.magnitude), ForceMode2D.Impulse);

        // Enable gravity to affect the barrel once it starts rolling
        rb.gravityScale = 1;
    }

    private void StopRolling()
    {
        // Stop rolling the barrel
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0; // Disable gravity after rolling stops
        isRolling = false;
    }

    private void DestroyBarrel()
    {
        // Destroy the barrel GameObject
        Destroy(gameObject);
    }
}
