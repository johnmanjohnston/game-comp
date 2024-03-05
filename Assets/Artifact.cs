using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    public Image fadeOutter;
    public bool shouldFadeOut;

    public void InitFadeOut()
    {
        fadeOutter.gameObject.SetActive(true);
        fadeOutter.color = new Color(0f, 0f, 0f, 0f);

        Invoke(nameof(ActivateFadeOutBool), 5f);
    }

    private void ActivateFadeOutBool() { shouldFadeOut = true; }

    private void Update()
    {
        if (shouldFadeOut)
        {
            fadeOutter.color = new Color(0f, 0f, 0f, fadeOutter.color.a + Time.deltaTime * 1.5f);

            if (fadeOutter.color.a > 0.99f)
            {
                FadeOutAndLoadMainMenu();
            }
        }
    }

    public void FadeOutAndLoadMainMenu()
    {
        SceneManager.LoadScene(0); // load main menu
    }
}
