using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

/// <summary>
/// Holds a list of all the NetworkControllers in the game, and handles communications between them and the server, using WebSockets.
/// </summary>
public class NetworkManager : MonoBehaviour
{
    [SerializeField] float distanceUpdateThreshold = 0.1f;

    public static NetworkManager Instance;
    Queue<MessageEventArgs> serverEventQueue = new Queue<MessageEventArgs>();
    Vector3 playerPosition;
    Quaternion playerRotation;
    [SerializeField] private string _idYourPlayer = null;
    [SerializeField] private string _yourRoom = null;
    public List<string> otherPlayersId;
    private List<Item> roomItemList;
    [SerializeField] private GameObject roomListObject;

    WebSocket _socket;
    bool isIngame = false;

    List<Transform> _buttonList = new List<Transform>();

    const string URL = "ws://192.168.205.68:3003"; //////// SERVER ADDRESS ////////

    [SerializeField] private GameObject _rooms;
    [SerializeField] private bool useEpsilon;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        roomItemList = new List<Item>();
        if (isIngame)
        {
            playerPosition = GameManager.Instance.getPlayer().transform.position;
        }

        Instance = this;
        _socket = new WebSocket(URL);
        _socket.OnMessage += (sender, e) =>
        {
            serverEventQueue.Enqueue(e);
        };
        _socket.Connect();
    }

    private void ProcessEvent(MessageEventArgs messageEventArgs)
    {
        //Debug.Log("Informaci�n recibida: " + messageEventArgs.Data);

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
                    if (_idYourPlayer != info.data[0].value)
                    {
                        _idYourPlayer = info.data[0].value;
                    }
                    break;
                case "SetDummyId":
                    foreach (Item data in info.data)
                    {
                        //DummyManager.dummyManager.saveId(data.value);
                        if (!otherPlayersId.Contains(data.value))
                        {
                            otherPlayersId.Add(data.value);
                            DummyManager.dummyManager.AssignToDictionary(data.value);
                        }

                    }
                    break;
                case "PlayerInfo":
                    GameObject dummy;
                    try
                    {
                        dummy = DummyManager.dummyManager.DummyDictionary[info.data[0].value];
                    }
                    catch (Exception)
                    {
                        break;
                    }
                    if (dummy != null)
                    {
                        //dummy.transform.position = info.data[1].value.Vector3FromString();
                        dummy.GetComponent<Dummy>().movePosition(info.data[1].value.Vector3FromString(), Quaternion.Euler(info.data[2].value.Vector3FromString()));
                    }
                    break;
                case "PlayerDisconnect":
                    break;
                case "CreateRoom":
                    _yourRoom = info.data[0].value;
                    MainMenuManager.Instance.SetId(_yourRoom);
                    MainMenuManager.Instance.SetPlayersText(info.data.Count);
                    break;
                case "GetRooms":
                    for (int i = 0; i < info.data.Count; i++)
                    {
                        GameObject go = Instantiate(roomListObject, _rooms.transform);
                        go.name = info.data[i].value;

                        go.GetComponentInChildren<RoomPreview>().SetRoomInfo(info.data[i].value, info.data[i].key);

                        //go.GetComponentsInChildren<TextMeshPro>()[0].text = "Id: " + info.data[i].value;
                        //go.GetComponentsInChildren<TextMeshPro>()[0].text = info.data.Count + "/4";
                        //go.GetComponentsInChildren<TextMeshPro>()[0].text = info.data.Count + "/4";

                        roomItemList.Add(info.data[i]);
                        //_buttonList[i].name = info.data[i].value;
                        //_buttonList[i].GetComponent<Button>().onClick.AddListener(delegate { JoinRoom(_buttonList[i].name); });
                    }
                    break;
                case "JoinRoom":
                    Debug.Log(_yourRoom);
                    if (string.IsNullOrEmpty(_yourRoom))
                    {
                        _yourRoom = info.data[0].key;
                        MainMenuManager.Instance.JoinRoom();
                        MainMenuManager.Instance.SetPlayersText(info.data.Count);
                        MainMenuManager.Instance.SetId(_yourRoom);
                    }
                    else if (_yourRoom == info.data[0].key)
                    {
                        //otherPlayersId.Add(info.data[0].value);
                        MainMenuManager.Instance.SetPlayersText(info.data.Count);
                    }
                    break;
                case "StartGame":
                    SceneLoader.Instance.LoadScene("MainScene");
                    break;
                default:
                    break;
            }
        }

    }

    float GetDistanceUpdateThreshold(bool useEpsilon = false)
    {
        return useEpsilon ? Vector3.kEpsilon : distanceUpdateThreshold;
    }
    private void Update()
    {
        if (serverEventQueue.Count > 0)
        {
            ProcessEvent(serverEventQueue.Dequeue());
        }
        if (isIngame)
        {
            Vector3 pos = GameManager.Instance.getPlayer().transform.position;
            float dif = Vector3.SqrMagnitude(pos - playerPosition);
            float difRot = Vector3.SqrMagnitude(GameManager.Instance.getPlayer().transform.rotation.eulerAngles - playerRotation.eulerAngles);
            if (dif > GetDistanceUpdateThreshold(useEpsilon) || difRot > GetDistanceUpdateThreshold(useEpsilon)) //Cuando el jugador se mueve o rota se envia su posición.
            {
                Info message = createNetworkMessage();
                _SendMessage(message);
                playerPosition = GameManager.Instance.getPlayer().transform.position;
                playerRotation = GameManager.Instance.getPlayer().transform.rotation;
            }
        }
    }


    Info createNetworkMessage()
    {
        Info message = new Info();
        message.action = ActionType.PlayerInfo.ToString();
        message.data = new List<Item>();
        message.data.Add(new Item { key = _yourRoom, value = _idYourPlayer });
        message.data.Add(new Item { key = ParamKey.POSITION.ToString(), value = GameManager.Instance.getPlayer().transform.position.Vector3ToString() });
        message.data.Add(new Item { key = ParamKey.ROTATION.ToString(), value = GameManager.Instance.getPlayer().transform.rotation.eulerAngles.Vector3ToString() });

        return message;
    }


    public void CreateRoom()
    {
        Info message = new Info();
        message.action = ActionType.CreateRoom.ToString();
        message.data = new List<Item>();

        _SendMessage(message);
    }


    public void GetRooms()
    {
        Info message = new Info();
        message.action = ActionType.GetRooms.ToString();
        _SendMessage(message);
    }
    public void JoinRoom(Info message)
    {
        _SendMessage(message);
    }

    public void StartGame()
    {
        Info message = new Info();
        message.action = ActionType.StartGame.ToString();
        message.data = new List<Item>();
        message.data.Add(new Item { key = ParamKey.ID.ToString(), value = _yourRoom });

        _SendMessage(message);
    }

    public void GameStarted()
    {
        Info message = new Info();
        message.action = ActionType.SetDummyId.ToString();
        message.data = new List<Item>();
        message.data.Add(new Item { key = _yourRoom, value = _idYourPlayer });

        _SendMessage(message);
    }

    public void MissionChangeState(BaseMission mission)
    {
        Info message = new Info();
        message.action = ActionType.MissionChange.ToString();
        message.data = new List<Item>();
        message.data.Add(new Item { key = _yourRoom, value = mission.GetMissionState().ToString() });
    }

    void _SendMessage(Info message)
    {
        string json = JsonConvert.SerializeObject(message);
        if (_socket.IsAlive) _socket.Send(json);
    }

    public void IsInGame()
    {
        isIngame = true;
        GameStarted();
    }


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
        CreateRoom,
        GetRooms,
        JoinRoom,
        LeaveRoom,
        StartGame,
        EndGame,
        MissionChange
    }



    public string getPlayerID()
    {
        return _idYourPlayer;
    }

    public void SetPlayerID(string id)
    {
        _idYourPlayer = id;
    }

}