using System;
using NUnit.Framework.Constraints;
using UnityEngine;

// interface Interactable
// {
//     public Func<bool> unInteracted { get; }
// }

public class Interaction : MonoBehaviour
{
    public RaycastHit hit;
    
    void Update() => RaycastDown();

    void RaycastDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        
        var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new(0, 0, 0.1f));
        Physics.Raycast(screenToWorldPoint, (screenToWorldPoint - Camera.main.transform.position).normalized, out hit);
        CameraControl.pivotPoint = hit.transform?.GetComponentInChildren<Tooth>() ? hit.transform.Find("Pivot") : null;
        
        // if (hit.transform?.GetComponentInChildren<Interactable>() is { } a && !a.unInteracted())
        // {
        //     
        // }
    }
}
