using UnityEngine;

public class PirateHitbox : MonoBehaviour
{
    private CrewController controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<CrewController>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !controller.isAttacking && !controller.inCooldown)
        {
            StartCoroutine(controller.AttackPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.inCooldown = false;
        }
    }
}
