using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorial.Event();
            gameObject.SetActive(false);
        }
    }
}
