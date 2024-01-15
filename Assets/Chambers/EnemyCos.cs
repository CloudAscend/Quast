using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCos : MonoBehaviour
{
    [SerializeField] private Transform maxB;
    [SerializeField] private Transform minB;
    [SerializeField] private float speed;
    private Rigidbody2D rigid;
    private Vector3 movePos;
    private bool isMove;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        if (isMove)
        {
            //rigid.MovePosition(movePos.normalized * speed * Time.fixedDeltaTime);
            rigid.velocity = movePos * speed;

            if (Vector3.Distance(movePos, transform.position) <= 0.1f)
            {
                isMove = false;
            }
        }
    }

    IEnumerator MoveCoroutine()
    {
        Vector3 player = transform.position;

        Vector3 max = maxB.position;
        Vector3 min = minB.position;
        Vector3 pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        Debug.Log(pos);
        movePos = new Vector2(pos.x - player.x, pos.y - player.y).normalized;
        isMove = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveCoroutine());
    }
}
