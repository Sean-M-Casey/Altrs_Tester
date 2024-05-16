using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IHackable
{
    List<HackScriptable> Hacks { get; }

    

    void OnHackingStart();

    void OnHackingFinish();
}

public interface IWorldspaceMenu
{
    IWorldspaceSelectable currentTarget { get; set; }

    void OnOpenMenu();

    void OnCloseMenu();

    void CycleMenuElements();

    void InvokeCurrentTarget();
}


public interface IWorldspaceSelectable
{
    public enum SelectableState { Interactable, NonInteractable, Selected, Activated }


    SelectableState CurrentState { get; set; }
    bool IsInteractable { get; set; }

    bool IsSelected { get; set; }

    TextMeshProUGUI TextMesh { get; set; }
    Image BaseImage { get; set; }

    Color InteractableColour { get; set; }
    Color NonInteractableColour { get; set; }
    Color SelectedColour { get; set; }
    Color ActivatedColour { get; set; }

    IWorldspaceSelectable NextSelectableUp { get; set; }
    IWorldspaceSelectable NextSelectableDown { get; set; }
    IWorldspaceSelectable NextSelectableLeft { get; set; }
    IWorldspaceSelectable NextSelectableRight { get; set; }

    void InvokeSelectable();

    void WatchState(SelectableState state);

    void FindSelectables();
    
}

