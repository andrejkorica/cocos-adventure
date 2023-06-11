using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    
    public GameObject[] positions;

    [SerializeField]
    private int[] flipIndexes;

    private int index;
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index].transform.position, Time.deltaTime * speed);

        Vector3 pos1 = new Vector3(positions[index].transform.position.x, positions[index].transform.position.y, 0);

        if (Vector2.Distance(transform.position, pos1) < 0.1f)
        {
            if (index == positions.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            FlipIfNeeded();
        }
    }

    private void FlipIfNeeded()
    {
        if (ArrayContainsValue(flipIndexes, index))
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private bool ArrayContainsValue(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }
}
