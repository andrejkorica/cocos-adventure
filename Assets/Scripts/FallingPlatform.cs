using System.Collections;
using UnityEngine;


public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float respawnDelay = 1f;
    private Vector3 startPosition;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
            particles.Play();
        }
        if (collision.gameObject.CompareTag("Ground") && GetComponent<SpriteRenderer>().enabled)
        {
            StartCoroutine(Respawn());
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
    }

    
}