using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMoveController : MonoBehaviour
{

    [Header("Movement")]
    public float moveAccel = 2;
    public float maxSpeed = 4 ;
    
    [Header("Jump")]
    public float jumpAccel;
    private bool isJumping;
    private bool isOnGround;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private Rigidbody2D rig;
    private Animator _animator;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
     // read input
        if (Input.GetMouseButtonDown(0))
        {
            // if (isOnGround)
            // {
                
            // }
            isJumping = true;
        }
        _animator.SetBool("isOnGround", isOnGround);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (!isOnGround && GetComponent<Rigidbody>().velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocityVector = rig.velocity;

        if (isJumping)
        {
            velocityVector.y += jumpAccel;
            isJumping = false;
        }
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;
    }
    private void OnDrawGizmos()
    {
        var position = transform.position;
        Debug.DrawLine(position, position + Vector3.down * groundRaycastDistance, Color.white);
    }
}
