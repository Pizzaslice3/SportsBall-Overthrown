using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrow : MonoBehaviour
{
    [Header("The basics")]
    //public float currentThrowForce;
    //public float chargeSpeed;
    public float throwForce;
    public float gravDelay;

    //public float throwMod;

    //[Header("Charge Bar")]
    //public Slider chargeSlider;
    //public Image chargeColor;
    //public Color pChargeCol, okChargeCol;
    //private bool chargingUp, chargingDown;
    //private float chargeSliderValue;
    //public float sweetSpotMin = 70;
    //public float sweetSpotMax = 90;

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

    // Start is called before the first frame update
    void Start()
    {
        //chargeSlider.value = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        reticle.color = defaultRetColor;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    void Reticle()
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, pickUpRange);
        if (hit)
        {
            if (hitInfo.transform.gameObject.CompareTag("Ball"))
            {
                reticle.color = selectedRetColor;
                //print("I see a ball");

                if (Input.GetMouseButtonDown(1) && !hasBall)
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
            //if (Input.GetMouseButtonDown(0))
            //{
            //    chargingUp = true;
            //    StartCoroutine(ChargeThrow());
            //}
            if (Input.GetMouseButtonDown(0))
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


        //Only for playtest!!!
        throwForce = currentBall.myForce;
        gravDelay = currentBall.myGravityDelay;
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
}