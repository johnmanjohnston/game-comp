using GameComp.Interactables;

public class ScrollActivation : Interactable
{
    private Scroll scroll;
    public bool deactivateAfterViewingScroll;

    public string scrollText;
    public float lifetime;

    private void Start()
    {
        scroll = FindFirstObjectByType<Scroll>();
        scrollText = scrollText.Replace("`", "\n");
    }

    protected override void OnInteract()
    {
        scroll.ShowScrollText(scrollText, lifetime);

        if (deactivateAfterViewingScroll)
        {
            gameObject.SetActive(false);
        }
    }
}
