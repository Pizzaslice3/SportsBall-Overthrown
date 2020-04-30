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

    public SphereCollider realCollider;
    public SphereCollider pickUpCollider;

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
        pickUpCollider.enabled = false;
        realCollider.enabled = false;
    }

    public void Thrown()
    {
        realCollider.enabled = true;
        alive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            pickUpCollider.enabled = true;
            alive = false;
        }
    }
}
