using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI hackNameTextMesh;
    [SerializeField] private Image image;

    [SerializeField] private HackScriptable scriptable;

    [SerializeField] private string defaultNameString = "UnnamedHack";
    [SerializeField] private Sprite defaultSprite = null;


    public void InitializeHackButton(HackScriptable scriptable)
    {
        if(hackNameTextMesh != null) hackNameTextMesh.text = (string.IsNullOrEmpty(scriptable.HackName)) ? defaultNameString : scriptable.HackName;

        if(image != null) image.sprite = (scriptable.HackIcon != null) ? scriptable.HackIcon : defaultSprite;
    }
}
