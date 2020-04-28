using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{

    public CapsuleCollider topHalf;
    public CapsuleCollider bottomHalf;
    public Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;

            _rb = rb;

            if(_rb != null)
            _rb.AddForce(Vector3.back * 20, ForceMode.Impulse);
        }
    }
}
