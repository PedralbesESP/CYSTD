using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UseItemMission : BaseMission
{
    [SerializeField]
    List<ItemType> _requiredItems;

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
}
