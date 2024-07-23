using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogHolder : MonoBehaviour
{
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private TextMeshProUGUI speakerNameTMP;
    [SerializeField] private TextMeshProUGUI subtitleTMP;

    private Sprite speakerPortraitSprite;
    private string speakerName;
    private string subtitle;

    public void InitialiseLog(LogContainer container)
    {
        speakerName = container.speakerName;
        subtitle = container.formattedText;

        speakerPortraitSprite = container.speakerImage;

        DisplayLogInformation();
    }

    void DisplayLogInformation()
    {
        speakerNameTMP.text = speakerName;
        subtitleTMP.text = subtitle;
        speakerPortrait.sprite = speakerPortraitSprite;
    }

    public void DeleteSelf()
    {
        Destroy(gameObject);
    }
}
