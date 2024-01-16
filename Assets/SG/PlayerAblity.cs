using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerAblity : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;
    public GameObject Light;
    public GameObject Ptc;
    public GameObject YellowPtc;
    public GameObject LightningAura;
    public Slider MassSlider;
    public bool LightningBall = false;

    void Start()
    {
        GameManager.instance.fadescript.Fade(true);
        rb = GetComponent<Rigidbody>();
        MassSlider.maxValue = 5f; // 최대값을 6으로 설정
    }

    void Update()
    {
        MassSlider.value = Mathf.Lerp(MassSlider.value, MassSlider.value = rb.mass, Time.deltaTime * 4); 

        if (GameManager.instance.playercontroller.Key > 0)
        {
            YellowPtc.SetActive(true);
        }
        else
        {
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
                case "LightningBall":
                    AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1.3f), 1);
                    LightningAura.SetActive(true);
                    LightningBall = true;
                    break;
                case "Speed":
                    GameManager.instance.player.GetComponent<PlayerController>().moveSpeed *= 4;
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
