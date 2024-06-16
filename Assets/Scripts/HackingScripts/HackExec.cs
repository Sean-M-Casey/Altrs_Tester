using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HackExec : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button hackButton;

    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private HackScriptable hack;

    [SerializeField] private GameObject hackExecPanel;
    

    public Button HackButton { get { return hackButton; } }
    public HackScriptable Hack { get { return hack; } }


    /// <summary>Initialises HackExec with Scriptable Hack.</summary>
    /// <param name="inHack">HackScriptable to initialise with.</param>
    public void InitialiseHack(HackScriptable inHack)
    {

        hack = inHack;

        textMesh.text = hack.HackName + "()";

        hackButton.onClick.AddListener(hack.Execute);

    }



    /// <summary>Removes listeners and destroys this hack object.</summary>
    public void DestroyHack()
    {
        hackButton.onClick.RemoveAllListeners();

        Destroy(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {

    }

}
