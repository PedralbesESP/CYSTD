using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionOptions : MonoBehaviour
{
    [SerializeField]
    TMP_Text _interactionHint;
    bool _isActiveWithTime = false;

    public static InteractionOptions Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ActivateWithTime(string text, float seconds)
    {
        if (!_isActiveWithTime)
        {
            _isActiveWithTime = true;
            Activate(text);
            StartCoroutine(DeactivateAfterSeconds(seconds));
        }
    }

    IEnumerator DeactivateAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Deactivate();
        _isActiveWithTime = false;
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
