using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitRagdoll : MonoBehaviour
{

    public bool hasHitPlayer = false;

    public GameObject ragdoll;

    public Animator ragdollAnimator;


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && hasHitPlayer == false)

        {
            hasHitPlayer = true;
            ragdollAnimator.enabled = false;
            ragdoll.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1.8f, -.4f) * 600, ForceMode.Impulse);
        }

    }
}
