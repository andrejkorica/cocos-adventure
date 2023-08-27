using UnityEngine;

public class OpenerAndCan : MonoBehaviour
{
    private bool hasOpener = false;
    private int numCans = 0;
    private AttributesData attributesData;
    private PlayerHealth playerHealth;

    private void Start()
    {
        attributesData = new AttributesData();
        playerHealth = GetComponent<PlayerHealth>();
        LoadAttributesData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opener"))
        {
            hasOpener = true;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Can") && hasOpener)
        {
            numCans++;
            playerHealth.IncreaseMaxHealth();
            Destroy(collision.gameObject);
        }
    }

    private void LoadAttributesData()
    {
        // Učitaj podatke iz attributesData u postojeći playerHealth
        playerHealth.LoadData(attributesData);
    }

    private void SaveAttributesData()
    {
        // Sačuvaj podatke iz playerHealth u attributesData
        playerHealth.SaveData(attributesData);
    }

    private void OnApplicationQuit()
    {
        SaveAttributesData();
    }
}
