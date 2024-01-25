using WebSocketSharp;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class TestConnection : MonoBehaviour
{
   /* Queue<MessageEventArgs> serverEventQueue = new Queue<MessageEventArgs>();
    WebSocket ws;
    string URL = "ws://localhost:3003"; //Server address

    void Start()
    {
        ws = new WebSocket(URL);
        ws.OnMessage += (sender, e) =>
        {
            serverEventQueue.Enqueue(e);
        };
        ws.Connect();
    }

    private void Update()
    {
        if (serverEventQueue.Count > 0)
        {
            ProcessEvent(serverEventQueue.Dequeue());
        }
    }

    private void ProcessEvent(MessageEventArgs messageEventArgs)
    {
        Debug.Log("Información recibida: " + messageEventArgs.Data);

        Info info = new Info();
        info = JsonUtility.FromJson<Info>(messageEventArgs.Data);
        switch (info.action)
        {
            case "SetPlayerId":
                if (DummyManager.dummyManager.getPlayerID() != info.data[0].value)
                {
                    DummyManager.dummyManager.SetPlayerID(info.data[0].value);
                }
                break;
            case "SetDummyId":
                foreach (Item data in info.data)
                {
                    //DummyManager.dummyManager.saveId(data.value);
                    DummyManager.dummyManager.AssignToDictionary(data.value);
                }
                break;
        }
    }

    public class idLIst
    {
        public List<int> listaIds;
    }
    void sendInfo(InputAction.CallbackContext ctx)
    {
        Debug.Log("Send cositas");
        ws.Send("Hello");

    }

    private void OnDestroy()
    {
        ws.Close();
    }

    [Serializable]
    public class Info
    {
        public string action;
        public Item[] data;
    }
    [Serializable]
    public class Item
    {
        public string key;
        public string value;
    }
   */
}
