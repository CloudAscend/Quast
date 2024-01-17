using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Material ab;
    public Material bb;
    public bool isSwitch = false;
    public Transform potal;
    private void Awake()
    {
        
        
    }
    void Update()
    {
        if (!isSwitch)
        {
            gameObject.GetComponent<MeshRenderer>().material = ab;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = bb;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isSwitch)
        {
            StartCoroutine(Blink());
           
            potal.gameObject.GetComponent<Potal>().Init();
            //isSwitch = false;
        }
        else if (other.gameObject.tag == "Player" && !isSwitch)
        {
            isSwitch = true;
            AudioManager.instance.PlaySound(transform.position, 7, Random.Range(1f, 1.3f), 1);

        }
    }
    IEnumerator Blink()
    {
        GameManager.instance.playerab.transform.position = potal.transform.position;
        GameManager.instance.fadescript.Fade(true);
        yield return new WaitForSeconds(0.7f);
     

       
    }


}
