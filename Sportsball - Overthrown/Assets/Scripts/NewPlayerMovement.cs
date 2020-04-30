using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewPlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    private float speed = 12f;
    private float jumpHeight = 3f;
    private float gravity = -9.81f;
    private float dodgeTime = .5f;
    private float dodgeMult = 5f;

    private bool dodging = false;

    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;

    private enum Class { BASKETBALLER, SPRINTER };
    private Class currentClass = Class.SPRINTER;

    [Header("Sprinter stats")]
    public float sprintSpeed;
    public float sprintJump;
    public float sprintThrow;
    public float sprintDodgeTime;
    public float sprintDodgeMult;

    [Header("BasketBaller stats")]
    public float ballerSpeed;
    public float ballerJump;
    public float ballerThrow;
    public float ballerDodgeTime;
    public float ballerDodgeMult;

    [Header("UI")]
    public TextMeshProUGUI classText;

    PlayerThrow pThrow;

    

    // Start is called before the first frame update
    void Start()
    {
        pThrow = GetComponent<PlayerThrow>();
        pThrow.SetThrowForce(sprintThrow);
        speed = sprintSpeed;
        jumpHeight = sprintJump;
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
        //basic movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (!dodging)
        {
            //actual movement
            controller.Move(move * speed * Time.deltaTime);

            //Jump Related
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            //keeps player moving downward at all times
            controller.Move(velocity * Time.deltaTime);

        }

        //if (Input.GetButtonDown("Dodge") && move != Vector3.zero)
        //{
        //    //stores value of move at that exact moment, so it can no longer be changed mid dodge
        //    StartCoroutine(Dodge(move));
        //}
    }

    private IEnumerator Dodge(Vector3 newDirection)
    {
        dodging = true;
        controller.Move(newDirection * dodgeMult);
        yield return new WaitForSeconds(dodgeTime);
        dodging = false;
    }

    public void ClassInformation()
    {
        if(Input.GetButtonDown("Swap Class") && !dodging)
        {
            switch(currentClass)
            {
                case Class.SPRINTER:
                    BasketBallPlayer();
                    break;
                case Class.BASKETBALLER:
                    Sprinter();
                    break;
            }
            
        }
        
    }

    public void BasketBallPlayer()
    {
        classText.text = "Current Class: BasketBaller\n~Stats~\n-High Jump Force\n-Low Speed\n-High Throw Speed";

        print("isBasketBallPlayer");

        currentClass = Class.BASKETBALLER;

        pThrow.SetThrowForce(ballerThrow);
        speed = ballerSpeed;
        jumpHeight = ballerJump;
        dodgeTime = ballerDodgeTime;
        dodgeMult = ballerDodgeMult;

    }

    public void Sprinter()
    {
        classText.text =  "Current Class: Sprinter\n~Stats~\n-Low Jump Force\n-High Speed\n-Low Throw Speed";
        print("isSprinter");

        currentClass = Class.SPRINTER;

        pThrow.SetThrowForce(sprintThrow);
        speed = sprintSpeed;
        jumpHeight = sprintJump;
        dodgeTime = sprintDodgeTime;
        dodgeMult = sprintDodgeMult;
    }

}
