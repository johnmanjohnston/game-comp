using GameComp.Interactables;
using UnityEngine;

public class DemoInteractable : Interactable
{
    protected override void OnInteract()
    {
        print("OnInteract() has been overriden by the DemoInteractable class");
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(0, 4);
    }

    protected override void OnPlayerEnterRange()
    {
        base.OnPlayerEnterRange();
        print("OnPlayerEnterRange() has been called. Called the base implementation as well.");
    }

    protected override void OnPlayerExitRange()
    {
        base.OnPlayerExitRange();
        print("OnPlayerExitRange() has been called. Called the base implementation as well.");
    }
}