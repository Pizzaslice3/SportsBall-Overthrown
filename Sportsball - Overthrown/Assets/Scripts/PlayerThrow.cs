using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThrow : MonoBehaviour
{
    public float currentThrowForce, minThrowForce, maxThrowForce;
    public float chargeSpeed;
    public bool chargingThrow;

    private float chargeSliderValue;
    public Slider chargeSlider;

    public bool hasBall;
    public GameObject currentBall;
    Rigidbody ballBody;

    // Start is called before the first frame update
    void Start()
    {
        //testing variables: for before we can actually pick things up
        ballBody = currentBall.GetComponent<Rigidbody>();
        ballBody.useGravity = false;
        hasBall = true;

        chargeSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    void Controls()
    {
        if (Input.GetMouseButton(0))
        {
            print("Holding button");
            if (hasBall)
            {
                chargingThrow = true;
                StartCoroutine(ChargeThrow());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hasBall)
            {
                chargingThrow = false;
                ThrowBall();
            }
        }
    }

    /// <summary>
    ///Essentially adds the ball to the player's inventory
    /// </summary>
    void PickUpBall(GameObject newBall)
    {
        hasBall = true;
        currentBall = newBall;
        ballBody = currentBall.GetComponent<Rigidbody>();
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
        ballBody.useGravity = true;

        ballBody.AddRelativeForce(Vector3.forward * currentThrowForce * Time.deltaTime * 1000);
        currentThrowForce = minThrowForce;


        chargeSliderValue = (currentThrowForce - minThrowForce) / (maxThrowForce - minThrowForce);
        chargeSlider.value = chargeSliderValue;

        currentBall = null;
        ballBody = null;
        hasBall = false;

    }
}