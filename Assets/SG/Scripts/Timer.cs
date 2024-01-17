using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer time;

    [SerializeField] private Image timerSprite;
    [SerializeField] private TMP_Text timerText;
    [HideInInspector] public int timeRate = 0;
    [HideInInspector] public int timeSec;

    private void Awake()
    {
        time = this;
        TimerStop();
        Debug.Log("Timer : " + timeSec);
    }

    public void TimerStart()
    {
        timeRate = timeSec;
        timerText.text = timeRate.ToString();
        timerSprite.enabled = true;
        timerText.enabled = true;

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        timeRate -= 1;
        timerSprite.fillAmount = (float)timeRate / timeSec;
        timerText.text = timeRate.ToString();

        if (timeRate <= 5)
        {
            timerSprite.color = new Color(1f, 0, 0, 1f);
            timerText.color = new Color(1f, 0, 0, 1f);
        }

        if (timeRate > 0)
            StartCoroutine(Countdown());
        else
            TimerStop();
    }

    private void TimerStop()
    {
        timerSprite.color = new Color(1f, 1f, 0, 1f);
        timerText.color = new Color(1f, 1f, 0, 1f);

        timerSprite.enabled = false;
        timerText.enabled = false;
    }
}
