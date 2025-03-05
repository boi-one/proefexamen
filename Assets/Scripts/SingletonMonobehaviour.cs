using Unity.VisualScripting;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T reference
    {
        get
        {
            if(_reference == null)
                FindAnyObjectByType<SingletonMonobehaviour<T>>().Awake();
            return _reference;
        }
        set => _reference = value;
    } private static T _reference;

    bool awakeHappened = false;

    protected void Awake()
    {
        if (awakeHappened) return;
        awakeHappened = true;
        
        reference = FindAnyObjectByType<T>();
        if(typeof(T).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(_ => _.DeclaringType == typeof(T) && _.Name == "Awake") is {} found)
            found.Invoke(reference, new object[] { });
    }

}
