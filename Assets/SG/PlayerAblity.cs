using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerAblity : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;
    public GameObject Light;
    public GameObject Ptc;
    public GameObject YellowPtc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playercontroller.Key > 0)
        {
            YellowPtc.SetActive(true);
        }
        else { 

        YellowPtc.SetActive(false);
        }
        if (rb.mass > 5)
        {
            Light.SetActive(true);

        }
        else
        {

            Light.SetActive(false);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Item item = other.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    rb.mass += 5;
                    Ptc.SetActive(true);
                    Invoke("PtcOff", 2.0f);
                    break;
                case "Key":
                   
                
                    
                    GameManager.instance.playercontroller.Key++;
                    break;
            }
           

            AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1f, 1.3f), 1);
            Destroy(other.gameObject);
        }



    }
    void PtcOff()
    {
        Ptc.SetActive(false);

    }
}
