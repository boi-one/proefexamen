using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tooth : Part
{
    public bool clean;
    MeshRenderer meshRenderer;

    public void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        List<Material> toothAfflicions = new List<Material>();
        toothAfflicions.Add(meshRenderer.materials[0]);
        foreach (var affliction in Afflictions)
        {
            toothAfflicions.Add(affliction.material);
            
            if (Random.Range(0f, 1f) < ScoreSystem.reference.difficultyMultiplier switch { 1 => 0.1f, 2 => 0.3f, 3 => 0.5f })
                affliction.Amount = UnityEngine.Random.Range(0.4f, 1f);
            else affliction.Amount = 0;
        }

        meshRenderer.SetMaterials(toothAfflicions);
    }

    void Update()
    {
        for (int i = 0; i < Afflictions.Length; i++)
        {
            meshRenderer.materials[i+1].SetFloat("_Dirtyness", Afflictions[i].Amount);
        }
    }
}