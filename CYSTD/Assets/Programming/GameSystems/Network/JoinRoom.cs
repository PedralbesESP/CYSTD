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
        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y, 0);
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

}
