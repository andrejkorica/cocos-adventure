using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectingKeyAndChest : MonoBehaviour
{
    public static int requiredKeys;
    public int collectedKeys = 0;

    private void Start()
    {
        CountKeysInScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            collectedKeys++;
            Debug.Log("Picked up key!");

            if (collectedKeys >= requiredKeys)
            {
                CheckForChest();
            }
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            if (collectedKeys >= requiredKeys)
            {
                OpenChest();
            }
            else
            {
                Debug.Log("You need to pick up the required number of keys first!");
            }
        }
    }

    private void OpenChest()
    {
        // TODO: Animation
        var dataStorage = DataPersistenceManager.instance;
        dataStorage.SaveGame();

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(buildIndex + 1);
        SceneManager.LoadScene(buildIndex + 1);
    }

    private void CountKeysInScene()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        requiredKeys = keys.Length;
    }

    private void CheckForChest()
    {
        GameObject chest = GameObject.FindGameObjectWithTag("Chest");
        if (chest != null)
        {
            // Enable the interaction with the chest here (e.g., show a message)
        }
    }
}
