using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _mainCameraRotation;

    [SerializeField]
    private float playerSpeed = 1f;
    [SerializeField, Tooltip("Max speed the player can move at")]
    private float maxPlayerSpeed = 1f;
    [SerializeField]
    private float maxAirSpeed = 10f;
    private Vector2 _movementDir = Vector2.zero;
    [SerializeField] private float jumpForce = 10;
    private bool _inAir = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCameraRotation = FindObjectOfType<CinemachineBrain>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //calls the MovePlayer() script every frame
    public void Update()
    {
        MovePlayer();
    }

    //if the player collides with anything they can now jump (make collision only for ground objects
    private void OnCollisionEnter(Collision other)
    {
        _inAir = false;     
    }

    //when the player is not colliding with something they can't jump (make collision only for ground objects)
    private void OnCollisionExit(Collision other)
    {
        _inAir = true;
    }


    //called in update / gets a v3 of movement input from the OnMove() function / clamps velocity / 
    void MovePlayer()
    {
        Vector3 velocity = _rigidbody.velocity;
        var camRot = _mainCameraRotation.rotation;
        camRot.eulerAngles = new Vector3(0, camRot.eulerAngles.y, 0);
        if (_inAir)
        {
            velocity += camRot * Vector3.ClampMagnitude(new Vector3(_movementDir.x, 0, _movementDir.y) * playerSpeed * Time.deltaTime, maxAirSpeed);
        }
        else
        {
            velocity += camRot * Vector3.ClampMagnitude(new Vector3(_movementDir.x, 0, _movementDir.y) * playerSpeed * Time.deltaTime, maxPlayerSpeed);
        }
        velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        _rigidbody.velocity = velocity;
    }



    //called upon in the Player Input component on the player / returns the _movementDir used in MovePlayer() based on keys pressed
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDir = context.performed ? context.ReadValue<Vector2>() : Vector2.zero;
    }


    //called upon in the Player Input component on the player / if space is pressed & the bool _inAir is false, than player jumps
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!_inAir)
        { 
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }  
}
