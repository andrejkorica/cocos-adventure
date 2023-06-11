using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public float BackgroundShiftAmount = 1f;
    public float MiddlegroundShiftAmount = 1f;
    public float ForegroundShiftAmount = 1f;

    public GameObject Camera;
    public GameObject Background;
    public GameObject Middleground;
    public GameObject Foreground;

    private Vector2 BgCenter;

    public void Start()
    {
        BgCenter = Background.transform.position;
    }

    void Update()
    {

        Background.transform.position = GetUpdatedPosition(Background, BackgroundShiftAmount);
        Middleground.transform.position = GetUpdatedPosition(Middleground, MiddlegroundShiftAmount);

        Foreground.transform.position = GetUpdatedPosition(Foreground, ForegroundShiftAmount);
    }

    public Vector3 GetUpdatedPosition(GameObject gameObject, float shiftAmount = 0)
    {
        float extraChange = shiftAmount * (BgCenter.x - Camera.transform.position.x);

        return new Vector3(
            x: Camera.transform.position.x + extraChange,
            y: Camera.transform.position.y,
            z: gameObject.transform.position.z
        );
    }
}