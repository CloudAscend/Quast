using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anime;

    [Header("Player Attack")]
    [SerializeField] private BoxCollider attackBoundary;
    [SerializeField] private KeyCode attackKey;
    private Rigidbody rb;
    public Vector3 inputVec;
    private bool isFacingRight = true;
    private bool isAttack;
    //private GameObject enemy;
    private int curAnime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Motion();
        Attack();
    }

    private void Movement()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        Vector3 dirVec = inputVec.normalized * moveSpeed;
        rb.velocity = new Vector3(dirVec.x, rb.velocity.y, dirVec.z);

        bool isRun = inputVec.x != 0 || inputVec.z != 0 == true;
        anime.SetBool("Run", isRun);

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

    private void Motion()
    {
        
    }

    //private void Attack()
    //{
    //    string curAttAnime = "Attack1";

    //    switch (curAnime % 3)
    //    {
    //        case 0:
    //            curAttAnime = "Attack1";
    //            break;
    //        case 1:
    //            curAttAnime = "Attack2";
    //            break;
    //        case 2:
    //            curAttAnime = "Attack3";
    //            break;
    //    }

    //    if (Input.GetKeyDown(attackKey))
    //    {
    //        anime.SetTrigger(curAttAnime);
    //        isAttack = true;
    //        //if (enemy != null)
    //        //{
    //        //    enemy.GetComponent<Enemy>().Damage();
    //        //}
    //        curAnime++;
    //    }

    //    if (sprite.flipX) attackBoundary.center = new Vector3(-1.2f, 1.4f, -1.2f);
    //    else attackBoundary.center = new Vector3(1.2f, 1.4f, 1.2f);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy") && isAttack)
    //    {
    //        //Debug.Log(other.gameObject.GetComponent<Enemy>());
    //        if (other.gameObject.GetComponent<Enemy>() != null)
    //        other.gameObject.GetComponent<Enemy>().Damage();
    //        isAttack = false;
    //    }
    //}
}
