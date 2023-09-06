using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    private int currentLevelIndex;
    private SpriteRenderer CanRenderer;
    private BoxCollider2D canCollider;
    private Light2D canLight;
    private Light2D openerLight;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Opener"))
        {
            GameObject canOpenerObject = GameObject.FindGameObjectWithTag("Opener");
            OpernerRenderer = canOpenerObject.GetComponent<SpriteRenderer>();
            openerLight = canOpenerObject.GetComponent<Light2D>();

        }

        if (GameObject.FindGameObjectWithTag("Can"))
        {

            GameObject canObject = GameObject.FindGameObjectWithTag("Can");
            CanRenderer = canObject.GetComponent<SpriteRenderer>();
            canCollider = canObject.GetComponent<BoxCollider2D>();
            canLight = canObject.GetComponent<Light2D>();

        }

    }
    private void Start()
    {


        attributesData = new AttributesData();
        playerHealth = GetComponent<PlayerHealth>();
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("LEVEL INDEX " + currentLevelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opener"))
        {
            hasOpener = true;
            OpernerRenderer.enabled = false;
            openerLight.enabled = false;
        }
        else if (collision.CompareTag("Can") && hasOpener)
        {
            playerHealth.IncreaseMaxHealth();
            CollectCan();

        }
    }

    private void CollectCan()
    {

        if (currentLevelIndex == 2 && !has1Can)
        {
            Debug.Log("has1Can");
            has1Can = true;
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }
        else if (currentLevelIndex == 3 && !has2Can)
        {
            Debug.Log("has2Can");
            has2Can = true;
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }
        else if (currentLevelIndex == 5 && !has3Can)
        {
            Debug.Log("has3Can");
            has3Can = true;
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }
        //todo: build index scene (koje je level koji je index)
        //ako je index == treci level onda pokupi prvi can, itd
        //stavim ih na true
    }

    public void LoadData(AttributesData data) //drugi spriterenderer se pali ako je taj i taj skupljen na tom levelu...samo jedan se koristi po levelu
                                              //ppomocu ifova znam koji je koji
    {
        has1Can = data.has1Can;
        has2Can = data.has2Can;
        has3Can = data.has3Can;
        hasOpener = data.hasOpener;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (hasOpener)
        {
            OpernerRenderer.enabled = false;
            openerLight.enabled = false;
        }

        if (currentLevelIndex == 2 && has1Can)
        {
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }

        if (currentLevelIndex == 3 && has2Can)
        {
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }

        if (currentLevelIndex == 5 && has3Can)
        {
            CanRenderer.enabled = false;
            canCollider.enabled = false;
            canLight.enabled = false;
        }

    }

    public void SaveData(AttributesData data)
    {
        data.has1Can = has1Can;
        data.has2Can = has2Can;
        data.has3Can = has3Can;
        data.hasOpener = hasOpener;

    }

}
