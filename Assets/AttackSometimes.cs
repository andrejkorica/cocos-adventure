using System.Collections;
using UnityEngine;

public class AttackSometimes : MonoBehaviour
{
    private Animator anim;
    public float DamageDelay = 1f; // Change to 4 seconds for periodic attacks
    private bool CantDealDamage = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PeriodicAttack());
    }

    private IEnumerator PeriodicAttack()
    {
        while (true) // Infinite loop for periodic attacks
        {


            if (!CantDealDamage)
            {
                StartCoroutine(AttackPlayer());
            }

            yield return new WaitForSeconds(DamageDelay);
        }
    }

    private IEnumerator AttackPlayer()
    {
        anim.SetBool("canAttack", true);

        yield return new WaitForSeconds(1f); // Adjust this delay for the attack animation if needed

        anim.SetBool("canAttack", false);
    }

    private IEnumerator DelayDamage()
    {
        CantDealDamage = true;
        yield return new WaitForSeconds(DamageDelay);
        CantDealDamage = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!CantDealDamage && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();
            if (playerHealth != null)
            {
                StartCoroutine(DelayDamage());
                StartCoroutine(AttackPlayer());

                playerHealth.TakeDamage();
                playerHealth.UpdateHeartImages();
            }
        }
    }

}
