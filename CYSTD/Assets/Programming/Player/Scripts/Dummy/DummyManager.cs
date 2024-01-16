using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyManager : MonoBehaviour
{
    public static DummyManager dummyManager;
    //Este id estará en otro sitio
    private int _idYourPlayer;
    //public List<GameObject> dummyObjects = new List<GameObject>(4);
    private Dictionary<int, GameObject> dummyObjects;
    private int _spawnDummy;


    public void SetPlayerID(int id)
    {
        _idYourPlayer = id;
    }
    private void Start()
    {
        if (dummyManager != null)
        {
            Debug.LogError("Hay más de 1 dummyManager en la escena");
        }
        dummyManager = this;
        dummyObjects = new Dictionary<int, GameObject>();
    }
    public void assignDummy(GameObject dummy)
    {
        //dummyObjects.Add(dummy);
    }
    public void SpawnDummy(int playerNum)
    {
        if (playerNum != _idYourPlayer)
        {
            dummyObjects[0].SetActive(true);

        }
    }
}
