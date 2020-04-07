using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateBall : MonoBehaviour
{

    public GameObject dodgeballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(dodgeballPrefab, gameObject.transform);
    }
}
