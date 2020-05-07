using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerThrow : MonoBehaviour
{
    [Header("The basics")]
    //public float currentThrowForce;
    //public float chargeSpeed;
    public float throwForce;
    public float gravDelay;

    [Header("Respawning")]
    public float respawnTime;
    public Vector3 respawnPos;
    public bool respawning;

    [Header("Ball Related")]

    public bool hasBall;
    private Dodgeball currentBall;
    Rigidbody ballBody;
    public Vector3 ballHoldPos;

    [Header("UI")]
    public GameObject playerCam;
    public float pickUpRange = 2f;
    public Image reticle;
    public Color defaultRetColor, selectedRetColor, enemyRetColor;

    [Header("Player Feedback")]
    public AudioSource ballThrownSFX;

    private NewPlayerMovement pMove;

    // Start is called before the first frame update
    void Start()
    {
        respawnPos = transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        reticle.color = defaultRetColor;

        pMove = GetComponent<NewPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    public void SetThrowForce(float force)
    {
        throwForce = force;
    }

    void Reticle()
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, pickUpRange);
        if (hit)
        {
            if (hitInfo.transform.gameObject.CompareTag("Ball"))
            {

                reticle.color = selectedRetColor;

                if (Input.GetButtonDown("Pick Up") && !hasBall)
                {
                    PickUpBall(hitInfo.transform.gameObject);
                }
            }
            else
            {
                reticle.color = defaultRetColor;
            }
        }
        else
        {
            reticle.color = defaultRetColor;

        }
    }

    void Controls()
    {
        if (hasBall)
        {
            if (Input.GetButtonDown("Throw"))
            {
                StartCoroutine(ThrowBall());
            }            
        }

        Reticle();

    }

    /// <summary>
    ///Essentially adds the ball to the player's inventory
    /// </summary>
    void PickUpBall(GameObject newBall)
    {
        hasBall = true;
        currentBall = newBall.GetComponent<Dodgeball>();
        ballBody = currentBall.rb;

        ballBody.isKinematic = true;
        ballBody.useGravity = false;

        currentBall.transform.parent = playerCam.transform;
        currentBall.transform.localPosition = ballHoldPos;
        currentBall.AssignPlayer(this);


        
    }

    //IEnumerator ChargeThrow()
    //{


    //    while (Input.GetMouseButton(0))
    //    {
    //        //determine which direction the bar should go in
    //        if (currentThrowForce >= 100)
    //        {
    //            print("Topped off");
    //            currentThrowForce = 100;
    //            chargingUp = false;
    //            chargingDown = true;
    //        }
    //        else if (currentThrowForce <= 0)
    //        {
    //            print("Bottomed out");

    //            currentThrowForce = 0;
    //            chargingUp = true;
    //            chargingDown = false;
    //        }

    //        //actually changes value of currentThrowForce
    //        if (chargingUp)
    //        {
    //            currentThrowForce += chargeSpeed;
    //            print("Charging up: " + currentThrowForce);
    //        }
    //        else if (chargingDown)
    //        {
    //            print("Charging down: " + currentThrowForce);
    //            currentThrowForce -= chargeSpeed;
    //        }

    //        //translates value visually
    //        chargeSliderValue = currentThrowForce / 100;
    //        chargeSlider.value = chargeSliderValue;

    //        if (currentThrowForce > sweetSpotMin && currentThrowForce < sweetSpotMax)
    //        {
    //            chargeColor.color = pChargeCol;
    //        }
    //        else
    //        {
    //            chargeColor.color = okChargeCol;
    //        }

    //        yield return 0;
    //    }
    //}

    /// <summary>
    /// Launches the ball in direction camera is facing
    /// </summary>
    IEnumerator ThrowBall()
    {
        //print("Throwing ball");
        ballBody.isKinematic = false;

        currentBall.transform.localPosition = new Vector3(0, 0, 2);
        currentBall.Thrown();

        Vector3 throwDirection = Camera.main.transform.forward;
        //throwDirection = Camera.main.transform.TransformDirection(throwDirection);

        ballBody.AddForce(throwDirection * throwForce * Time.deltaTime * 100);
        //messy prototype code; FIX LATER
        //if(throwForce == pMove.ballerThrow)
        //{
        //    ballBody.AddForce(Vector3.up * 300);
        //}

        currentBall.transform.parent = null;

        ballThrownSFX.PlayOneShot(ballThrownSFX.clip);

        
        ballBody.useGravity = true;

        //chargeSliderValue = currentThrowForce / 100;
        //chargeSlider.value = chargeSliderValue;

        currentBall = null;
        ballBody = null;
        hasBall = false;

        yield return new WaitForSeconds(gravDelay);

    }

    void Respawn()
    {
        SceneManager.LoadScene(1);
        //gameObject.transform.position = respawnPos;
        //transform.position = respawnPos;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("TurretBall"))
        {
            Respawn();
        }
    }
}