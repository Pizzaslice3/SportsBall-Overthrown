﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    public float gravity = -9.81f;

    public Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    public bool currentClass = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ClassInformation();
    }

    public void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void ClassInformation()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            currentClass = !currentClass;

            if (currentClass)
            {
                Sprinter();

            }
            if (!currentClass)
            {
                BasketBallPlayer();
            }
        }
        
    }

    public void BasketBallPlayer()
    {
        print("isBasketBallPlayer");
        jumpHeight = 12f;
        speed = 7;


    }

    public void Sprinter()
    {
        print("isSprinter");
        jumpHeight = 3;
        speed = 20f;

    }

}
