using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public PlayerThrow thrownBy;
    private Vector3 originPos;

    public Rigidbody rb;
    public bool alive;
    private int numberOfPlayersHit;
    private GameObject[] playersHit;

    public float myForce = 500;

    void Start()
    {
        originPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Respawn()
    {
        transform.position = originPos;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Respawn();
        }
    }

    public void AssignPlayer(PlayerThrow player)
    {
        thrownBy = player;
    }
}
