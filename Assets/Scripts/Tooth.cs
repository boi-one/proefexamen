using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Tooth : Part
{
    public bool clean;
    MeshRenderer meshRenderer;

    public void Awake()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        List<Material> toothAfflicions = new List<Material>();
        foreach (var affliction in Afflictions)
        {
            toothAfflicions.Add(meshRenderer.materials[0]);
            toothAfflicions.Add(affliction.material);
            int chance = affliction.Type == AfflictionType.hole ? 6 : 3;
            if (Mathf.FloorToInt(UnityEngine.Random.Range(1, chance)) == 1)
                affliction.Amount = UnityEngine.Random.Range(0.3f, 1.0f);
        }
        meshRenderer.SetMaterials(toothAfflicions);
    }

    void Update()
    {

        for (int i = 0; i < Afflictions.Length; i++)
        {
            meshRenderer.materials[i].SetFloat("_Dirtyness", Afflictions[i].Amount);
        }
    }

    void Clean(AfflictionType toolType, float decreaseAmount)
    {
        if (Afflictions.First(_ => _.Type == toolType) is { } affliction)
        {
            affliction.Amount -= decreaseAmount;
            if (affliction.Amount == 0) clean = true;
        }
    }
}

