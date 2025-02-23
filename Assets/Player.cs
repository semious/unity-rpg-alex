using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
   

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldownTImer;

    [Header("Attack Info")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private int comboCounter;

    private int facingDirection = 1;
    private bool facingRight = true;

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;


    private float xInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        Movement();
        CheckInput();
        CollisionChecks();

        DashAbility();

        FlipController();
        AnimatorControllers();

    }

    private void DashAbility()
    {
        dashTime -= Time.deltaTime;
       if (!(dashCooldownTImer < 0) ) dashCooldownTImer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTImer < 0)
        {
            dashTime = dashDuration;
            dashCooldownTImer = 2.0f;
        }
    }

    public void AttackOver()
    {
        isAttacking = false;
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
        }

         if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (dashTime>0)
        {
            rb.velocity = new Vector2(dashSpeed * xInput, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y); 
        }
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
       bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y); 

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
