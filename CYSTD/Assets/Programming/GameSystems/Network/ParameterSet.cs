using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a message sent from a game object. Contains the game objects id and a parameter list.
/// </summary>
public class ParameterSet
{
    string _senderId;
    Dictionary<int, string> _parameters;

    public string SenderId
    {
        get => _senderId;
        set { _senderId = value; }
    }

    public Dictionary<int, string> Parameters
    {
        get => _parameters;
        set { _parameters = value; }
    }

    public ParameterSet(string senderId)
    {
        _senderId = senderId;
        _parameters = new Dictionary<int, string>();
    }

    public ParameterSet(string senderId, Dictionary<int, string> parameters)
    {
        _senderId = senderId;
        _parameters = parameters;
    }

    /// <summary>
    /// Adds a parameter to the list. If the key is already in the list, it replaces the value with the new one.
    /// </summary>
    /// <param name="key">The id of the variable that is going to be sent.</param>
    /// <param name="value">The new value of that variable.</param>
    public void AddParameter(int key, string value)
    {
        if (_parameters == null)
        {
            return;
        }
        try
        {
            _parameters.Add(key, value);
        } 
        catch (ArgumentException)
        {
            _parameters[key] = value;
        }
    }

    /// <summary>
    /// Adds a parameter to the list. If the key is already in the list, it replaces the value with the new one.
    /// </summary>
    /// <param name="key">The id of the variable that is going to be sent.</param>
    /// <param name="value">The new value of that variable.</param>
    public void AddParameter(ParamKey key, string value)
    {
        AddParameter((int)key, value);
    }

    /// <summary>
    /// Adds a parameter to the list. If the key is already in the list, it replaces the value with the new one.
    /// </summary>
    /// <param name="key">The id of the variable that is going to be sent.</param>
    /// <param name="value">The new value of that variable.</param>
    public void AddParameter(ParamKey key, double value)
    {
        AddParameter((int)key, value.ToString());
    }

    /// <summary>
    /// Adds a parameter to the list. If the key is already in the list, it replaces the value with the new one.
    /// </summary>
    /// <param name="key">The id of the variable that is going to be sent.</param>
    /// <param name="value">The new value of that variable.</param>
    public void AddParameter(ParamKey key, long value)
    {
        AddParameter((int)key, value.ToString());
    }

}
