using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyManager : MonoBehaviour
{
    List<GameObject> dummyObjects = new List<GameObject>(4);

    public void SpawnDummy(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                for (int i = playerNum + 1; i < dummyObjects.Count; i++)
                {
                    dummyObjects[i].SetActive(true);
                }
                break;
            case 1:
                dummyObjects[2].SetActive(true);
                dummyObjects[3].SetActive(true);
                dummyObjects[0].SetActive(true);
                break;
            case 2:
                dummyObjects[3].SetActive(true);
                dummyObjects[0].SetActive(true);
                dummyObjects[1].SetActive(true);
                break;
                case 3:
                for (int i = 0; i < dummyObjects.Count; i++)
                {
                    dummyObjects[i].SetActive(true);
                }
                break;

        }
    }
}
