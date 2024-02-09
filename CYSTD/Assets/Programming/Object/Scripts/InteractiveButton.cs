using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractiveButton : MonoBehaviour
{
    bool _pressed;
    bool _enabled;
    public delegate void MyEventHandler(bool pressed);
    public static event MyEventHandler OnPressed;

    public bool IsPressed()
    {
        return _pressed;
    }

    public void Press()
    {
        if (!_enabled) return;
        _pressed = !_pressed;
        OnPressed?.Invoke(_pressed);
    }

    public void Enable()
    {
        _enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
