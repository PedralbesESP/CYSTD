using UnityEngine;

public class Identifiable : MonoBehaviour
{
    long _id = -1;
    public long Id
    {
        get => _id;
        set => _id = value;
    }
    void Awake() { this.GenerateID(); }
}
