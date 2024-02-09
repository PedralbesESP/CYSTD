using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    TMP_Text _inventoryText;
    [SerializeField]
    int maxItems;
    Dictionary<ItemType, GameObject> _items;
    Notebook _notebook;
    
    public Notebook Notebook { get => _notebook; }

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
        if (_items.Count <= maxItems && item != null)
        {
            if (item.TryGetComponent(out Notebook n) && _notebook == null)
            {
                _notebook = n;
                item.SetActive(false);
                return true;
            }
            else if (item.TryGetComponent(out Grabbable g) && !_items.ContainsKey(g.ItemType))
            {
                item.SetActive(false);
                var itemCopy = Instantiate(item);
                itemCopy.GetComponent<Grabbable>().ItemType = item.GetComponent<Grabbable>().ItemType;
                _items.Add(g.ItemType, itemCopy);
                _SetInventoryText();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks the inventory for a specific item.
    /// </summary>
    public bool HasItem(ItemType itemType)
    {
        if (itemType.Equals(ItemType.NOTEBOOK)) return _notebook != null;
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
            _SetInventoryText();
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

    void _SetInventoryText()
    { 
        _inventoryText.SetText(string.Join(", ", _items.Select(i => i.Key)));
    }
}
