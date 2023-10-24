using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemDisplayerUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] ProgressBar _chargeBar;
    [SerializeField] TooltipTrigger _activeTrigger;
    Inventory inventory;
    EntityBase _player;
    private void Start()
    {
        _player = GameManager.PlayerSpawner.GetPlayer();
        inventory = _player.GetComponent<Inventory>();
        inventory.onActiveChanged += Refresh;
        Refresh();
    }
    public void Refresh()
    {
        _image.sprite = inventory.ActiveItem.Picture;
        _activeTrigger.header = inventory.ActiveItem.name;
        _activeTrigger.content = inventory.ActiveItem.GetDescription();
    }
    private void OnDestroy()
    {
        inventory.onActiveChanged -= Refresh;
    }
    private void FixedUpdate()
    {
        _chargeBar.CurrentValue = inventory.ActiveItem.Charge;
        _chargeBar.Max = inventory.ActiveItem.MaxCharge;
    }
}