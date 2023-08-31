using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RisingLava : MonoBehaviour
{
    public bool IsRising = false;
    public float speed = 0.001f;
    void Start()
    {
        
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
