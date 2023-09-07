using UnityEngine;


public class LavaRiseFaster : MonoBehaviour
{
    public float RisingSpeedMultiplyer = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RisingLava.speed += RisingSpeedMultiplyer;
        }
    }
}
