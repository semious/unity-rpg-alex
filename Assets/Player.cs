using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldownTImer;

    [Header("Attack Info")]
    [SerializeField] private float comboTime = 0.3f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;


    private float xInput;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {

        base.Update();

        CheckInput();

        Movement();

        dashTime -= Time.deltaTime;
        if (!(dashCooldownTImer < 0)) dashCooldownTImer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            DashAbility();

        comboTimeWindow -= Time.deltaTime;

        FlipController();
        AnimatorControllers();

    }

    private void DashAbility()
    {
        if (dashCooldownTImer < 0 && !isAttacking)
        {
            dashTime = dashDuration;
            dashCooldownTImer = 2.0f;

        }
    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2)
            comboCounter = 0;
    }


    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartAttackEvent();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void StartAttackEvent()
    {
        if(!isGrounded)
        {
            return;
        }

        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (dashTime > 0)
            {
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
            }
            else
            {
                rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
       
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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

 
}
