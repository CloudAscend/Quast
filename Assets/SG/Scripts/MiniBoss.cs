using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniBoss : EnemyBase
{
    Animator anim;
    [SerializeField] Boss mainBoss;
    [SerializeField] Transform BoundaryMax;
    [SerializeField] Transform BoundaryMin;
    private Vector3 movePos;
    private bool isMove;
    private bool isAttack;

    public TMP_Text problemText;
    [HideInInspector] public bool trueProblem;

    [Header("Attack Time")]
    private float attackTime;

    private void Start()
    {
        
    }

    protected override void Update()
    {
        if (!isAttack)
        {
            base.Update();
            //Move();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        isMove = false;
        if (trueProblem)
        {
            mainBoss.TakeDamage(25);
            mainBoss.PhaseEnd();
        }
        else
        {
            GameManager.instance.player.GetComponent<PlayerController>().CurHP -= 2;
        }
    }

    private void OnEnable()
    {
        //isMove = true;
        //StartCoroutine(MoveCoroutine());
        rigid.isKinematic = true;
        StartCoroutine(AttackCoroutine());
    }
    //юс╫ц
    public void ChangeText(string problem)
    {
        problemText.text = problem;
    }

    //public void SanJunipro()
    //{

    //}

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

            GameObject player = GameManager.instance.player;

            if (player == null)
                return;

            Vector3 dirVec = player.transform.position - transform.position;
            spriteRenderer.flipX = dirVec.x < 0 || dirVec.z > 0;
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
        isMove = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        attackTime = Timer.time.timeSec;
        yield return new WaitForSeconds(attackTime);

        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveCoroutine());
        isAttack = true;
        rigid.isKinematic = false;
        problemText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAttack)
        {
            GameManager.instance.player.GetComponent<PlayerController>().CurHP -= 2;
        }
    }
}
