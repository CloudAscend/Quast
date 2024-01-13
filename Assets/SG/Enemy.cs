using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator anim;
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] string playerTag = "Player";
    [SerializeField] GameObject bulletPrefab;
    bool isLive = true;
    Rigidbody rigid;
    [SerializeField] bool Attacktrue = false;
    float timeSinceLastAttack = 0f;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject DeathEffect;
    [SerializeField] GameObject item;

    [SerializeField] Slider Hpbar;
    [SerializeField] Slider Hpbar2;
    [SerializeField] int CurHP = 10;
    [SerializeField] int MaxHP = 10;

    [SerializeField] float detectionRange = 5f; // 플레이어 감지 범위 추가

    void Update()
    {
        if (CurHP <= 0)
        {
            Destroy(Instantiate(DeathEffect,transform.position, transform.rotation),2f);
            Instantiate(item, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }

        Hpbar.value = Mathf.Lerp(Hpbar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        Hpbar2.value = Mathf.Lerp(Hpbar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

        //Damage();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player == null)
            return;

        Vector3 dirVec = player.transform.position - transform.position;
        float distanceToPlayer = dirVec.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > stoppingDistance)
            {
                Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
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

    public void Damage()
    {
        CurHP--;
    }
}
