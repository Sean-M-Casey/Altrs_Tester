using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HackFunctionality : ScriptableObject
{
    protected HackState state = HackState.Inactive;
    public void Execute(IHackable target)
    {
        HackingSystem.instance.StartCoroutine(ExecuteRoutine(target));
    }

    public void Execute(IHackable target, HackParam param)
    {
        HackingSystem.instance.StartCoroutine(ExecuteRoutine(target, param));
    }

    public abstract IEnumerator ExecuteRoutine(IHackable target);
    public abstract IEnumerator ExecuteRoutine(IHackable target, HackParam param);

    public void Resolve()
    {
        state = HackState.Inactive;
    }
}
