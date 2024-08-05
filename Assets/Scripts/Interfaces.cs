using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IHackable
{
    public HackableTypes HackableType { get; }

    public GameObject TargetAsGameObject { get; }
}



