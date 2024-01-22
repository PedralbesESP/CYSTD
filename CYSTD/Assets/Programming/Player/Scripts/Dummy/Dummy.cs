using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    public string dummyUid;
    public Text dummyText;
    
    public void SetId(string uid)
    {
        dummyUid = uid;
        if (dummyText != null)
        {
            dummyText.text = dummyUid;
        }
    }

}
