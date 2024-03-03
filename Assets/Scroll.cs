using UnityEngine;
using System.Collections;
using TMPro;

public class Scroll : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject scrollContentContainer;

    public Animator animator;

    private void Start()
    {
        scrollContentContainer.SetActive(false);
    }

    public void ShowScrollText(string scrollText, float t)
    {
        scrollContentContainer.SetActive(true);
        text.text = scrollText;
        animator.SetBool("shouldBeUp", true);

        StartCoroutine(HideScroll(t));
    }

    private IEnumerator HideScroll(float time)
    {
        yield return new WaitForSeconds(time - 0.5f);
        animator.SetBool("shouldBeUp", false);

        yield return new WaitForSeconds(0.5f);
        scrollContentContainer.SetActive(false);
    }
}