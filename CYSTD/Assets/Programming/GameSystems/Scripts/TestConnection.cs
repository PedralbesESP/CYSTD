using WebSocketSharp;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private InputActionAsset testConection;
    private InputAction connect;

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
            Debug.Log("Message recived from " + ((WebSocket)sender).Url + ", Data :" + e.Data);
        };
        ws.Connect();
    }

    
    
    void sendInfo(InputAction.CallbackContext ctx)
    {
        Debug.Log("Send cositas");
        ws.Send("Hello");
    }
}
