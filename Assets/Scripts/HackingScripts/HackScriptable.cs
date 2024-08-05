using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewHack", menuName = "ScriptableObjects/HackScriptable", order = 1)]
public class HackScriptable : ScriptableObject
{
    [SerializeField] private string hackName;
    [SerializeField] private string hackDescription;
    [SerializeField] private Sprite hackIcon;

    [SerializeField] private List<HackParam> hackParams = new List<HackParam>();

    public string HackName { get { return hackName; } }
    public string HackDescription { get { return hackDescription; } }

    public Sprite HackIcon { get { return hackIcon; } }

    public List<HackParam> HackParams { get { return hackParams; } }

    public HackFunctionality function;

}




