using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Potal : MonoBehaviour
{
    public string type;
    public Transform NextPotal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (type)
            {
                case "Blue":
                    if (Input.GetKey(KeyCode.C)&& GameManager.instance.playercontroller.potal1)
                    {
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);

                        StartCoroutine(Blink());

                    }
                    break;

                case "Yellow":
                    if (Input.GetKey(KeyCode.C))
                    {
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);
                        StartCoroutine(Blink());

                    }
                    break;
            }
        }
        IEnumerator Blink()
        {
            GameManager.instance.fadescript.Fade(false);
            yield return new WaitForSeconds(0.7f);
            GameManager.instance.playercontroller.transform.position = NextPotal.position;
            yield return new WaitForSeconds(0.8f);

            GameManager.instance.fadescript.Fade(true);
        }

    }
}
