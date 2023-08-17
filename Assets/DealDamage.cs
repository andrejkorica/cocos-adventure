using System.Collections;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float DamageDelay = 2f;
    private bool CantDealDamage = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !CantDealDamage)
        {
            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                StartCoroutine(DelayDamage());
                playerHealth.TakeDamage();
                playerHealth.UpdateHeartImages();
            }
        }
    }

    IEnumerator DelayDamage()
    {
        CantDealDamage = true; 
        yield return new WaitForSeconds(DamageDelay);
        CantDealDamage = false;
    }

}
