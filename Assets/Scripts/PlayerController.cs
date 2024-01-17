using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    [SerializeField] Color playerA;
    public bool NeverDie = false;

    public int CurHP;
    public int MaxHP;
    public Slider HPbar;


    public bool isEvent;


    public int Key = 0;
    public GameObject ChainEffect;

    public AudioSource audioSrc;
    public bool potal1 = false;
    public bool isMoving = false;
    private bool isDeath = false;
    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void Update()
    {
        MoveSfx();
        Die();
        if (HPbar != null)
            HPbar.value = Mathf.Lerp(HPbar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 4);
        if (!isEvent)
        {
            if (!isDeath)
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
    IEnumerator NeverDieS()
    {
        playerA.a = 0.5f;
        sprite.color = playerA;

        NeverDie = true;
        yield return new WaitForSeconds(2f);
        NeverDie = false;
        playerA.a = 1f;
        sprite.color = playerA;
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
            AudioManager.instance.PlaySound(transform.position, 6, Random.Range(0.9f, 1f), 1);

                Destroy(Instantiate(ChainEffect, other.gameObject.transform.position, transform.rotation),2f);
                Key--;
                other.gameObject.transform.position = new Vector3(10,40,10);
            }
        }

     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone" && !NeverDie)
        {
            StartCoroutine(NeverDieS());
            CurHP -= 2;
            Destroy(other.gameObject);
            GameManager.instance.camerashake.Shake();
        }
        if (other.gameObject.tag == "Lightning" && !NeverDie)
        {
            AudioManager.instance.PlaySound(transform.position, 3, Random.Range(0.9f, 1f), 1);

            StartCoroutine(NeverDieS());
            CurHP -= 3;
            GameManager.instance.camerashake.Shake();
        }
        if (other.gameObject.tag == "Enemy" && !NeverDie)
        {
            StartCoroutine(NeverDieS());
            CurHP -= 1;
            GameManager.instance.camerashake.Shake();
        }

    }
    void Die()
    {
        if (CurHP <= 0 && !isDeath)
        {
            isDeath = true;
            anime.SetTrigger("Death");
            StartCoroutine(Fadein());
        }

    }
    IEnumerator Fadein()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.fadescript.Fade(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SG 1");

    }
    void MoveSfx()
    {
        if (rb.velocity.x != 0|| rb.velocity.z != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        else
        {
            audioSrc.Stop();
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
