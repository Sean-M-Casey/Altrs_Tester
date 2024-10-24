using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewHack", menuName = "ScriptableObjects/BaseHackScriptable", order = 1)]
public class HackScriptable : ScriptableObject
{
    [SerializeField] private string hackName;
    [SerializeField] private string hackDescription;
    [SerializeField] private Sprite hackIcon;
    [SerializeField] private HackTypes hackType;
    [SerializeField] private List<HackableTypes> compatibleHackables = new List<HackableTypes>();

    [SerializeField] private List<HackParam> hackParams = new List<HackParam>();

    public string HackName { get { return hackName; } }
    public string HackDescription { get { return hackDescription; } }

    public Sprite HackIcon { get { return hackIcon; } }

    public HackTypes HackType { get { return hackType; } }
    public List<HackableTypes> CompatibleHackables { get { return compatibleHackables; } }

    public List<HackParam> HackParams { get { return hackParams; } }

    //public HackFunctionality function;

    public bool IsHackCompatible(IHackable target)
    {
        foreach(HackableTypes type in compatibleHackables)
        {
            if(type == target.HackableType) return true;
        }

        return false;
    }

    public virtual void StartExecute(IHackable target)
    {
        Debug.Log($"HACK: Hack of {hackName} is executing on target of {target.TargetAsGameObject.name}");
    }

    public virtual void StartExecute(IHackable target, HackParam hackParam)
    {
        Debug.Log($"HACK: Hack of {hackName} is executing on target of {target.TargetAsGameObject.name}, with a parameter of {hackParam.ValueAsString}");
    }

}




