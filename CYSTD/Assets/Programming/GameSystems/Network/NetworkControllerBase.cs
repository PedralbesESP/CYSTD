using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages sending and recieving data with the NetworkManager.
/// </summary>
public abstract class NetworkControllerBase : Identifiable
{
    void Start()
    {
        NetworkManager.Instance.AddNetworkController(this);
    }

    /// <summary>
    /// Should return the parameters that the game object is going to send to the server, for the NetworkManager to get and send them.
    /// </summary>
    /// <returns>A ParameterSet with the id of the sender game object and a Dictionary representing the values to send.</returns>
    public abstract ParameterSet SendData();

    /// <summary>
    /// Should read the data sent from the server and set it to the game object.
    /// </summary>
    /// <param name="parameterSet">The ParameterSet sent from the server.</param>
    public abstract void RecieveData(ParameterSet parameterSet);
}
