using UnityEngine;

public class Patient : SingletonMonobehaviour<Patient>
{
    public string Name;
    public Part[] Parts => _parts ??= GetComponentsInChildren<Part>();
    Part[] _parts;
}