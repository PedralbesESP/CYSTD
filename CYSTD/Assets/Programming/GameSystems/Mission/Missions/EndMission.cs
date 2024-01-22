using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EndMission : BaseMission
{
    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void EnableMission()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            _CompleteMission();
        }
    }
}
