using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorldspaceSelectable : MonoBehaviour, IWorldspaceSelectable
{

    [SerializeField] protected IWorldspaceSelectable.SelectableState currentState;

    [SerializeField] protected bool isSelected;
    [SerializeField] protected bool isInteractable;

    [SerializeField] protected TextMeshProUGUI textMesh;
    [SerializeField] protected Image baseImage;

    [SerializeField] protected Color interactableColour;
    [SerializeField] protected Color nonInteractableColour;
    [SerializeField] protected Color selectedColour;
    [SerializeField] protected Color activatedColour;

    [SerializeField] protected IWorldspaceSelectable nextSelectableUp;
    [SerializeField] protected IWorldspaceSelectable nextSelectableDown;
    [SerializeField] protected IWorldspaceSelectable nextSelectableLeft;
    [SerializeField] protected IWorldspaceSelectable nextSelectableRight;

    public IWorldspaceSelectable.SelectableState CurrentState { get { return currentState; } set { currentState = value; } }
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public bool IsSelected { get { return isSelected; } set { isSelected = value; } }
    public TextMeshProUGUI TextMesh { get { return textMesh; } set { textMesh = value; } }
    public Image BaseImage { get { return baseImage; } set { baseImage = value; } }
    public Color InteractableColour { get { return interactableColour; } set { interactableColour = value; } }
    public Color NonInteractableColour { get { return nonInteractableColour; } set { nonInteractableColour = value; } }
    public Color SelectedColour { get { return selectedColour; } set { selectedColour = value; } }
    public Color ActivatedColour { get { return activatedColour; } set { activatedColour = value; } }
    public IWorldspaceSelectable NextSelectableUp { get { return nextSelectableUp; } set { nextSelectableUp = value; } }
    public IWorldspaceSelectable NextSelectableDown { get { return nextSelectableDown; } set { nextSelectableDown = value; } }
    public IWorldspaceSelectable NextSelectableLeft { get { return nextSelectableLeft; } set { nextSelectableLeft = value; } }
    public IWorldspaceSelectable NextSelectableRight { get { return nextSelectableRight; } set { nextSelectableRight = value; } }



    void Awake()
    {

        FindSelectables();
    }

    public abstract void InvokeSelectable();

    public virtual void WatchState(IWorldspaceSelectable.SelectableState state)
    {
        if(state != CurrentState)
        {
            switch(state)
            {
                case IWorldspaceSelectable.SelectableState.Interactable:
                    BaseImage.color = InteractableColour;
                    CurrentState = state;
                    break;

                case IWorldspaceSelectable.SelectableState.NonInteractable:
                    BaseImage.color = NonInteractableColour;
                    CurrentState = state;
                    break;

                case IWorldspaceSelectable.SelectableState.Selected:
                    BaseImage.color = SelectedColour;
                    CurrentState = state;
                    break;

                case IWorldspaceSelectable.SelectableState.Activated:
                    BaseImage.color = ActivatedColour;
                    CurrentState = state;
                    break;
            }
        }
    }


    public void FindSelectables()
    {
        RaycastHit hit;

        GameObject parent = gameObject.transform.parent.gameObject;


        if(Physics.Raycast(gameObject.transform.position, parent.transform.up, out hit))
        {
            if(hit.collider.gameObject.GetComponent<WorldspaceSelectable>() is IWorldspaceSelectable)
            {
                NextSelectableUp = hit.collider.gameObject.GetComponent<WorldspaceSelectable>();
            }
        }
        if (Physics.Raycast(gameObject.transform.position, -parent.transform.up, out hit))
        {
            if (hit.collider.gameObject.GetComponent<WorldspaceSelectable>() is IWorldspaceSelectable)
            {
                NextSelectableDown = hit.collider.gameObject.GetComponent<WorldspaceSelectable>();
            }
        }
        if (Physics.Raycast(gameObject.transform.position, parent.transform.right, out hit))
        {
            if (hit.collider.gameObject.GetComponent<WorldspaceSelectable>() is IWorldspaceSelectable)
            {
                NextSelectableRight = hit.collider.gameObject.GetComponent<WorldspaceSelectable>();
            }
        }
        if (Physics.Raycast(gameObject.transform.position, -parent.transform.right, out hit))
        {
            if (hit.collider.gameObject.GetComponent<WorldspaceSelectable>() is IWorldspaceSelectable)
            {
                NextSelectableLeft = hit.collider.gameObject.GetComponent<WorldspaceSelectable>();
            }
        }
    }
}
