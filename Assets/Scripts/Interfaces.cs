using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IHackable
{
    public HackableTypes HackableType { get; }

    public List<HackScriptable> Hacks { get; }

    public GameObject TargetAsGameObject { get; }
}

public interface IScannable
{
    float TimeToScan { get; }

    bool IsScannable { get; }

    bool HasBeenScanned { get; }

    bool IsCurrentScanTarget { get; }

    void AttemptScan();
    void OnScanSuccess();
    void OnScanCanceled();
    void OnScanEnded();
}


