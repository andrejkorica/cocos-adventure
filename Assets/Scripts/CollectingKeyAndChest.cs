using UnityEngine;

public class CollectingKeyAndChest : MonoBehaviour
{
    private bool hasKey = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject); 
            hasKey = true; 
            Debug.Log("Picked up key!");
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            if (hasKey)
            {
                Debug.Log("Moving to next level!");
                // implementacija koda koji vodi na sljedeci level
            }
            else
            {
                Debug.Log("You need to pick up the key first!");
            }
        }
    }
}
