using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCamera : MonoBehaviour
{

    public GameObject ragdoll;

    private Vector3 offset;

  


    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - ragdoll.transform.position;
        
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ragdoll.GetComponent<Rigidbody>().AddForce(new Vector3 (.5f,1,0) * 600, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ragdoll.transform.position + offset;
    }
}
