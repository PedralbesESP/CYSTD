using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

/// <summary>
/// Holds a list of all the NetworkControllers in the game, and handles communications between them and the server, using WebSockets.
/// </summary>
public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    Queue<MessageEventArgs> serverEventQueue = new Queue<MessageEventArgs>();
    Dictionary<string, NetworkControllerBase> _networkControllers;
    Vector3 playerPosition;

    WebSocket _socket;

    const string URL = "ws://192.168.205.68:3003"; //////// SERVER ADDRESS ////////

    void Start()
    {
        playerPosition = GameManager.Instance.getPlayer().transform.position;
        Instance = this;
        _networkControllers = new Dictionary<string, NetworkControllerBase>();
        _socket = new WebSocket(URL);
        _socket.OnMessage += (sender, e) =>
        {
            serverEventQueue.Enqueue(e);
        };
        _socket.Connect();
    }

    private void ProcessEvent(MessageEventArgs messageEventArgs)
    {
        Debug.Log("Información recibida: " + messageEventArgs.Data);

        Info info = new Info();
        info = JsonUtility.FromJson<Info>(messageEventArgs.Data);
        if (info == null)
        {
            Debug.LogError("El mensaje ha llegado vacio");
        }
        else
        {
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
                case "PlayerInfo":
                    GameObject dummy = DummyManager.dummyManager.DummyDictionary[info.data[0].value];
                    if (dummy != null)
                    {
                        dummy.transform.position = info.data[1].value.Vector3FromString();
                        dummy.transform.rotation = Quaternion.Euler(info.data[2].value.Vector3FromString());
                    }
                    break;
                case "PlayerDisconnect":

                default:
                    break;
            }
        }

    }
    private void Update()
    {
        if (serverEventQueue.Count > 0)
        {
            ProcessEvent(serverEventQueue.Dequeue());
        }
        Vector3 pos = GameManager.Instance.getPlayer().transform.position;
        float dif = Vector3.SqrMagnitude(pos - playerPosition);
        if (dif > Vector3.kEpsilon)
        {
            Info message = createNetworkMessage();
            _SendMessage(message);
            playerPosition = GameManager.Instance.getPlayer().transform.position;
        }
        
    }

    IEnumerator waitCo()
    {
        while (true)
        {

            //yield return new WaitForSecondsRealtime(.2f);
        }
    }

    Info createNetworkMessage()
    {
        Info message = new Info();
        message.action = ActionType.PlayerInfo.ToString();
        message.data = new List<Item>();
        message.data.Add(new Item { key = ParamKey.ID.ToString(), value = DummyManager.dummyManager.getPlayerID() });
        message.data.Add(new Item { key = ParamKey.POSITION.ToString(), value = GameManager.Instance.getPlayer().transform.position.Vector3ToString() });
        message.data.Add(new Item { key = ParamKey.ROTATION.ToString(), value = GameManager.Instance.getPlayer().transform.rotation.eulerAngles.Vector3ToString() });

        return message;
    }

    /*
    NetworkMessage _CreateNetworkMessage()
    {
        List<ParameterSet> parametersToSend = new List<ParameterSet>();


        ParameterSet parameters = GameManager.Instance.getPlayer().GetComponent<PlayerNetworkController>().SendData();
        if (parameters != null && parameters.Parameters.Count > 0)
        {
            parametersToSend.Add(parameters);
        }


        if (parametersToSend.Count < 0)
        {
            return null;
        }

        long timestamp = Mathf.RoundToInt(Time.realtimeSinceStartup * 1000);
        return new NetworkMessage(timestamp, parametersToSend);
    }
    */
    void _SendMessage(Info message)
    {
        string json = JsonConvert.SerializeObject(message);
        if (_socket.IsAlive) _socket.Send(json);
    }

    /*

    class NetworkMessage
    {
        public NetworkMessage(long timestamp, List<ParameterSet> parameters)
        {
            _timestamp = timestamp;
            _parameters = parameters;
        }
        long _timestamp;
        List<ParameterSet> _parameters;
        public long Timestamp => _timestamp;
        public List<ParameterSet> ParameterSets => _parameters;
    }
*/

    public class idLIst
    {
        public List<int> listaIds;
    }

    private void OnDestroy()
    {
        _socket.Close();
    }

    [Serializable]
    public class Info
    {
        public string action;
        public List<Item> data;
    }
    [Serializable]
    public class Item
    {
        public string key;
        public string value;
    }

    public enum ActionType
    {
        PlayerInfo,
        SetPlayerId,
        SetDummyId,
        PlayerDisconnect,
    }


}
