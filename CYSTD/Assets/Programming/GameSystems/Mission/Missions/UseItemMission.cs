using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UseItemMission : BaseMission
{
    [SerializeField]
    List<ItemType> _requiredItems;
    [SerializeField]
    bool _refreshRequirements;

    List<ItemType> _requirements;

    private void Start()
    {
        _requirements = _requiredItems;
    }

    public void AddRequirements(ItemType newItem)
    {
        if (_requirements != null)
        {
            _requirements.Add(newItem);
        }
    }

    public List<ItemType> UseRequiredItems()
    {
        List<ItemType> usedItems = new List<ItemType>();
        foreach (ItemType t in Inventory.Instance.GetAvaliableItems())
        {
            if (_requirements.Contains(t))
            {
                usedItems.Add(t);
                _requirements.Remove(t);
                Destroy(Inventory.Instance.RetrieveItem(t));
            }
        }
        if (_requirements.Count < 1) _CompleteMission();
        return usedItems;
    }

    public override void Enable()
    {
        base.Enable();
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public override void Disable()
    {
        base.Disable();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void ResetMission()
    {
        base.ResetMission();
        if (_refreshRequirements) _requirements = _requiredItems;
    }
}
