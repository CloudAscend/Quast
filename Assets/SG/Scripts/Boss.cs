using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Problem
{
    public string question;
    public bool isTrue;
}

public class Boss : EnemyBase
{
    [SerializeField] private Canvas canvas;
    private BoxCollider box;
    private float timeRate;
    private bool isAttack = true;
    public int bossPhase;

    [Header("Mini Boss")]
    [SerializeField] private GameObject[] miniBoss;

    [Header("Problems")]
    [SerializeField] private int countTime;
    [SerializeField] private List<Problem> problem;

    [Header("Phase HP")]
    [SerializeField] private float phase1hp;
    [SerializeField] private float phase2hp;

    protected override void Awake()
    {
        base.Awake();
        box = GetComponent<BoxCollider>();
        isAttack = true;
    }

    protected override void Update()
    {
        base.Update();
        if (CurHP <= phase1hp && isAttack)
        {
            Attack();
        }
        else if (CurHP <= phase2hp && isAttack)
        {
            bossPhase = 2;
            Attack();
        }
        Move();
    }

    private void Attack()
    {
        isAttack = false;

        switch (bossPhase)
        {
            case 1:
                StartCoroutine(Phase1()); //그림자 분신술 퀴즈
                break;
            case 2:
                StartCoroutine(Phase2()); //포네이도
                break;
            case 3:
                StartCoroutine(Phase3());
                break;
            case 4:
                StartCoroutine(Phase4());
                break;
            case 5:
                StartCoroutine(Phase5());
                break;
        }
    }

    private void Move()
    {
        Vector3 player = GameManager.instance.player.transform.position;
        Vector3 moveVec = new Vector3(player.x - transform.position.x, 0, 
                                      player.z - transform.position.z);

        rigid.velocity = moveVec * speed;

        Vector3 dirVec = player - transform.position;

        spriteRenderer.flipX = dirVec.x < 0 || dirVec.z > 0 == false;
    }

    private IEnumerator Phase1()
    {
        Timer.time.timeSec = countTime;

        int length = miniBoss.Length;

        List<string> pList = new List<string> { };

        for (int i = 0; i < length; i++)
        {
            pList.Add(problem[i].question);
        }

        for (int i = 0; i < length; i++)
        {
            float radian = (360f / 5) * i * Mathf.Deg2Rad;
            float radius = 15f;

            string proSen = pList[UnityEngine.Random.Range(0, pList.Count)];

            GameObject mBoss = miniBoss[i];
            mBoss.SetActive(true);
            mBoss.GetComponent<MiniBoss>().ChangeText(proSen);
            mBoss.GetComponent<MiniBoss>().trueProblem = problem[i].isTrue;
            mBoss.transform.position = new Vector3(transform.position.x + Mathf.Cos(radian) * radius, 3.6f, 
                                                         transform.position.z + Mathf.Sin(radian) * radius);

            pList.Remove(proSen);
        }

        OffActivate();
        Timer.time.TimerStart();
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator Phase2()
    {
        yield return new WaitForSeconds(0);
    }

    private IEnumerator Phase3()
    {
        yield return new WaitForSeconds(0);
    }

    private IEnumerator Phase4()
    {
        yield return new WaitForSeconds(0);
    }

    private IEnumerator Phase5()
    {
        yield return new WaitForSeconds(0);
    }

    //Phase 1, 2, 3, 4, 5

    private void OffActivate()
    {
        box.enabled = false;
        canvas.enabled = false;
        spriteRenderer.enabled = false;
    }

    private void OnActivate()
    {
        box.enabled = true;
        canvas.enabled = true;
        spriteRenderer.enabled = true;
    }

    public void PhaseEnd()
    {
        for (int i = 0; i < miniBoss.Length; i++)
        {
            miniBoss[i].SetActive(false);
        }

        isAttack = true;
        OnActivate();
    }
}
