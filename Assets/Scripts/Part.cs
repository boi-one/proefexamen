using System;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Affliction[] Afflictions;

    void Awake()
    {
        // Init afflictions
        foreach (var affliction in Afflictions)
        {
            affliction.Amount = 1f;
        }
    }
}