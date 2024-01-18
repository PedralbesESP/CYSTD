using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UseItemMission : BaseMission
{
    [SerializeField]
    List<ItemType> _requiredItems;

    public void UseRequiredItems()
    {
        foreach (ItemType t in Inventory.Instance.GetAvaliableItems())
        {
            if (_requiredItems.Contains(t))
            {
                _requiredItems.Remove(t);
                Destroy(Inventory.Instance.RetrieveItem(t));
            }
        }
        if (_requiredItems.Count < 1) _CompleteMission();
    }
}
