using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastEnemy : MonoBehaviour
{

    public string enemyname;
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
    [SerializeField] GameObject DamageEffect;
    [SerializeField] GameObject item;

    [SerializeField] GameObject YDeathEffect;
    [SerializeField] GameObject YDamageEffect;
    [SerializeField] GameObject Yitem;

    [SerializeField] GameObject GDeathEffect;
    [SerializeField] GameObject GDamageEffect;

    [SerializeField] Slider Hpbar;
    [SerializeField] Slider Hpbar2;
    [SerializeField] int CurHP = 10;
    [SerializeField] int MaxHP = 10;





    [SerializeField] float detectionRange = 5f; // 플레이어 감지 범위 추가

    void Update()
    {
        if (CurHP <= 0)
        {
            switch (enemyname)
            {
                case "P":
                    Destroy(Instantiate(DeathEffect, transform.position, transform.rotation), 2f);
                    Instantiate(item, transform.position, transform.rotation);
                    break;
                case "Y":
                    Destroy(Instantiate(YDeathEffect, transform.position, transform.rotation), 2f);
                    Instantiate(Yitem, transform.position, transform.rotation);
                    break;
                case "G":
                    Destroy(Instantiate(GDeathEffect, transform.position, transform.rotation), 2f);

                    break;

            }



            gameObject.SetActive(false);
        }

        Hpbar.value = Mathf.Lerp(Hpbar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        Hpbar2.value = Mathf.Lerp(Hpbar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

    }

    void Awake()
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
        switch (enemyname)
        {
            case "P":
                Destroy(Instantiate(DamageEffect, transform.position, transform.rotation), 3f);
                break;
            case "Y":
                Destroy(Instantiate(YDamageEffect, transform.position, transform.rotation), 3f);
                break;
            case "G":
                Destroy(Instantiate(GDamageEffect, transform.position, transform.rotation), 3f);
                break;

        }

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
            knockbackDirection.y = 0;
            transform.Translate(knockbackDirection * 1.5f, Space.World);
        }


        GameManager.instance.camerashake.Shake();
        CurHP -= damage;
    }
    void Move()
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
    private void OnTriggerEnter(Collider other)
    {
        if (enemyname == "P")
        {
            if (other.gameObject.tag == "Chain")
            {
                gameObject.tag = "Untagged";
                speed = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (enemyname == "P")
        {
            if (other.gameObject.tag == "Chain")
            {
                gameObject.tag = "Enemy";

                speed = 5;
            }
        }
    }
}