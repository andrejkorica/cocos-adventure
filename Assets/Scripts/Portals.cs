using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portals : MonoBehaviour
{
    public Transform otherportal;
    private bool coolDownActive = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !coolDownActive)
        {
            collision.transform.position = otherportal.position;
            StartCoroutine(coolDown());
            otherportal.GetComponent<Portals>().StartCoroutine("coolDown");
        }
    }

    public IEnumerator coolDown()
    {
        coolDownActive = true;
        yield return new WaitForSeconds(2f);
        coolDownActive = false;
    }
}
