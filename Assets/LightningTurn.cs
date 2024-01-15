using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTurn : MonoBehaviour
{
    public float rotationSpeed = 50f; 

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
