using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isAnimating = false; 
    public GameObject potal;
    public GameObject title;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            TitleAnim();
        }
    }
    public void TitleAnim()
    {
        if (!isAnimating)
        {
            StartCoroutine(TitleAnimationCor());
        }
    }

    private IEnumerator TitleAnimationCor()
    {
        isAnimating = true; 

        title.SetActive(false);
        GameManager.instance.fadescript.Fade(true);
        potal.SetActive(true);
        animator.SetBool("Run", true);
        yield return new WaitForSeconds(1.4f);
        animator.SetBool("Dash", true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.fadescript.Fade(false);
        yield return new WaitForSeconds(1.7f);

        SceneManager.LoadScene("SG 1");

        isAnimating = false; 
    }
}