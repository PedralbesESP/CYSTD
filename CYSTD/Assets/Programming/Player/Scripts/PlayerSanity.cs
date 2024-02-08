using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [Tooltip("Cantidad de cordura o sanity actual")]
    [SerializeField] private float _sanity = 100;
    [SerializeField]private List<Collider> _lights;
    [Tooltip("Numero con el que se calcula la curación maxima y el resto en consecuencia")]
    [Range(0.1f, 10)]
    [SerializeField] private float factorHeal = 10f;
    [Tooltip("Distancia minima = a la distancia a la que el jugador se cura más rapido.")]
    [Range(1,20)]
    [SerializeField] private float _distanciaMinima = 1f;
    [Tooltip("Numero con el que se hace la operación para restar sanity")]
    [SerializeField] private float resMultiplier;
    float _dif = 0;
    float _lastDif;
    void Start()
    {
        _sanity = 100;
        _lights = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        _dif = 0;
        foreach (Collider light in _lights)
        {
            _lastDif = Vector3.SqrMagnitude(light.transform.position - transform.position);
            if (_lastDif < _dif || _dif == 0)
            {
                _dif = _lastDif;
            }
        }

        if (_sanity <= 100)
        {
            if (_lights.Count > 0)
            {
                _sanity += Mathf.Min(factorHeal*_distanciaMinima / (_dif + float.Epsilon), factorHeal) * Time.deltaTime;
                //Debug.Log(_sanity);

            }
        }
        if (_lights.Count == 0)
        {
            _sanity -= resMultiplier * Time.deltaTime;
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

    private void OnDrawGizmos()
    {
        if (_lights.Count > 0)
        {
            foreach (Collider light in _lights)
            {
                Gizmos.DrawLine(light.transform.position, transform.position);
                Gizmos.color = Color.blue;
                float dif = Vector3.SqrMagnitude(light.transform.position - transform.position);
            }
        }

    }
}
