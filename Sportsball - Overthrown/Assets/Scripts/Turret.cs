using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject ballPrefab;
    private float shootDelay;
    public GameObject player;
    public float launchForce = 50000;
    public float destroyAfter = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootBall());
        shootDelay = Random.Range(1f, 4f);
    }

    public IEnumerator ShootBall()
    {
        while(true)
        {
            GameObject newBall = Instantiate(ballPrefab,transform.position,transform.rotation);
            newBall.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,1) * Time.deltaTime * launchForce);
            Destroy(newBall, destroyAfter);

            yield return new WaitForSeconds(shootDelay);
        }
    }
}
