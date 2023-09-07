using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RisingLava : MonoBehaviour
{
    public static bool IsRising = false;
    public static float speed = 0.3f;
    void Start()
    {
        IsRising = false;
        speed = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRising)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }

    public void StartRising()
    {
        IsRising = true;
    }
}
