using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestHackFuncChooseNumber", menuName = "ScriptableObjects/ChooseNumber", order = 1)]
public class TestHackChooseNumber : HackScriptable
{
    public override void StartExecute(IHackable target, HackParam hackParam)
    {
        switch(hackParam.type)
        {
            case HackParamTypes.Int:
                Debug.Log($"TESTHACK: You chose {hackParam.ValueAsInt}");
                break;

            case HackParamTypes.Float:
                Debug.Log($"TESTHACK: You chose {hackParam.ValueAsFloat}");
                break;
        }
    }
}
