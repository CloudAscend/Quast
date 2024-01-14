using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalOpen : MonoBehaviour
{
    public GameObject LightningBall;
    public GameObject PotalAura;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            if (Input.GetKey(KeyCode.C) && GameManager.instance.playerab.LightningBall)
            {
                LightningBall.SetActive(true);
                GameManager.instance.playerab.LightningAura.SetActive(false);
                PotalAura.SetActive(true);
                GameManager.instance.playercontroller.potal1 = true;

            }
        }
    }
}
