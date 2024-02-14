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

    [SerializeField] private Light_State InitialState;

    [SerializeField] private List<LightEvent> _lightEvents = new List<LightEvent>();
    
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
            if (_sanity <= 90)
            {
                if (!_lightEvents[0].IsActive())
                {
                    _lightEvents[0].PlayEvent();
                }
                for(int i = 1; i < _lightEvents.Count; i++ )
                {
                    _lightEvents[i].StopEvent();
                }
                
                
            }
            if(_sanity<= 80)
            {
                if (!_lightEvents[1].IsActive())
                {
                    _lightEvents[1].PlayEvent();
                }
                for (int i = 2; i < _lightEvents.Count; i++)
                {
                    _lightEvents[i].StopEvent();
                }
            }
            if(_sanity<= 70)
            {
                if (!_lightEvents[2].IsActive())
                {
                    _lightEvents[2].PlayEvent();
                }
                for (int i = 3; i < _lightEvents.Count; i++)
                {
                    _lightEvents[i].StopEvent();
                }
            }
            if (_sanity <= 60)
            {

            }
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
