using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 transOffset;
    [SerializeField] private float speed;
    //[SerializeField] private Vector3 rotatOffset;

    private void FixedUpdate()
    {
        CameraTarget();
    }

    private void CameraTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + transOffset, speed);
    }
}
