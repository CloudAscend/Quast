using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] private Tutorial tut;

    private void OnDisable()
    {
        Tutorial.instance.Event();
    }
}
