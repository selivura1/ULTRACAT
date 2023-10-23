using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform _inventoryObject;
    Inventory inventory;
    EntityBase _player;
    private void OnEnable()
    {
        _player = GameManager.PlayerSpawner.GetPlayer(); ;
        inventory = _player.GetComponent<Inventory>();
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < _inventoryObject.childCount; i++)
        {
            Destroy(_inventoryObject.GetChild(i).gameObject);
        }
        foreach (var item in inventory.Items)
        {
            var go = new GameObject(item.name);
            go.AddComponent<Image>().sprite = item.Picture;
            var tooltip = go.AddComponent<TooltipTrigger>();
            tooltip.header = item.name;
            tooltip.content = item.GetDescription();
            go.transform.SetParent(_inventoryObject);
            go.transform.localScale = Vector3.one;
        }
    }
    private void OnDestroy()
    {
        inventory.onItemsReferesh -= Refresh;
    }
}
