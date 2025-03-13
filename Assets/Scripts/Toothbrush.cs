using System.Linq;
using TMPro;
using UnityEngine;

public class Toothbrush : Tool
{
    protected override void Affect(Part target)
    {
        base.Affect(target);

        Debug.Log("Where we brushing boys");
        
        
        // OMNI TOOL
        target.Afflictions.ToList().ForEach(_ =>
        {
            var pre = _.Amount;

            // cleaning effect
            
            if (pre > 0 && _.Amount == 0)
            {
                // completed, spawn finish effect
            }
        });
        
        // todo: when we got all the tools bring this back
         if (target.Afflictions.FirstOrDefault(_ => _.Type == this.intendedType) is { } aff)
         {
            aff.Amount -= Input.mousePositionDelta.magnitude / 5 * Time.deltaTime;
         }
    }
}
