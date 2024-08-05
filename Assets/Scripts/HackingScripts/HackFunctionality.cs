using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HackFunctionality : MonoBehaviour
{
    protected HackState state = HackState.Inactive;
    public void Execute(IHackable target)
    {
        StartCoroutine(ExecuteRoutine(target));
    }

    public abstract IEnumerator ExecuteRoutine(IHackable target);

    public void Resolve()
    {
        state = HackState.Inactive;
    }
}
