using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectingKeyAndChest : MonoBehaviour
{
    private bool hasKey = true; 

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
                goToNextLevel();
            }
            else
            {
                Debug.Log("You need to pick up the key first!");
            }
        }
    }

    private void goToNextLevel() {
        // TODO Save
        // TODO Animation
        var dataStorage = DataPersistenceManager.instance;
        dataStorage.SaveGame();

        dataStorage.LoadGame();
        // int buildIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(buildIndex + 1);
    }
}
