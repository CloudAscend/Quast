using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerAblity : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;
    public GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.mass > 5)
        {
            Light.SetActive(true);
            spriteRenderer.color = Color.blue;

        }
        else
        {

            Light.SetActive(false);

            spriteRenderer.color = Color.white;

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
                   
                    break;
            }
            Destroy(other.gameObject);
        }



    }
}
