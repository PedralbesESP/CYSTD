using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        //Info message = new Info();
        NetworkManager.Info _message = new NetworkManager.Info();
        _message.action = NetworkManager.ActionType.JoinRoom.ToString();
        _message.data = new List<NetworkManager.Item>();
        _message.data.Add(new NetworkManager.Item { key = ParamKey.ID.ToString(), value = transform.parent.gameObject.name });
        NetworkManager.Instance.JoinRoom(_message);
    }

    [ContextMenu("DebugClick")]
    void DebugClick()
    {
        onClick();
    }

}
