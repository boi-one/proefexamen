using UnityEngine;

public class Patient : MonoBehaviour
{
    public static Patient instance;
    void Awake() => instance = this;
    
    
    public string Name;
    public Part[] Parts => _parts ??= GetComponentsInChildren<Part>();
    Part[] _parts;
}