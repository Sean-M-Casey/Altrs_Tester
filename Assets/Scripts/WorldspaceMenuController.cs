using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldspaceMenuController : MonoBehaviour
{
    public enum MenuDirection { Up, Down, Left, Right }

    private IWorldspaceSelectable selected;

    public void SelectSelectable(IWorldspaceSelectable selectable)
    {
        if(selected != null) selected.WatchState(IWorldspaceSelectable.SelectableState.Interactable);

        selected = selectable;

        selected.WatchState(IWorldspaceSelectable.SelectableState.Selected);
    }



    public void MoveSelectable(MenuDirection direction)
    {
        switch(direction)
        {
            case MenuDirection.Up:

                break;

            case MenuDirection.Down:

                break;
        }
    }
}
