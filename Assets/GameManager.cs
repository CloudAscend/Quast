using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerAblity playerab;
    public PlayerController playercontroller;
    public Enemy enemy;
    public CameraShake camerashake;
    public GameObject player;
    public FadeScript fadescript;
    // Start is called before the first frame update
    void Start()
    {

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
