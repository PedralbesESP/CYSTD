using WebSocketSharp;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private InputActionAsset testConection;
    private InputAction connect;
    Queue<MessageEventArgs> serverEventQueue = new Queue<MessageEventArgs>();
    WebSocket ws;
    string URL = "ws://localhost:3003"; //Server address

    void Start()
    {
        connect = testConection.FindActionMap("test").FindAction("connection");
        connect.performed += sendInfo;
        connect.Enable();

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


        /* if (messageEventArgs.Data.Contains("listaIds"))
         {
             idLIst data = JsonUtility.FromJson<idLIst>(messageEventArgs.Data);
             foreach (int id1 in data.listaIds)
             {
                 DummyManager.dummyManager.SpawnDummy(id1);
             }
             //DummyManager.dummyManager.SpawnDummy();

         }*/
        //Llamar el método que procesa el ParameterSet y actualiza lo que toca con los valores recibidos
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
}
