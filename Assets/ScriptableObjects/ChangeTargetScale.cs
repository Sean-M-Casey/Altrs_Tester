using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeTargetScale", menuName = "ScriptableObjects/ChangeTargetScaleHack")]
public class ChangeTargetScale : HackScriptable<Vector3>
{
    protected override void HackFunction(Vector3 arg1)
    {
        target.transform.localScale = arg1;
    }
}
