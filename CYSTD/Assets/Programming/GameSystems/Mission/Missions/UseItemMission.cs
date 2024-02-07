using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UseItemMission : BaseMission
{
    [SerializeField]
    List<ItemType> _requiredItems;

    public void AddRequirements(ItemType newItem)
    {
        if (_requiredItems != null)
        {
            _requiredItems.Add(newItem);
        }
    }

    public List<ItemType> UseRequiredItems()
    {
        List<ItemType> usedItems = new List<ItemType>();
        foreach (ItemType t in Inventory.Instance.GetAvaliableItems())
        {
            if (_requiredItems.Contains(t))
            {
                usedItems.Add(t);
                _requiredItems.Remove(t);
                Destroy(Inventory.Instance.RetrieveItem(t));
            }
        }
        if (_requiredItems.Count < 1) _CompleteMission();
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
}
