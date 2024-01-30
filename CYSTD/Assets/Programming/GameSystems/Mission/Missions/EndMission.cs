using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EndMission : BaseMission
{
    void Awake()
    {
        Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            _CompleteMission();
        }
    }

    public override void Enable()
    {
        base.Enable();
        gameObject.GetComponent<BoxCollider>().enabled = true;
        if (gameObject.TryGetComponent(out MeshRenderer m)) m.enabled = true; // SACAR
    }

    public override void Disable()
    {
        base.Disable();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        if (gameObject.TryGetComponent(out MeshRenderer m)) m.enabled = false; // SACAR
    }
}
