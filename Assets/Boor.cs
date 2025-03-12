using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Boor : Tool
{
    [SerializeField] ParticleSystem ps;

    Coroutine active;
    bool e
    {
        get => _e;
        set
        {
            if (value == _e)
                return;
            
            _e = value;
            
            if (value)
            {
                if (active != null)
                    StopCoroutine(active);
                active = StartCoroutine(wait());
                IEnumerator wait()
                {
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    e = false;
                }
            }
        }
    } bool _e
    {
        get => __e;
        set
        {
            if (value)
                ps.Play();
            else
                ps.Stop();
            __e = value;
        }
    } bool __e;

    void Start()
    {
        ps.Stop();
    }

    protected override void Affect(Part target)
    {
        base.Affect(target);

        Debug.Log("Where we drilling boys");

        FindAnyObjectByType<Part>(FindObjectsInactive.Include);
        
        // todo: when we got all the tools bring this back
        if (target.Afflictions.FirstOrDefault(_ => _.Type == this.intendedType) is { } aff)
        {
            aff.Amount -= 0.5f * Time.deltaTime;
            
            // particles
            e = true;
        }
    }
}