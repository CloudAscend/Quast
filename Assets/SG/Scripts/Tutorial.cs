using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    [SerializeField] private Message[] message;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Image dialogImage;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private KeyCode nextInput;
    [SerializeField] private float delayMessage = 0.1f;
    [SerializeField] private AudioClip textSoundClip; // 추가: 텍스트 소리 파일
    private AudioSource textSound; // 추가: AudioSource 컴포넌트
    private bool isTutorial;
    private int curValue = -1;
    private int curMessage = 0;

    private void Awake()
    {
        instance = this;
        dialogImage.enabled = false;

        // 추가: AudioSource 초기화
        textSound = gameObject.AddComponent<AudioSource>();
        textSound.clip = textSoundClip;
        textSound.loop = false;
        textSound.playOnAwake = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextInput))
        {
            dialogText.enabled = true;
            StartCoroutine(SendMessage());
        }
    }

    public void Event()
    {
        curValue += 1;
        dialog.GetComponent<Animator>().SetBool("OnDialog", true);
        dialogImage.enabled = true;
        isTutorial = true;
        if (GameManager.instance.player != null)
            GameManager.instance.player.GetComponent<PlayerController>().isEvent = true;
        StartCoroutine(SendMessage());
    }

    private IEnumerator SendMessage()
    {
        if (isTutorial)
        {
            isTutorial = false;

            Message curMess = message[curValue];

            if (curMess.messages.Count <= curMessage)
            {
                isTutorial = false;
                dialog.GetComponent<Animator>().SetBool("OnDialog", false);
                GameManager.instance.player.GetComponent<PlayerController>().isEvent = false;
                dialogImage.enabled = false;
                curMessage = 0;
            }
            else
            {
                yield return new WaitForSeconds(delayMessage);
                char[] letter = curMess.messages[curMessage].ToCharArray();
                string sentence = " ";

                dialogText.text = sentence;

                // 추가: 텍스트 사운드 재생
                textSound.Play();

                for (int c = 0; c < letter.Length; c++)
                {
                    yield return new WaitForSeconds(delayMessage);
                    sentence += letter[c];
                    dialogText.text = sentence;
                }

                // 추가: 텍스트 사운드 멈춤
                textSound.Stop();

                isTutorial = true;
                curMessage += 1;
            }
        }
    }
}
