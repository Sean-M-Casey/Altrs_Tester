using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>HackScriptable is the base class for all Hack Objects. It derives from ScriptableObject.</summary>
public abstract class HackScriptable : ScriptableObject
{
    /// <summary>Target that the Hack acts upon.</summary>
    protected HackableObject target;

    /// <summary>The name of the hack to be displayed on the button text.</summary>
    [SerializeField] protected string hackName;

    [SerializeField] protected string hackDescription;

    public string HackName { get { return hackName; } }
    public string HackDescription { get { return hackDescription; } }

    /// <summary>Assigns HackableObject as Hack target.</summary>
    /// <param name="target">Hackable to set as target.</param>
    public virtual void SetTarget(HackableObject target)
    {
        this.target = target;
    }

    /// <summary>Executes HackFunction.</summary>
    public virtual void Execute()
    {
        HackFunction();
    }

    /// <summary>Holds Hack's functionality.</summary>
    public virtual void HackFunction()
    {

    }
}

/// <summary>HackScriptable<T0> is a variant of the base HackScriptable which accepts 1 parameter of variable type.</summary>
public abstract class HackScriptable<T0> : HackScriptable
{
    [SerializeField] protected T0 param1 = default(T0);

    public override void Execute()
    {
        HackFunction(param1);
    }

    /// <summary>Can be used to change Hack params via script.</summary>
    /// <param name="arg1">Value to set param to.</param>
    public virtual void SetParams(T0 arg1)
    {
        param1 = arg1;
    }

    /// <summary>Holds Hack's functionality. Accepts one parameter.</summary>
    /// <param name="arg1">Param to pass into function.</param>
    protected abstract void HackFunction(T0 arg1);
}

/// <summary>HackScriptable<T0, T1> is a variant of the base HackScriptable which accepts 2 parameters of variable type.</summary>
public abstract class HackScriptable<T0, T1> : HackScriptable
{
    [SerializeField] protected T0 param1 = default(T0);
    [SerializeField] protected T1 param2 = default(T1);

    public override void Execute()
    {
        HackFunction(param1, param2);
    }

    /// <summary>Can be used to change Hack params via script.</summary>
    /// <param name="arg1">Value to set first param to.</param>
    /// <param name="arg2">Value to set second param to.</param>
    public virtual void SetParams(T0 arg1, T1 arg2)
    {
        param1 = arg1;
        param2 = arg2;
    }

    /// <summary>Holds Hack's functionality. Accepts two parameters.</summary>
    /// <param name="arg1">First Param to pass into function.</param>
    /// <param name="arg2">Second Param to pass into function.</param>
    protected abstract void HackFunction(T0 arg1, T1 arg2);
}
