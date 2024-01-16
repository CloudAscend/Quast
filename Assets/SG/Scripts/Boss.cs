using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    [SerializeField] private GameObject go;
    public int bossPhase;

    private void Attack()
    {
        switch (bossPhase)
        {
            case 1:
                Phase1();
                break;
            case 2:
                Phase2();
                break;
            case 3:
                Phase3();
                break;
            case 4:
                Phase4();
                break;
            case 5:
                Phase5();
                break;
        }
    }

    private IEnumerator Phase1()
    {
        yield return new WaitForSeconds(0);
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
}
