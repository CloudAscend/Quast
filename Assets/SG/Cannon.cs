using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject CannonBullet;
    public GameObject CannonPtc;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Fire()
    {

        float waitTime = Random.Range(2f, 10f);
        yield return new WaitForSeconds(waitTime);
        while (true)
        {


            Destroy(Instantiate(CannonPtc, transform.position, transform.rotation),2f);
            GameObject bulletObject = Instantiate(CannonBullet, transform.position, transform.rotation);
            StartCoroutine(Fires());
            Rigidbody rigid = bulletObject.GetComponent<Rigidbody>();
            Destroy((bulletObject),6f);
            rigid.AddForce(Vector3.back * 10, ForceMode.Impulse);
            yield return new WaitForSeconds(2f);

        }
    }
    IEnumerator Fires()
    {
        rb.velocity = Vector3.forward * 6;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.back * 6;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
    }


}
