using WebSocketSharp;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private InputActionAsset testConection;
    private InputAction connect;

    WebSocket ws;
    string URL = "ws://140.238.221.197";

    void Start()
    {
        connect = testConection.FindActionMap("test").FindAction("connection");
        connect.performed += sendInfo;
        connect.Enable();

        ws = new WebSocket(URL);


        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message recived from " + ((WebSocket)sender).Url + ", Data :" + e.Data);

            //Join or create session and create dummy if there are ppl in that session. 
        };
        ws.Connect();
    }

    
    
    void sendInfo(InputAction.CallbackContext ctx)
    {
        Debug.Log("Send cositas");
        
        ws.Send("Hello");
    }
}
