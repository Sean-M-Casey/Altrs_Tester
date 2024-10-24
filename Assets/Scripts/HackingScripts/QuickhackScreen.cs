using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickhackScreen : MonoBehaviour
{
    [SerializeField] private GameObject hackscreenContainer;

    private List<HackButton> currentHackButtons = new List<HackButton>();

    private bool isCreatingButtons = false;
    private bool isDestroyingButtons = false;

    public List<HackButton> HackButtons { get {  return currentHackButtons; } }

    public bool IsModifyingButtons { get { return (isCreatingButtons == true || isDestroyingButtons == true); } }

    private Selectable currentSelectable;

    public delegate void OnSelectableChanged();

    public OnSelectableChanged selectableChanged;

    public void CreateHackButtons(List<HackScriptable> hacks)
    {
        isCreatingButtons = true;

        foreach(HackScriptable hack in hacks)
        {
            HackButton button = Instantiate(HackingSystem.instance.HackButtonPrefab, hackscreenContainer.transform);

            button.InitializeHackButton(hack);

            selectableChanged += button.ControlSubPanelState;

            currentHackButtons.Add(button);
        }

        isCreatingButtons = false;

        HackButtons[0].Button.Select();
    }


    public void NavigateHacks(Vector3 direction)
    {
        currentSelectable = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        selectableChanged.Invoke();

        currentSelectable.FindSelectable(direction).Select();
    }

    public void HackExecute()
    {
        Debug.Log($"QUICKHICK: HackExecute Called");

        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        button.onClick.Invoke();
    }

    public void DestroyCurrentHackButtons()
    {
        isDestroyingButtons = true;

        foreach (HackButton button in currentHackButtons)
        {
            button.DeleteSelf();
        }

        currentHackButtons.Clear();

        isDestroyingButtons = false;
    }
}
