using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    bool _grabbed;
    public bool Grabbed
    {
        get => _grabbed;
        set
        {
            if (value) 
            _grabbed = value;
        }
    }

    [SerializeField]
    ItemType _itemType;
    public ItemType ItemType
    {
        get => _itemType;
        set
        {
            _itemType = value;
        }
    }
}
