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

    Dictionary<long, NetworkControllerBase> _networkControllers;

    WebSocket _socket;

    const string URL = "ws://192.168.205.68:3003"; //////// SERVER ADDRESS ////////

    void Awake()
    {
        Instance = this;
        _networkControllers = new Dictionary<long, NetworkControllerBase>();
        _socket = new WebSocket(URL);
        _socket.OnMessage += (sender, e) =>
        {
            NetworkMessage recieved = JsonConvert.DeserializeObject<NetworkMessage>(e.Data);
            foreach (var parameterSet in recieved.ParameterSets)
            {
                _networkControllers[parameterSet.SenderId].RecieveData(parameterSet);
            }

        };
        //_socket.Connect();
    }

    void Update()
    {
        NetworkMessage message = _CreateNetworkMessage();
        if (message == null || message?.ParameterSets.Count < 1)
        {
            return;
        }
        //_SendMessage(message);
    }

    NetworkMessage _CreateNetworkMessage()
    {
        List<ParameterSet> parametersToSend = new List<ParameterSet>();

        if (_networkControllers.Count < 0)
        {
            return null;
        }

        foreach (var controller in _networkControllers)
        {
            ParameterSet parameters = controller.Value.SendData();
            if (parameters != null && parameters.Parameters.Count > 0)
            {
                parametersToSend.Add(parameters);
            }
        }

        if (parametersToSend.Count < 0)
        {
            return null;
        }

        long timestamp = Mathf.RoundToInt(Time.realtimeSinceStartup * 1000);
        return new NetworkMessage(timestamp, parametersToSend);
    }

    void _SendMessage(NetworkMessage message)
    {
        string json = JsonConvert.SerializeObject(message);
        if (_socket.IsAlive) _socket.Send(json);
    }

    /// <summary>
    /// Adds the NetworkController to the list to send and recieve data.
    /// </summary>
    public void AddNetworkController(NetworkControllerBase controller)
    {
        try
        {
            _networkControllers.Add(controller.Id, controller);
        }
        catch (ArgumentException)
        {
            _networkControllers[controller.Id] = controller;
        }
    }

    class NetworkMessage {
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
}
