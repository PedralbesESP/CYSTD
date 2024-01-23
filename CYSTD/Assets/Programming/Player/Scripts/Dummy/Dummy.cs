using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    [SerializeField]private string dummyUid;
    public Text dummyText;
    
    public void SetId(string uid)
    {
        dummyUid = uid;
        if (dummyText != null)
        {
            dummyText.text = dummyUid;
        }
    }
    public string getId()
    {
        return dummyUid;
    }

    public void moveDummy(Vector3 position, Vector3 rotation)
    {
        //position.Vector3ToString
    }

}
