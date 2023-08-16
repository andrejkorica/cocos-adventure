using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System;

public class CrewController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public bool ToggleWalk;
    private bool isAttacking = false;
    private float attackCooldown = 2.0f;
    private bool inCooldown = false;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
  
    }

    void Update()
    {
        if (!isAttacking)
        {
            WalkAB();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking && !inCooldown)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCooldown = false;
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        anim.SetBool("PlayerInRange", true);
       

        yield return new WaitForSeconds(1f);


        anim.SetBool("PlayerInRange", false);

       

        // Start cooldown
        inCooldown = true;
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        
    }



    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void WalkAB()
    {

        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        Debug.Log(MathF.Abs(transform.position.x - currentPoint.position.x));
        if (MathF.Abs(transform.position.x - currentPoint.position.x) < 0.5f)
        {
            Flip();
            rb.velocity = Vector2.zero;

            if (currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }
            else
            {
                currentPoint = pointB.transform;
            }
        }
    }
}
