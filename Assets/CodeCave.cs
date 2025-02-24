using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CodeCave
{
    static TrackerClass Tracker => _tracker ??= new GameObject("tracker").AddComponent<TrackerClass>();
    static TrackerClass _tracker;
    class TrackerClass : MonoBehaviour
    {
        public Action OnUpdate = () => { };
        void Awake() => DontDestroyOnLoad(gameObject);
        void Update() => OnUpdate();
    }

    public static void AddToUpdate(this Action target)
    {
        Tracker.OnUpdate += target;
    }
    
    public static (T, T2)? GetLowest<T,T2>(this IEnumerable<T> target, Func<T,T2> func) where T2 : IComparable
    {
        (T, T2)? lowest = null;
        foreach (var elem in target)
        {
            if (lowest is not { } notnull)
                lowest = (elem, default);
            else
            {
                var a = func(elem);
                if (a.CompareTo(notnull.Item2) < 0)
                    lowest = (elem, a);
            }
        }
        return lowest;
    }
}