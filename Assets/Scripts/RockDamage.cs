using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject particles)
    {
        if (particles.CompareTag("ParticleDamage"))
        {
            Debug.Log("COLISION");
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("PlayerHealth script found on the player character.");
                playerHealth.TakeDamage();
                playerHealth.UpdateHeartImages();
            }
            

        }
    }

}
