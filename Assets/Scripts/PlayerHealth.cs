using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;  
    public float invulnerabilityDuration = 5f;  
    public Transform spawnPoint;  

    public Image HeartImage1; 
    public Image HeartImage2;  
    public Image HeartImage3;

    private int currentHealth;  
    private bool isInvulnerable;  

    private static int previousHealth;  // Broj zivota pri prethodnom levelu

    private void Start()
    {
        currentHealth = previousHealth > 0 ? previousHealth : maxHealth;
        UpdateHeartImages();  
    }

    private void Update()
    {
        if (isInvulnerable)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if ( collider.CompareTag("Spike"))  // || collider.CompareTag("Enemy") 
            {
                TakeDamage();
                //Debug.Log("Trenutni broj života: " + currentHealth);
                UpdateHeartImages();
                break;
            }
        }
    }

    private void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private void Die()
    {
        transform.position = spawnPoint.position;
        previousHealth = currentHealth;  // Spremi trenutni broj zivota za sljedeci level
        currentHealth = maxHealth;

        UpdateHeartImages();

        //Debug.Log("Trenutni broj zivota: " + currentHealth);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPickup"))
        {
            currentHealth++;
            Destroy(collision.gameObject);

            //Debug.Log("Trenutni broj života: " + currentHealth);
            UpdateHeartImages(); 
        }
    }

    private void UpdateHeartImages()
    {

    switch (currentHealth)
    {
        case 3:
            HeartImage1.enabled = true;
            HeartImage2.enabled = true;
            HeartImage3.enabled = true;
            break;
        case 2:
            HeartImage1.enabled = true;
            HeartImage2.enabled = true;
            HeartImage3.enabled = false;
            break;
        case 1:
            HeartImage1.enabled = true;
            HeartImage2.enabled = false;
            HeartImage3.enabled = false;
            break;
        case 0:
            HeartImage1.enabled = false;
            HeartImage2.enabled = false;
            HeartImage3.enabled = false;
            break;
    }
    }
}
