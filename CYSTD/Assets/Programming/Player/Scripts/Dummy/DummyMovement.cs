using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyManager : MonoBehaviour
{
    List<GameObject> dummyObjects = new List<GameObject>(4);

    public void SpawnDummy(int playerNum)
    {
        for(int i=0; i < dummyObjects.Count; i++)
        {
            if (i != playerNum)
            {
                dummyObjects[i].SetActive(true);
            }
        }
    }
}
