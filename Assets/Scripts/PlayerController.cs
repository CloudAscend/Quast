using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anime;

    [Header("Player Attack")]
    [SerializeField] private BoxCollider attackBoundary;
    [SerializeField] private KeyCode attackKey;
    private Rigidbody rb;
    public Vector3 inputVec;
    private bool isFacingRight = true;
    private int curAnime;
    public Vector3 boxSize;
    public Transform pos;
    [SerializeField]
    float curTime;
    public float coolTime = 0.5f;


    public int CurHP;
    public int MaxHP;
    public Slider HPbar;


    public bool isEvent;


    public int Key = 0;
    public GameObject ChainEffect;


    public bool potal1 = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void Update()
    {
        if (HPbar != null)
            HPbar.value = Mathf.Lerp(HPbar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 4);
        if (!isEvent)
        {
            Movement();
            Attack();
            Motion();
        }
        else
        {
            rb.velocity = Vector3.zero;
            anime.SetBool("Run", false);
        }
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

    private void Attack()
    {
        string curAttAnime = "Attack1";

        switch (curAnime % 3)
        {
            case 0:
                curAttAnime = "Attack1";
                break;
            case 1:
                curAttAnime = "Attack2";
                break;
            case 2:
                curAttAnime = "Attack3";
                break;
        }
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(attackKey))
            {
                AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1.2f), 1);
                Collider[] colliders = Physics.OverlapBox(pos.position, boxSize / 2f);

                foreach (Collider collider in colliders)
                {
                    if (collider != null)
                    {
                        // 태그를 확인하여 Enemy인 경우에만 처리
                        if (collider.gameObject.CompareTag("Enemy"))
                        {
                            // Enemy 컴포넌트를 가져오기
                           
                            Enemy enemyComponent = collider.gameObject.GetComponent<Enemy>();
                            EnemyBase enemyCom = collider.gameObject.GetComponent<EnemyBase>();

                            // Enemy 컴포넌트가 존재하는지 확인
                            if (enemyComponent != null)
                            {
                                AudioManager.instance.PlaySound(transform.position, 1, Random.Range(0.9f, 1f), 1);
                                enemyComponent.TakeDamage(2);
                            }
                            if (enemyCom != null)
                            {
                                AudioManager.instance.PlaySound(transform.position, 1, Random.Range(0.9f, 1f), 1);
                                enemyCom.TakeDamage(2);
                            }    
                        }
                    }
                }

                anime.SetTrigger(curAttAnime);
                curAnime++;
                curTime = coolTime;
            }
        }

        else
        {
            curTime -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chain")
        {
            if (Input.GetKey(KeyCode.C) && Key > 0)
            {
                Debug.Log("S");
            
                Destroy(Instantiate(ChainEffect, other.gameObject.transform.position, transform.rotation),2f);
                Key--;
                other.gameObject.transform.position = new Vector3(10,40,10);
            }
        }

     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            CurHP--;
            Destroy(other.gameObject);
            GameManager.instance.camerashake.Shake();
        }


    }
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
