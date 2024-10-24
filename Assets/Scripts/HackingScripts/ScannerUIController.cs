using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScanMode { InfoMode, HackingMode }
public class ScannerUIController : MonoBehaviour
{
    [SerializeField] private GameObject scannerInfoPanel;
    [SerializeField] private GameObject hackingPanel;

    [SerializeField] private ScanMode scanMode = ScanMode.InfoMode;



    public void SwitchScannerMode()
    {
        switch(scanMode)
        {
            case ScanMode.InfoMode:

                scanMode = ScanMode.HackingMode;

                break;

            case ScanMode.HackingMode:

                scanMode = ScanMode.InfoMode;

                break;
        }
    }

    private void AnimateModeChange()
    {
        
    }
}
