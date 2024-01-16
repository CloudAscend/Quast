using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    private void OnDisable()
    {
        Tutorial.instance.Event();
    }
}
