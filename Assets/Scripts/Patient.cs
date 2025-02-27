using UnityEngine;

public class Patient : MonoBehaviour
{
    public static Patient refer;
    void Awake() => refer = this;
    
    public string Name;
    public Part[] Parts => _parts ??= GetComponentsInChildren<Part>();
    Part[] _parts;
}