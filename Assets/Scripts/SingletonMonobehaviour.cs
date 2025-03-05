using Unity.VisualScripting;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T reference;

    protected void Awake()
    {
        reference = FindAnyObjectByType<T>();
        typeof(T).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(_ => _.DeclaringType == typeof(T) && _.Name == "Awake")
            .Invoke(reference, new object[] { });
    }

}
