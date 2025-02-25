using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tooth : Part
{
    public bool clean;

    public void Awake()
    {
        List<Material> toothAfflicions = new List<Material>();
        foreach (var affliction in Afflictions) toothAfflicions.Add(affliction.material);
        transform.GetChild(0).GetComponent<MeshRenderer>().SetMaterials(toothAfflicions);
    }

    private void Update()
    {
        for (int i = 0; i < Afflictions.Length; i++)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().materials[i].SetFloat("_Dirtyness", Afflictions[i].Amount);
        }
    }

    void Clean(AfflictionType toolType)
    {
        foreach (var affliction in Afflictions) if (affliction.Name == toolType) affliction.Amount -= 0.1f;
    }
}
