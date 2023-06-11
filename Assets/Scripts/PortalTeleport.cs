using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform[] portals;
    public Transform[] pairedPortals;

    private Rigidbody playerRigidbody;
    private bool isTeleporting;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        int portalIndex = FindPortalIndex(player);

        if (portalIndex != -1)
        {
            Vector3 pairedPortalPosition = pairedPortals[portalIndex].position;
            Vector3 teleportDirection = pairedPortalPosition - portals[portalIndex].position;
            float playerSpeed = playerRigidbody.velocity.magnitude;
            player.position = pairedPortalPosition;
            playerRigidbody.velocity = teleportDirection.normalized * playerSpeed;
        }

        isTeleporting = false;
    }

    private int FindPortalIndex(Transform portal)
    {
        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i] == portal)
            {
                return i;
            }
        }

        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

