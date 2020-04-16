using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrow : MonoBehaviour
{
    public float currentThrowForce, minThrowForce, maxThrowForce;
    public float chargeSpeed;
    public bool chargingThrow;

    public GameObject playerCam;

    private float chargeSliderValue;
    public Slider chargeSlider;

    public float pickUpRange = 2f;
    public bool hasBall;
    private Dodgeball currentBall;
    Rigidbody ballBody;
    public Vector3 ballHoldPos;

    public Image reticle;
    public Color defaultRetColor, selectedRetColor,enemyRetColor;

    // Start is called before the first frame update
    void Start()
    {
        chargeSlider.value = 0;
        //reticle.color = defaultRetColor;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    void Cursor()
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo,pickUpRange);
        if (hit)
        {
            if (hitInfo.transform.gameObject.CompareTag("Ball"))
            {
                reticle.color = selectedRetColor;

                if (Input.GetMouseButtonDown(1) && !hasBall)
                {
                    PickUpBall(hitInfo.transform.gameObject);
                    print("Clicked on a ball");
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
        if(hasBall)
        {
            if (Input.GetMouseButton(0))
            {
                print("Holding button");

                chargingThrow = true;
                StartCoroutine(ChargeThrow());
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                chargingThrow = false;
                ThrowBall();
            }
        }

        Cursor();
        
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

    }

    IEnumerator ChargeThrow()
    {
        while (chargingThrow)
        {
            if (currentThrowForce < maxThrowForce)
            {
                currentThrowForce += chargeSpeed;
            }
            else
            {
                currentThrowForce = maxThrowForce;
            }
            chargeSliderValue = (currentThrowForce - minThrowForce) / (maxThrowForce - minThrowForce);
            chargeSlider.value = chargeSliderValue;
            yield return 0;
        }
    }

    /// <summary>
    /// Launches the ball in direction camera is facing
    /// </summary>
    void ThrowBall()
    {
        print("Throwing ball");
        ballBody.isKinematic = false;
        ballBody.useGravity = true;

        currentBall.transform.localPosition = Vector3.zero;
        currentBall.transform.parent = null;

        Vector3 throwDirection = Vector3.forward;
        throwDirection = Camera.main.transform.TransformDirection(throwDirection);

        ballBody.AddForce(throwDirection * currentThrowForce * Time.deltaTime * 10);
        currentThrowForce = minThrowForce;


        chargeSliderValue = (currentThrowForce - minThrowForce) / (maxThrowForce - minThrowForce);
        chargeSlider.value = chargeSliderValue;


        currentBall = null;
        ballBody = null;
        hasBall = false;

    }
}