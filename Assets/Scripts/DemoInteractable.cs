using GameComp.Interactables;
using UnityEngine;

public class DemoInteractable : Interactable
{
    protected override void OnInteract()
    {
        print("OnInteract() has been overriden by the DemoInteractable class");
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(0, 4);
    }
}