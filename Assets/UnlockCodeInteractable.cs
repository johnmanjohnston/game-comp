using UnityEngine;
using GameComp.Interactables;
using UnityEngine.Events;

public class UnlockCodeInteractable : Interactable
{
    public UnityEvent onEnterSuccessfulCode;
    public string unlockCodeSolution;

    private UnlockCodeButtonSubmission btnSubmission;

    private void Start()
    {
        btnSubmission = FindFirstObjectByType<UnlockCodeButtonSubmission>();
    }

    protected override void OnInteract()
    {
        btnSubmission.interactable = this;
        btnSubmission.EnableGFXContainer();
    }

    protected override void OnPlayerExitRange()
    {
        base.OnPlayerExitRange();
        btnSubmission.DisableGFXContainer();
    }

    public void OnCorrectCode()
    {
        onEnterSuccessfulCode.Invoke();

        OnPlayerExitRange();
        Destroy(this);
    }
}