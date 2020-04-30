using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchField : MonoBehaviour
{
    public bool touchingABall;
    public Dodgeball thisBall;

    private void Update()
    {
        if(!thisBall.alive)
        {
            touchingABall = false;
            thisBall = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DodgeBall"))
        {
            thisBall = other.gameObject.GetComponent<Dodgeball>(); 
            if(thisBall.alive)
            {
                touchingABall = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DodgeBall"))
        {
            touchingABall = false;
            thisBall = null;
        }

    }
}
