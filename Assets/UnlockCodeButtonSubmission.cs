using TMPro;
using UnityEngine;

public class UnlockCodeButtonSubmission : MonoBehaviour
{
    public TMP_InputField inputField;
    public UnlockCodeInteractable interactable;

    public GameObject gfxContainer;

    public void Start()
    {
        DisableGFXContainer();
    }

    public void VerifyCode()
    {
        if (inputField.text == interactable.unlockCodeSolution)
        {
            print("correct code");
            interactable.OnCorrectCode();
        } else
        {
            print("wrong code");
            print(inputField.text);
        }
    }

    public void EnableGFXContainer() { gfxContainer.SetActive(true); }
    public void DisableGFXContainer() { gfxContainer.SetActive(false); }
}
