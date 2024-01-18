using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int maxItems;
    Dictionary<ItemType, GameObject> _items;

    public static Inventory Instance;

    void Awake()
    {
        Instance = this;
        _items = new Dictionary<ItemType, GameObject>(maxItems);
    }

    /// <summary>
    /// Adds the item to the inventory, returns true if the addition was successful and false if it wasn't added.
    /// </summary>
    public bool AddItem(GameObject item)
    {
        if (_items.Count <= maxItems && 
            item != null &&
            item.TryGetComponent(out Grabbable g) &&
            !_items.ContainsKey(g.ItemType))
        {
            item.SetActive(false);
            _items.Add(g.ItemType, item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks the inventory for a specific item.
    /// </summary>
    public bool HasItem(ItemType itemType)
    {
        return _items.ContainsKey(itemType);
    }

    /// <summary>
    /// Removes the item from the inventory and returns it for further use.
    /// </summary>
    public GameObject RetrieveItem(ItemType itemType)
    {
        if (_items.TryGetValue(itemType, out GameObject i))
        {
            i.transform.position = transform.position;
            i.SetActive(true);
            _items.Remove(itemType);
            return i;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns a list with all avaliable item types on the inventory.
    /// </summary>
    /// <returns></returns>
    public List<ItemType> GetAvaliableItems()
    {
        return new List<ItemType>(_items.Keys);
    }
}
