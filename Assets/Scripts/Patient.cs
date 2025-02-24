using UnityEngine;

public class Patient : MonoBehaviour
{ 
    public string Name;
    public Part[] Parts => _parts ??= GetComponentsInChildren<Part>();
    Part[] _parts;
}