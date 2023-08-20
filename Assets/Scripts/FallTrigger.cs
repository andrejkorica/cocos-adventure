using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BreakablePlatform parentScript = transform.parent.GetComponent<BreakablePlatform>();
            if (parentScript != null)
            {
                parentScript.TriggerFall();
            }
        }
    }
}
