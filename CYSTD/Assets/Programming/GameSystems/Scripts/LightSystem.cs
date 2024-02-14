using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    public List<Light> _light; //cambiar a private
    float lightForce;

    private void Start()
    {
        List<GameObject> lightGameObject = new List<GameObject>();
        foreach(Transform child in transform)
        {
            lightGameObject.Add(child.gameObject);
        }
        foreach (GameObject childLight in lightGameObject)
        {
            Light light = childLight.GetComponentInChildren<Light>();

            if (light != null)
            {
                _light.Add(light);
            }
        }
        lightForce = _light[0].intensity; // lightForce = fuerza inicial

        foreach (Light light in _light)
        {
            SphereCollider cC = light.gameObject.AddComponent<SphereCollider>();
            cC.radius = light.range * 0.5f;
            cC.isTrigger = true;
        }
    }

    private void Update()
    {
        //t += 0.5f * Time.deltaTime;
        //lightForce = Mathf.Lerp(lightForce, 0, t);
        foreach(Light light in _light)
        {
            //light.intensity = lightForce;
            light.intensity = Timer.Instance.TimePercentage;
        }
    }
}
