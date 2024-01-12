using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody rb;
    public Vector3 inputVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        Vector3 dirVec = inputVec.normalized * moveSpeed;  // Vector2에서 Vector3로 변경
        rb.velocity = new Vector3(dirVec.x, rb.velocity.y, dirVec.z);  // y 값은 현재의 y 값으로 유지

        sprite.flipX = inputVec.x < 0 || inputVec.z > 0;
    }
}
