using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
    }
}
