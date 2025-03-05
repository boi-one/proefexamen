using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T reference;

    void Awake()
    {
        reference = FindAnyObjectByType<T>();
        typeof(T).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(_ => _.DeclaringType == typeof(T) && _.Name == "Awake")
            .Invoke(reference, new object[] { });
    }
}


public class Tool : Singleton<Tool> 
{
    // WRONG!!!
    void Awake()
    {
    }
    
    
    public static Tool CurrentlySelectedTool
    {
        get => _currentlySelectedTool;
        set
        {
            if (_currentlySelectedTool == value)
                return;
            if (_currentlySelectedTool is not null)
                _currentlySelectedTool.IsSelected = false;
            if (value is not null)
                value.IsSelected = true;
        }
    } static Tool _currentlySelectedTool;

    bool IsSelected
    {
        get => _isSelected;
        set
        {
            // Make ourselves visible
            gameObject.SetActive(value);
            _isSelected = value;
        }
    } bool _isSelected;
    
    public Sprite UiIcon;

    public Transform interactPoint;

    [FormerlySerializedAs("afflictionType")] [FormerlySerializedAs("affliction")] public AfflictionType intendedType;

    void Update()
    {
        if (!IsSelected) 
            return;

        var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,0.1f));
        foreach (var hit in Physics.RaycastAll(screenToWorldPoint, (screenToWorldPoint - Camera.main.transform.position).normalized))
        {
            if (hit.transform == transform)
                continue;
                
            transform.position = hit.point + (transform.position - interactPoint.position);
            if (Input.GetKey(KeyCode.E))
                Use(hit.collider);
            
            break;
        }
    }

    /// <summary> Inheritors do usage effects </summary>
    protected virtual void Use(Collider whoLol)
    {
        if (whoLol.TryGetComponent<Part>(out var lol)) 
            Affect(lol);
    }
    
    protected virtual void Affect(Part target) { }
}