using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typing : MonoBehaviour
{

    public TMP_Text tx;
    private string m_text = "탈출에 성공하였다\n-END-";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
    }
    IEnumerator _typing()
    {
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}


