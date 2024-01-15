using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    Animator anim;
    public float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] string playerTag = "Player";
    [SerializeField] GameObject bulletPrefab;
    protected bool isLive = true;
    protected Rigidbody rigid;
    [SerializeField] bool Attacktrue = false;
    float timeSinceLastAttack = 0f;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject DeathEffect;
    [SerializeField] GameObject DamageEffect;
    [SerializeField] GameObject item;

    [SerializeField] Slider Hpbar;
    [SerializeField] Slider Hpbar2;
    [SerializeField] int CurHP = 10;
    [SerializeField] int MaxHP = 10;





    [SerializeField] float detectionRange = 5f; // 플레이어 감지 범위 추가

    protected virtual void Update()
    {
        if (CurHP <= 0)
        {
            Destroy(Instantiate(DeathEffect, transform.position, transform.rotation), 2f);
            Instantiate(item, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }

        Hpbar.value = Mathf.Lerp(Hpbar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        Hpbar2.value = Mathf.Lerp(Hpbar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

    }

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        Move();

    }

    public void TakeDamage(int damage)
    {
        Destroy(Instantiate(DamageEffect, transform.position, transform.rotation), 3f);
  
        GameObject player = GameManager.instance.player;
        if (player != null)
        {
            Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
            knockbackDirection.y = 0;
            transform.Translate(knockbackDirection * 1.5f, Space.World);
        }


        GameManager.instance.camerashake.Shake();
        CurHP -= damage;
    }
    protected virtual void Move()
    {
        if (!isLive)
            return;

        GameObject player = GameManager.instance.player;

        if (player == null)
            return;

        Vector3 dirVec = player.transform.position - transform.position;
        float distanceToPlayer = dirVec.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 nextVec = dirVec.normalized * speed;
                rigid.MovePosition(rigid.position + new Vector3(nextVec.x, 0, nextVec.z));
            }
            else
            {
                rigid.velocity = Vector3.zero;

                if (Time.time - timeSinceLastAttack >= attackCooldown && Attacktrue)
                {
                    Destroy(Instantiate(bulletPrefab, transform.position, Quaternion.identity), 3f);
                    timeSinceLastAttack = Time.time;
                }
            }

            spriteRenderer.flipX = dirVec.x < 0 || dirVec.z > 0;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (enemyname == "P")
    //    {
    //        if (other.gameObject.tag == "Chain")
    //        {
    //            gameObject.tag = "Untagged";
    //            speed = 0;
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (enemyname == "P")
    //    {
    //        if (other.gameObject.tag == "Chain")
    //        {
    //            gameObject.tag = "Enemy";

    //            speed = 5;
    //        }
    //    }
    //}
}