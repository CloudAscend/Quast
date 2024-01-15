using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Message[] message;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Image dialogImage;
    [SerializeField] private Text dialogText;
    [SerializeField] private KeyCode nextInput;
    [SerializeField] private float delayMessage = 0.1f;
    private bool isTutorial;
    private int curValue = -1;
    private int curMessage = 0;

    private void Awake()
    {
        dialogImage.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextInput) && isTutorial)
        {
            isTutorial = false;
            StartCoroutine(SendMessage());
        }
    }

    public void Event()
    {
        curValue += 1;
        dialog.GetComponent<Animator>().SetBool("OnDialog", true);
        dialogImage.enabled = true;
        isTutorial = true;
        GameManager.instance.player.GetComponent<PlayerController>().isEvent = true;
        StartCoroutine(SendMessage());
    }

    private IEnumerator SendMessage()
    {
        Debug.Log(curValue);
        Message curMess = message[curValue];

        if (curMess.messages.Count == curMessage)
        { 
            isTutorial = false;
            dialog.GetComponent<Animator>().SetBool("OnDialog", false);
            GameManager.instance.player.GetComponent<PlayerController>().isEvent = false;
            dialogImage.enabled = false;
            curMessage = 0;
        }
        else
        {
            dialogText.text = "* ";
            char[] letter = curMess.messages[curMessage].ToCharArray();

            for (int c = 0; c < letter.Length; c++)
            {
                yield return new WaitForSeconds(delayMessage);
                dialogText.text += letter[c];
            }
            isTutorial = true;

            curMessage += 1;
        }
    }
}
