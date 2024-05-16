using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DisplayName", menuName = "ScriptableObjects/DisplayNameHack")]
public class DisplayNameHack : HackScriptable
{
    public override void HackFunction()
    {
        Debug.Log($"HACKOUTPUT: Hack with name of \"{hackName}\", was executed on target with name of \"{target.name}\". ");
    }
}
