using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
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
    }
}
