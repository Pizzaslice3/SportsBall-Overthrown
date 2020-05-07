using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{

    public CapsuleCollider topHalf;
    public CapsuleCollider bottomHalf;
    public Rigidbody _rb;

    public bool hit = false;

    public ScoreManagerBehavior SMB;
    // Start is called before the first frame update
    void Start()
    {
    
            _rb = gameObject.GetComponent<Rigidbody>();
            SMB = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManagerBehavior>();
     
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ball") && !hit)
        {
            if(other.gameObject.GetComponent<Dodgeball>().alive)
            {
                hit = true;


                _rb.constraints = RigidbodyConstraints.None;
                _rb.AddForce(Vector3.back * 20, ForceMode.Impulse);
                SMB.IncreaseScore();
                Destroy(gameObject, .5f);
                
            }
        }
    }
}
