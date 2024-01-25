using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    [SerializeField]private string dummyUid;
    public Text dummyText ;

    private void Start()
    {
        dummyText.text = "";
    }
    public void SetId(string uid)
    {
        dummyUid = uid;
        if (dummyText != null)
        {
            dummyText.text = dummyText.text + "id: "+ dummyUid;
        }
    }
    public string getId()
    {
        return dummyUid;
    }

}
