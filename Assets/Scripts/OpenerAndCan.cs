using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenerAndCan : MonoBehaviour, IGlobalDataPersistance
{
    public bool hasOpener = false;
    public bool has1Can;
    public bool has2Can;
    public bool has3Can;
    private AttributesData attributesData;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Opener"))
        {
            GameObject canOpenerObject = GameObject.FindGameObjectWithTag("Opener");
            SpriteRenderer canOpenerRenderer = canOpenerObject.GetComponent<SpriteRenderer>();
        }

    
    }
    private void Start()
    {
        attributesData = new AttributesData();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opener"))
        {
            hasOpener = true;
            spriteRenderer.enabled = false;
        }
        else if (collision.CompareTag("Can") && hasOpener)
        {
            playerHealth.IncreaseMaxHealth();
            CollectCan();
            Destroy(collision.gameObject);
            //spriterenderer za canove
            //preko njega gasimo objektet
        }
    }

    private void CollectCan()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLevelIndex == 1 && !has1Can)
        {
            has1Can = true;
        }
        else if (currentLevelIndex == 3 && !has2Can)
        {
            has2Can = true;
        }
        else if (currentLevelIndex == 4 && !has3Can)
        {
            has3Can = true;
        }
        //todo: build index scene (koje je level koji je index)
        //ako je index == treci level onda pokupi prvi can, itd
        //stavim ih na true
    }

    public void LoadData(AttributesData data) //drugi spriterenderer se pali ako je taj i taj skupljen na tom levelu...samo jedan se koristi po levelu
                                              //ppomocu ifova znam koji je koji
    {
        this.has1Can = data.has1Can;
        this.has2Can = data.has2Can;
        this.has3Can = data.has3Can;
        this.hasOpener = data.hasOpener;
        GameObject canOpenerObject = GameObject.FindGameObjectWithTag("Opener");
        SpriteRenderer canOpenerRenderer = canOpenerObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = !this.hasOpener;
        Debug.Log(this.hasOpener);
    }

    public void SaveData(AttributesData data)
    {
        data.has1Can = has1Can;
        data.has2Can = has2Can;
        data.has3Can = has3Can;
        data.hasOpener = this.hasOpener;

    }

}
