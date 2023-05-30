using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject[] positions;

    public int index;
    public Vector3 localposition;
    public Vector3 globalposition;

    void Update()
    {
        localposition = transform.localPosition;
        globalposition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, positions[index].transform.position, Time.deltaTime * speed);

        Vector3 Pos1 = new(transform.position.x, transform.position.y, 0);

        if(Vector2.Distance(Pos1, positions[index].transform.position) < 0.1f)
        {
            if (index == positions.Length -1)
            {
                index = 0;
            }
            else
            {
                index ++;
            }
        }
    }
}
