using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

    
        if (other.gameObject.tag == "Rock") {
            GameManager.instance.playerab.rb.mass -=5;
            Debug.Log("d");
            gameObject.SetActive(false);
        }
    }
}
