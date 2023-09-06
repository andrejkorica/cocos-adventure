using UnityEngine;

public class LavaStartRising : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RisingLava.IsRising = true;
        }
    }
}
