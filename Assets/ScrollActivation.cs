using GameComp.Interactables;
using UnityEngine.Events;

public class ScrollActivation : Interactable
{
    private Scroll scroll;
    public bool deactivateAfterViewingScroll;

    public string scrollText;
    public float lifetime;

    public UnityEvent onView;

    private void Start()
    {
        scroll = FindFirstObjectByType<Scroll>();
        scrollText = scrollText.Replace("`", "\n");
    }

    protected override void OnInteract()
    {
        scroll.ShowScrollText(scrollText, lifetime);
        onView.Invoke();

        if (deactivateAfterViewingScroll)
        {
            gameObject.SetActive(false);
        }
    }
}
