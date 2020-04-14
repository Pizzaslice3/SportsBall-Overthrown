using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitRagdoll : MonoBehaviour
{

    public bool hasHitPlayer = false;

    public GameObject ragdoll;

    public Animator ragdollAnimator;

    public GameObject ragdollThings;

    public GameObject player;


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player" && hasHitPlayer == false)

        {
            player.SetActive(false);
            ragdollThings.SetActive(true);
            hasHitPlayer = true;
            ragdollAnimator.enabled = false;
            ragdoll.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1.4f, -.7f) * 300, ForceMode.Impulse);
        }

    }
}
