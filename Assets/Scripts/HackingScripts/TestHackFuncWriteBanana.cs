using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestHackFuncWriteBanana", menuName = "ScriptableObjects/CallTargetBanana", order = 1)]
public class TestHackFuncWriteBanana : HackScriptable
{
    void CallTargetBanana(IHackable target)
    {
        Debug.Log($"{target.TargetAsGameObject.name} is totes a banana");

    }

    public override void StartExecute(IHackable target)
    {
        CallTargetBanana(target);
    }

}
