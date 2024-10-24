using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackSubButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI hackParamTextMesh;

    private HackButton parentButton;

    private int index;

    private delegate void HackExecSubButton(IHackable target, HackParam param);

    private HackExecSubButton hackExecSubButton;

    public Button Button { get { return button; } }

    void OnButtonClick()
    {
        hackExecSubButton?.Invoke(HackingSystem.instance.CurrentTarget, parentButton.Scriptable.HackParams[index]);
    }

    public void InitializeSubButton(HackButton inParentButton, int inIndex)
    {
        if(button != null) button.onClick.AddListener(OnButtonClick);

        parentButton = inParentButton;
        index = inIndex;

        hackExecSubButton += parentButton.Scriptable.StartExecute;


    }

    public void DeleteSelf()
    {
        if(hackExecSubButton != null) hackExecSubButton = null;

        Destroy(this);
    }

}
