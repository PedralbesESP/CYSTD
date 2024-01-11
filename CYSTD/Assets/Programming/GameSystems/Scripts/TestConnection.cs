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
    string URL = "ws://localhost:3003";

    void Start()
    {
        connect = testConection.FindActionMap("test").FindAction("connection");
        connect.performed += sendInfo;
        connect.Enable();

        ws = new WebSocket(URL);


        ws.OnMessage += (sender, e) =>
        {
            serverEventQueue.Enqueue(e);
            Debug.Log("Message recived from " + ((WebSocket)sender).Url + ", Data :" + e.Data);
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
        //Llamar el método que procesa el ParameterSet y actualiza lo que toca con los valores recibidos
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
}
