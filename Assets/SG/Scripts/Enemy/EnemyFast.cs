using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : EnemyBase
{
    [SerializeField] Transform BoundaryMax;
    [SerializeField] Transform BoundaryMin;
    [SerializeField] Enemy[] enemys;
    private Vector3 movePos;
    private bool isMove = true;
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(MoveCoroutine());
    }

    protected override void Update()
    {
        base.Update();
        
    }

    private void FixedUpdate()
    {
        Move();
        //Check();
    }

    //private void Check()
    //{
    //    for (int index = 0; index < enemys.Length; index++)
    //    {
    //        if (!enemys[index].enabled) speed -= 10;
    //    }
    //}

    public void Check()
    {
        speed -= 12;
    }

    protected override void Move()
    {
        //base.Move();
        if (!isLive)
            return;

        if (isMove)
        {
            //rigid.MovePosition(movePos.normalized * speed * Time.fixedDeltaTime);
            rigid.velocity = movePos * speed;

            if (Vector3.Distance(movePos, transform.position) <= 0.1f)
            {
                isMove = false;

                if (OutRange()) 
                {
                    Transform posi = gameObject.transform.parent;
                    movePos = new Vector3(posi.position.x - transform.position.x, 0, posi.position.z - transform.position.z).normalized;
                }
                        //StartCoroutine(MoveCoroutine());
            }
        }
    }

    private bool OutRange()
    {

        return ((transform.position.x >= BoundaryMax.position.x ||
                 transform.position.z >= BoundaryMax.position.z) ||
                (transform.position.x <= BoundaryMin.position.x) ||
                 transform.position.z <= BoundaryMin.position.z);
    }

    IEnumerator MoveCoroutine()
    {
        Vector3 player = transform.position;

        Vector3 max = BoundaryMax.position;
        Vector3 min = BoundaryMin.position;
        Vector3 pos = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));

        movePos = new Vector3(pos.x - player.x, 0, pos.z - player.z).normalized;
        //Debug.Log(movePos);
        isMove = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveCoroutine());
    }
}
