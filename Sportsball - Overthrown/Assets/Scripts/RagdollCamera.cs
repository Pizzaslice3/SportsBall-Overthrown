using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCamera : MonoBehaviour
{

    public GameObject ragdollCamFollow;

    private Vector3 offset;

 
    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - ragdollCamFollow.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ragdollCamFollow.transform.position + offset;
    }
}
