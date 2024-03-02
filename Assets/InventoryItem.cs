using GameComp.Interactables;
using GameComp.PlayerConfigs;
using UnityEngine;

public class InventoryItem : Interactable
{
    private Player player;
    public string itemName; // for display
    public int itemID; // used to check validity of items
    public Sprite gfx { get; private set; }

    private void Start() {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
    }   

    protected override void OnInteract()
    {
        Debug.LogWarning("The inventory feature has been removed due to lack of time.");
        return;
        player.TryAddItemToInventory(this);
        this.gameObject.SetActive(false);
        print($"OnInteract() with InventoryItem. Played picked up item {itemName} which has an ID of {itemID}");
    }
}