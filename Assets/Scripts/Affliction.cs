using UnityEngine;

public class Affliction : MonoBehaviour
{
    Material material;
    [SerializeField, Range(0, 1)]
    float dirtyness = 1;
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.SetFloat("_Dirtyness", Mathf.Clamp(dirtyness, 0, 1));
    }
}
