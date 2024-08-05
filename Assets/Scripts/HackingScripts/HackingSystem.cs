using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HackParamTypes { String, Int, Float, Vector3 }
public enum HackableTypes { GenComp, RepoAltr, LiveAltr, SecCamera }
public enum HackTypes { Quickhack, Deephack }
public enum HackState { Inactive, InProgress, Completed }
public enum HackResult { Success, Failure }

public class HackingSystem : MonoBehaviour
{
    public static HackingSystem instance;

    [SerializeField] private IHackable currentTarget;

    public IHackable CurrentTarget { get { return currentTarget; } }
}


