using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerAblity playerab;
    public PlayerController playercontroller;
    public Enemy enemy;
    public CameraShake camerashake;
    public GameObject player;
    public FadeScript fadescript;
    public GameObject settingpanel;
    bool Setpan = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Setpan)
                OnSettingPanel();
            else
                OffSettingPanel();
        }
    }

    public void OnSettingPanel()
    {
        Setpan = true;
        settingpanel.SetActive(true);
    }

    public void OffSettingPanel()
    {
        Setpan = false;
        settingpanel.SetActive(false);
    }
    public void MainScene()
    {
        SceneManager.LoadScene("IntroScenes");
        settingpanel.SetActive(false);
    }

    public void EscapeGame()
    {
        Application.Quit();
    }
}
