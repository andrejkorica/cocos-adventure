using System.Collections;
using UnityEngine;


public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    public float respawnDelay = 1f;
    private Vector3 startPosition;
    private bool isFalling = false;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(Fall());
            particles.Play();
        }
        if (collision.gameObject.CompareTag("Ground") && GetComponent<SpriteRenderer>().enabled)
        {
            if(collision.gameObject.GetComponent<FallingPlatform>())
            {
                return;
            }
            else
            {
                StartCoroutine(Respawn());  
            }
            
        }
    }

    private void OnColisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particles.Stop();
        }
    }

    private IEnumerator Fall()
    {
        Debug.Log("fall");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        isFalling = true;
        //Destroy(gameObject, destroyDelay);
    }

    private IEnumerator Respawn()
    {
        particles.Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("respawn");
        yield return new WaitForSeconds(respawnDelay);
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = startPosition;
        isFalling = false;
    }

    
}