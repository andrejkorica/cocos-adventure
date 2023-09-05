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
    private SpriteRenderer OpernerRenderer;

    private SpriteRenderer CanRenderer1;
    private BoxCollider2D canCollider1;

    private SpriteRenderer CanRenderer2;
    private BoxCollider2D canCollider2;

    private SpriteRenderer CanRenderer3;
    private BoxCollider2D canCollider3;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Opener"))
        {
            GameObject canOpenerObject = GameObject.FindGameObjectWithTag("Opener");
            OpernerRenderer = canOpenerObject.GetComponent<SpriteRenderer>();

            
        }

        if (GameObject.FindGameObjectWithTag("Can1"))
        {

            GameObject canObject1 = GameObject.FindGameObjectWithTag("Can1");
            CanRenderer1 = canObject1.GetComponent<SpriteRenderer>();
            canCollider1 = canObject1.GetComponent<BoxCollider2D>();

        }

        if (GameObject.FindGameObjectWithTag("Can2"))
        {
            GameObject canObject2 = GameObject.FindGameObjectWithTag("Can2");
            CanRenderer2 = canObject2.GetComponent<SpriteRenderer>();
            canCollider2 = canObject2.GetComponent<BoxCollider2D>();

        }
        if (GameObject.FindGameObjectWithTag("Can3"))
        {
            GameObject canObject3 = GameObject.FindGameObjectWithTag("Can3");
            CanRenderer3 = canObject3.GetComponent<SpriteRenderer>();
            canCollider3 = canObject3.GetComponent<BoxCollider2D>();
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
            OpernerRenderer.enabled = false;
        }
        else if (collision.CompareTag("Can1") || collision.CompareTag("Can2") || collision.CompareTag("Can3") && hasOpener)
        {
            playerHealth.IncreaseMaxHealth();
            CollectCan();
            
        }
    }

    private void CollectCan()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLevelIndex == 1 && !has1Can)
        {
            has1Can = true;
            CanRenderer1.enabled = false;
            canCollider1.enabled = false;
        }
        else if (currentLevelIndex == 3 && !has2Can)
        {
            has2Can = true;
            CanRenderer2.enabled = false;
            canCollider2.enabled = false;
        }
        else if (currentLevelIndex == 4 && !has3Can)
        {
            has3Can = true;
            CanRenderer3.enabled = false;
            canCollider3.enabled = false;
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

        this.OpernerRenderer.enabled = !this.hasOpener;
        this.CanRenderer1.enabled = !this.has1Can;
        this.canCollider1.enabled = !this.has1Can;

        this.CanRenderer2.enabled = !this.has2Can;
        this.canCollider2.enabled = !this.has2Can;

        this.CanRenderer3.enabled = !this.has3Can;
        this.canCollider3.enabled = !this.has3Can;
    }

    public void SaveData(AttributesData data)
    {
        data.has1Can = has1Can;
        data.has2Can = has2Can;
        data.has3Can = has3Can;
        data.hasOpener = this.hasOpener;

    }

}
