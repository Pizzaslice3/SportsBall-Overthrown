using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject ballPrefab;
    public float shootDelay;
    public GameObject player;
    private bool playerInRange;
    public float destroyAfter = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            StartCoroutine(ShootBall());
            print("Spawning a ball");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    public IEnumerator ShootBall()
    {
        while(playerInRange)
        {
            GameObject newBall = Instantiate(ballPrefab,transform.position,transform.rotation);
            newBall.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,1) * Time.deltaTime * 100000);
            yield return new WaitForSeconds(shootDelay);
            Destroy(newBall, destroyAfter);
        }
    }
}
