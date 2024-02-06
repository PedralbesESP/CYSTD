using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float _sanity = 100;
    [SerializeField] private List<Collider> _lights;
    void Start()
    {
        _sanity = 100;
        _lights = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sanity <= 100)
        {
            if (_lights.Count > 0)
            {
                _sanity += 0.5f * Time.deltaTime;
            }
        }
        if (_lights.Count == 0)
        {
            _sanity -= 1f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            if (!_lights.Contains(other))
            {
                _lights.Add(other);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            _lights.Remove(other);
        }
    }
}
