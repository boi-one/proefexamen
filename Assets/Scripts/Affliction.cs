using System;
using UnityEngine;
using UnityEngine.Events;

public enum AfflictionType
{
    dirt,
    blood,
    hole
}

[Serializable]
public class Affliction
{
    public AfflictionType Name;
    public float Amount
    {
        get => _amount;
        set
        {
            if (_amount == value)
                return;

            value = Mathf.Clamp(value, 0f, 1f);
            _amount = value;

            switch (value)
            {
                case 1f:
                    OnAdded.Invoke();
                    break;
                case 0f:
                    OnRemoved.Invoke();
                    break;
            }
        } 
    }
    [SerializeField, Range(0, 1)]
    float _amount;
    public UnityEvent OnAdded, OnRemoved;
    public Material material;
}
