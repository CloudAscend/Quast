using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Potal : MonoBehaviour
{
    public string type;
    public Transform NextPotal;
    [SerializeField] private Platform[] activate;
    private int actSwitch;
    [SerializeField] GameObject PotalAura;
 
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
                case "BlueSwitch":
                    for (int i = 0; i < activate.Length; i++)
                    {
                        if (activate[i].isSwitch) actSwitch += 1;
                    }
                    if ( actSwitch == activate.Length)
                    {
                        PotalAura.SetActive(true);
                    }
                    if (Input.GetKey(KeyCode.C) && actSwitch == activate.Length)
                    {
                        
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);

                        StartCoroutine(Blink());

                    }
                    actSwitch = 0;
                    break;
                case "Yellow":
                    if (Input.GetKey(KeyCode.C))
                    {
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);
                        StartCoroutine(Blink());
                        StartCoroutine(AudioManager.instance.SwitchSong(0));

                    }
                    break;
                case "Yellowf":
                    if (Input.GetKey(KeyCode.C))
                    {
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);
                        StartCoroutine(Blink());
                        StartCoroutine(AudioManager.instance.SwitchSong(2));

                    }
                    break;
                case "SoundBlue":
                    if (Input.GetKey(KeyCode.C) && GameManager.instance.playercontroller.potal1)
                    {
                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);
                        StartCoroutine(AudioManager.instance.SwitchSong(1));

                        StartCoroutine(Blink());

                    }
                    break;
                case "SoundBlueSwitch":
                    for (int i = 0; i < activate.Length; i++)
                    {
                        if (activate[i].isSwitch) actSwitch += 1;
                    }
                    if (actSwitch == activate.Length)
                    {
                        PotalAura.SetActive(true);
                    }
                    if (Input.GetKey(KeyCode.C) && actSwitch == activate.Length)
                    {

                        AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1f, 1.3f), 1);
                        StartCoroutine(AudioManager.instance.SwitchSong(0));

                        StartCoroutine(Blink());

                    }
                    actSwitch = 0;
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

    public void Init()
    {
        for (int i = 0; i < activate.Length; i++)
        {
            activate[i].isSwitch = false;
        }
    }
 
}
