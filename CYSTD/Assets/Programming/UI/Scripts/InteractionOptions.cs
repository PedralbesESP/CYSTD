using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionOptions : MonoBehaviour
{
    [SerializeField]
    TMP_Text _interactionHint;

    public static InteractionOptions Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Activate(string text)
    {
        _Toggle(text);
    }

    public void Deactivate()
    {
        _Toggle(null);
    }

    void _Toggle(string text)
    {
        _interactionHint.enabled = text != null;
        _interactionHint.SetText(text);
    }
}
