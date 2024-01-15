using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    public GameObject Lighning;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        while (true) { 
        Destroy(Instantiate(Lighning,transform.position,transform.rotation),14f);
        yield return new WaitForSeconds(3f);
        }
    }
}
