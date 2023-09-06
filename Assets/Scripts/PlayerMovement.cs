using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMove = 0f;
    public float runSpeed = 40f;
    public float maxVelocity = 10f;
    private Rigidbody2D rb;

    private CharacterController2D Controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public void Start()
    {
        Controller = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && Controller.IsGrounded() && IsGrounded())
        {
            Controller.Jump();
        }

        Controller.Move(horizontalMove * Time.fixedDeltaTime);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, _groundLayer);
    }
}

