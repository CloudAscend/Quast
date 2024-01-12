using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody rb;
    public Vector3 inputVec;
    private bool isFacingRight = true;

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

        Vector3 dirVec = inputVec.normalized * moveSpeed;
        rb.velocity = new Vector3(dirVec.x, rb.velocity.y, dirVec.z);

     
        if (inputVec.x != 0)
        {
            bool isMovingRight = inputVec.x > 0;
            if (isMovingRight != isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;  // 플립된 상태로 유지
    }
}
