using System.Collections;
using UnityEngine;

public class BarrelRolling : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasStartedRolling = false;
    private Vector3 initialPosition;

    [SerializeField] private GameObject respawnPrefab; // Prefab to respawn
    [SerializeField] private float rollForce = 10f;
    [SerializeField] private float maxSpeed = 5f; // Maximum rolling speed
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private bool rollLeft = true; // Indicates whether the object should roll left or right
    [SerializeField] private float respawnDelay = 4f; // Delay before respawn

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // Store the initial position
    }

    private void Start()
    {
        ApplyInitialForce();
    }

    private void ApplyInitialForce()
    {
        Vector2 initialRollDirection = rollLeft ? Vector2.left : Vector2.right;
        rb.AddForce(initialRollDirection * rollForce, ForceMode2D.Impulse);

        hasStartedRolling = true;
    }

    private void FixedUpdate()
    {
        if (hasStartedRolling)
        {
            Roll();

            // Check if the object has lost all speed and respawn it
            if (rb.velocity.magnitude < 0.1f)
            {
                Respawn();
            }
        }
    }

    private void Roll()
    {
        Vector2 rollDirection = rollLeft ? Vector2.left : Vector2.right;

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
            Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, normal);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * 360f);
        }
    }

    private void Respawn()
    {
        // Instantiate a new object from the prefab at the initial position of the barrel
        GameObject newObject = Instantiate(respawnPrefab, initialPosition, Quaternion.identity);

        // Destroy the current object
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
                playerHealth.UpdateHeartImages();
            }

            // Delay the respawn
            StartCoroutine(RespawnAfterDelay());
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Respawn the barrel
        Respawn();
    }
}
