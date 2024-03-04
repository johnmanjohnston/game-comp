using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsObj;
    public Image fadeOutter;

    public bool shouldFadeOut;

    private void Start()
    {
        creditsObj.SetActive(false);
        fadeOutter.gameObject.SetActive(false);
    }

    public void PlayButton()
    {
        fadeOutter.gameObject.SetActive(true);
        shouldFadeOut = true;
    }

    public void Credits()
    {
        creditsObj.SetActive(true);
    }

    public void QuitApp()
    {
        print("Application quit");
        Application.Quit();
    }

    public void Update()
    {
        if (shouldFadeOut)
        {
            fadeOutter.color = new Color(0f, 0f, 0f, fadeOutter.color.a + Time.deltaTime * 1.5f);

            if (fadeOutter.color.a > 0.99f)
            {
                LoadGameScene();
            }
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
