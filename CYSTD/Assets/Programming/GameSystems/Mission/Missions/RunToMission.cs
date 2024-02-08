using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RunToMission : BaseMission
{
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
        gameObject.SetActive(true);
    }

    public override void Disable()
    {
        base.Disable();
        gameObject.SetActive(false);
    }
}
