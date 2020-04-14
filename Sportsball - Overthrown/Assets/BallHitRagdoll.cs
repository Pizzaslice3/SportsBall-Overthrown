using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitRagdoll : MonoBehaviour
{

    //bool that makes sure the force only gets applied once to the ragdoll when hit
    public bool hasHitPlayer = false;

    //the hips of the ragdoll for force to be applied to 
    public GameObject ragdoll;


    //gets the camera and the ragdoll that will appear when hit
    public GameObject ragdollThings;


    //finds the animatior on the ragdoll
    public Animator ragdollAnimator;


    //the direction the force on the reagdoll will be applied to (work on this so that where the ball hits determines these values
    public Vector3 ragdollForceDirection = new Vector3(0f, 1.4f, -.7f);


    //the force that will be applied to when hit by a ball (the higer the more impactful but the more messed up the ragdoll becomes)
    public int ragdollForce = 300;


    void OnCollisionEnter(Collision other)
    {

        //checks if the ball hits the player
        if(other.gameObject.tag == "Player" && hasHitPlayer == false)

        {
            other.gameObject.SetActive(false);
            ragdollThings.SetActive(true);
            hasHitPlayer = true;
            ragdollAnimator.enabled = false;
            ragdoll.GetComponent<Rigidbody>().AddForce(ragdollForceDirection * 300, ForceMode.Impulse);
        }

    }
}
