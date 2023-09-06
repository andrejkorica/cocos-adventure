using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class CompassController : MonoBehaviour, IGlobalDataPersistance
{
    public static bool hasCompass;
    public static Image[] images;
    private GameObject CompassObject;


    void Start()
    {        
        
        CompassObject = GameObject.FindGameObjectWithTag("WholeCompass");
        images = CompassObject.GetComponentsInChildren<Image>();


        if (CompassObject = GameObject.FindGameObjectWithTag("WholeCompass"))
        {
            Debug.Log("NASAO");
        } else Debug.Log("NISAM");
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCompass)
        {
            var scripts = FindObjectsOfType<CoinCollectScript>();
            List<float> distances = new List<float>();
            List<GameObject> objects = new List<GameObject>();

            for (int i = 0; i < scripts.Length; i++)
            {
                bool exists = (scripts[i].gameObject.GetComponent<SpriteRenderer>()).enabled;
                if (!exists)
                {
                    continue;
                }

                float x = scripts[i].gameObject.transform.position.x;
                float y = scripts[i].gameObject.transform.position.y;

                float playerX = transform.position.x;
                float playerY = transform.position.y;

                distances.Add(Mathf.Sqrt(Mathf.Pow((x - playerX), 2) + Mathf.Pow((y - playerY), 2)));
                objects.Add(scripts[i].gameObject);
            }

            int minIndex = distances.IndexOf(distances.Min());
            float angle = getAngle(objects[minIndex]);

            GameObject compass = GameObject.FindGameObjectsWithTag("Compass")[0];
            compass.transform.eulerAngles = new Vector3(0, 0, angle + 90 + 180);
        }
    }

    float getAngle(GameObject gameObject)
    {
        return Mathf.Atan2(
            (gameObject.transform.position.y - transform.position.y),
            (gameObject.transform.position.x - transform.position.x)
        ) * (180 / Mathf.PI);
    }

   

    public void LoadData(AttributesData data)
    {
        Debug.Log("Load compass");
        Debug.Log(data.hasCompass);
        hasCompass = data.hasCompass;

        CompassObject = GameObject.FindGameObjectWithTag("WholeCompass");
        images = CompassObject.GetComponentsInChildren<Image>();


        if (CompassObject = GameObject.FindGameObjectWithTag("WholeCompass"))
        {
            Debug.Log("NASAO");
        }
        else Debug.Log("NISAM");

        foreach (Image image in images)
        {
            if (image != null)
            {
                image.enabled = data.hasCompass;
            }
        }
    }

    public void SaveData(AttributesData data)
    {
    }
}
