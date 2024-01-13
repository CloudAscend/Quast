using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private KeyCode attackKey;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float maxDistance;
    private BoxCollider box;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(attackKey))
        {

        }
    }
}
