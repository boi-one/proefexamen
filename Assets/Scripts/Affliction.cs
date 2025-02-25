using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Affliction
{
    public string Name;
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
    } float _amount;
    public UnityEvent OnAdded, OnRemoved;
}
