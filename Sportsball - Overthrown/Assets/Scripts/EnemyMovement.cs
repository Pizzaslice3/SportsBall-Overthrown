﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private bool isGrounded;
    public Transform groundCheck;
    private float groundDistance = .4f;
    public LayerMask groundMask;

    private Vector3 velocity;
 
    private float gravity = -35f;

    private Rigidbody _rb;

    [Header("Check boxes for what you want enemy to do")]
    public bool doesThisEnemyJump = false;

    public bool doesThisEnemeyMove;

    [Header("Jump Variables")]
    public float jumpHeight = 12f;

    [Header("Movement Variables")]
    public Vector3 movementDirection;
    private bool changeDirection;
    public float movementSpeed;
    public float timeTillSwitchDirections;

    // Update is called once per frame

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();

        if (doesThisEnemeyMove)
            InvokeRepeating("SwitchDirections", timeTillSwitchDirections, timeTillSwitchDirections);
    
    }
    void Update()
    {
        if(doesThisEnemyJump)
        JumpingMovement();

        if (doesThisEnemeyMove)
         Movement();

    }

    public void JumpingMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }


        //Jump Related
        if (isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }


    //moves the character is a desired direction 
    public void Movement()
    {
        if(changeDirection == false)
        transform.position += movementDirection * movementSpeed * Time.deltaTime;

        if(changeDirection)
            transform.position -= movementDirection * movementSpeed * Time.deltaTime;  
    }


    //called when the character should change directions
    public void SwitchDirections()
    {
        changeDirection = !changeDirection;
    }

}
