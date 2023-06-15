using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHedgehog : MonoBehaviour
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
        Vector3 targetPosition = positions[index].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        Vector3 pos1 = new Vector3(targetPosition.x, targetPosition.y, 0);

        if (Vector2.Distance(transform.position, pos1) < 0.1f)
        {
            if (index < positions.Length - 1)
            {
                Vector3 direction = positions[index + 1].transform.position - targetPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (direction.x < 0)
                {
                    angle -= 180f;
                }
                transform.rotation = Quaternion.Euler(0, 0, angle);
                index++;
            }
            else if (index == positions.Length - 1)
            {
                Vector3 direction = positions[0].transform.position - targetPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (direction.x < 0)
                {
                    angle -= 180f;
                }
                transform.rotation = Quaternion.Euler(0, 0, angle);
                index = 0;
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
