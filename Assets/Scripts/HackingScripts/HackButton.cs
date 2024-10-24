using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HackButton : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI hackNameTextMesh;
    [SerializeField] private TextMeshProUGUI hackParamTextMesh;
    [SerializeField] private Image image;
    [SerializeField] private GameObject subButtonPanel;

    [SerializeField] private HackScriptable scriptable;

    [SerializeField] private string defaultNameString = "UnnamedHack";
    private string paramTextString = "";

    [SerializeField] private Sprite defaultSprite = null;

    private bool hasSubButtons = false;

    private List<HackSubButton> hackSubButtons = new List<HackSubButton>();

    private delegate void HackExecNoParams(IHackable target);

    private delegate void HackExecOneParam(IHackable target, HackParam param);

    private HackExecNoParams hackExecNoParams;

    private HackExecOneParam hackExecOneParam;

    public Button Button { get { return button; } }

    public HackScriptable Scriptable { get { return scriptable; } }

    public bool OwnsSelected { get { return IsSelectedMine(); } }

    private bool IsSelectedMine()
    {
        bool mineSelected = false;

        if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == Button)
        {
            mineSelected = true;

            return mineSelected;
        }
        else
        {
            if(hackSubButtons.Count > 0)
            {
                foreach(HackSubButton subButton in hackSubButtons)
                {
                    if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == subButton.Button)
                    {
                        mineSelected = true;
                        return mineSelected;
                    }
                }
            }
        }

        return mineSelected;
    }

    public void ControlSubPanelState()
    {
        if(OwnsSelected == true && hasSubButtons == true)
        {
            subButtonPanel.SetActive(true);
        }
        else
        {
            subButtonPanel.SetActive(false);
        }
    }

    public void InitializeHackButton(HackScriptable inScriptable)
    {

        if(subButtonPanel != null) { subButtonPanel.SetActive(false); }

        if (hackParamTextMesh != null)
        {
            paramTextString = "";
            hackParamTextMesh.text = paramTextString;
        }

        scriptable = inScriptable;

        if(hackNameTextMesh != null) hackNameTextMesh.text = (string.IsNullOrEmpty(scriptable.HackName)) ? defaultNameString : scriptable.HackName;

        if(image != null) image.sprite = (scriptable.HackIcon != null) ? scriptable.HackIcon : defaultSprite;


        if(scriptable.HackParams.Count == 0)
        {
            hackExecNoParams += scriptable.StartExecute;

            if (button != null) button.onClick.AddListener(OnButtonClick);

            paramTextString = "";
        }
        else if(scriptable.HackParams.Count == 1)
        {
            hackExecOneParam += scriptable.StartExecute;

            if (button != null) button.onClick.AddListener(OnButtonClick);

            switch(scriptable.HackParams[0].type)
            {
                case HackParamTypes.String:

                    paramTextString = string.Format(HackingSystem.instance.ParamFormat, (scriptable.HackParams[0].name, scriptable.HackParams[0].ValueAsString));

                    break;

                case HackParamTypes.Int:

                    paramTextString = string.Format(HackingSystem.instance.ParamFormat, (scriptable.HackParams[0].name, scriptable.HackParams[0].ValueAsInt));

                    break;

                case HackParamTypes.Float:

                    paramTextString = string.Format(HackingSystem.instance.ParamFormat, (scriptable.HackParams[0].name, scriptable.HackParams[0].ValueAsFloat));

                    break;

                case HackParamTypes.Vector3:

                    paramTextString = string.Format(HackingSystem.instance.ParamFormat, (scriptable.HackParams[0].name, scriptable.HackParams[0].ValueAsVector3));

                    break;
            }
        }
        else if(scriptable.HackParams.Count >= 2)
        {
            CreateSubButtons();

            if (button != null) button.onClick.AddListener(OnButtonClickHasSubButtons);
        }
    }

    void OnButtonClick()
    {
        hackExecNoParams?.Invoke(HackingSystem.instance.CurrentTarget);

        hackExecOneParam?.Invoke(HackingSystem.instance.CurrentTarget, scriptable.HackParams[0]);
    }

    void OnButtonClickHasSubButtons()
    {
        hackSubButtons[0].Button.Select();

        HackingSystem.instance.QuickhackScreen.selectableChanged.Invoke();
    }

    public void ShowSubButtonPanel()
    {
        if(hasSubButtons == false)
        {
            Debug.LogError("This Hack Button has no Sub Buttons, SubButtonPanel cannot be used.");
            return;
        }

        subButtonPanel.SetActive(true);
    }

    public void HideSubButtonPanel()
    {
        if (hasSubButtons == false)
        {
            Debug.LogError("This Hack Button has no Sub Buttons, SubButtonPanel cannot be used.");
            return;
        }

        subButtonPanel.SetActive(false);
    }

    

    void CreateSubButtons()
    {
        foreach(HackParam param in scriptable.HackParams)
        {
            int index = scriptable.HackParams.IndexOf(param);

            HackSubButton hackSubButton = Instantiate(HackingSystem.instance.HackSubButtonPrefab, subButtonPanel.transform);

            hackSubButton.InitializeSubButton(this, index);

            hackSubButtons.Add(hackSubButton);
        }

        hasSubButtons = true;
    }

    public void DeleteSelf()
    {
        if(hackExecNoParams != null) hackExecNoParams -= scriptable.StartExecute;
        if (hackExecOneParam != null) hackExecOneParam -= scriptable.StartExecute;

        if(hackSubButtons.Count > 0)
        {
            foreach(HackSubButton subButton in hackSubButtons)
            {
                subButton.DeleteSelf();
            }

            hackSubButtons.Clear();
            hasSubButtons = false;
        }

        Destroy(this);
    }
}
