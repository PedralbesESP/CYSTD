using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    [SerializeField]private string dummyUid;
    public Text dummyText ;
    Vector3 desiredPosition;
    Quaternion desiredRotation;
    [SerializeField] private float lerpSpeed = 0.1f;

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

    public void movePosition(Vector3 desiredPos, Quaternion desiredRot)
    {
        this.desiredPosition = desiredPos;
    }
    private void Update()
    {
        if(desiredPosition!= Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lerpSpeed);
        }
    }
}
