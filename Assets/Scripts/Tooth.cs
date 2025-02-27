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
    // MeshRenderer meshRenderer;
    //
    // public void Awake()
    // {
    //     List<Material> toothAfflicions = new List<Material>();
    //     foreach (var affliction in Afflictions)
    //     {
    //         toothAfflicions.Add(affliction.material);
    //         affliction.Amount = UnityEngine.Random.Range(0.0f, 1.0f);
    //     }
    //     transform.GetChild(0).GetComponent<MeshRenderer>().SetMaterials(toothAfflicions);
    //     meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    // }
    //
    // void Update()
    // {
    //
    //     for (int i = 0; i < Afflictions.Length; i++)
    //     {
    //         meshRenderer.materials[i].SetFloat("_Dirtyness", Afflictions[i].Amount);
    //     }
    // }
    //
    // void Clean(AfflictionType toolType, float decreaseAmount)
    // {
    //     if (Afflictions.First(_ => _.Type == toolType) is { } affliction)
    //         affliction.Amount -= decreaseAmount;
    // }
}

