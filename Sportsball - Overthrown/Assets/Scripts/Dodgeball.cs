using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public GameObject lastThrownBy;
    private Vector3 originPos;

    public bool alive;
    private int numberOfPlayersHit;
    private GameObject[] playersHit;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
