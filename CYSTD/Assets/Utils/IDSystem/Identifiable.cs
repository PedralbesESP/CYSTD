using UnityEngine;

public class Identifiable : MonoBehaviour
{
    public long id;
    public Identifiable() { this.GenerateID(); }
}
