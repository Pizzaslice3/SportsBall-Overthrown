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
        if(SMB == null)
        {
            SMB = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManagerBehavior>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ball") && hit == false)
        {
            hit = true;
            if (_rb == null)
            {
                Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
                _rb = rb;
            }

            if(_rb != null)
            _rb.AddForce(Vector3.back * 20, ForceMode.Impulse);

            Destroy(gameObject, .5f);
            SMB.IncreaseScore();
        }
    }
}
