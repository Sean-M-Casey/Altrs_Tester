using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class ExternalDialogueLogger : MonoBehaviour
{
    [SerializeField] private GameObject conversationLogObject;
    [SerializeField] private LogHolder logHolderPrefab;

    [SerializeField] private int countBeforeIncrement;
    [SerializeField] private float sizeIncrement;
    private float baseHeight;

    LayoutElement layoutElement;

    private List<LogContainer> fullLogContainers = new List<LogContainer>();
    private List<LogContainer> currentLogContainers = new List<LogContainer>();

    private List<LogHolder> fullLogHolders = new List<LogHolder>();
    private List<LogHolder> currentLogHolders = new List<LogHolder>();

    

    public void OnConversationStart(Transform actor)
    {
        layoutElement = conversationLogObject.GetComponent<LayoutElement>();
        baseHeight = layoutElement.preferredHeight;
        Debug.Log(string.Format("Starting conversation with {0}", actor.name));
    }

    public void OnConversationLine(Subtitle subtitle)
    {
        if(!string.IsNullOrEmpty(subtitle.formattedText.text))
        {
            LogContainer logContainer = new LogContainer(subtitle.speakerInfo.Name, subtitle.speakerInfo.portrait, subtitle.formattedText.text);

            UpdateLogsOnNewLine(logContainer);
        }

        
    }

    public void OnConversationEnd(Transform actor)
    {
        Debug.Log(string.Format("Ending conversation with {0}", actor.name));
    }

    void UpdateLogsOnNewLine(LogContainer logContainer)
    {
        fullLogContainers.Add(logContainer);

        currentLogContainers.Add(logContainer);
        
        LogHolder logHolder = Instantiate<LogHolder>(logHolderPrefab, conversationLogObject.transform);
        logHolder.InitialiseLog(logContainer);

        currentLogHolders.Add(logHolder);

        if(currentLogHolders.Count >= countBeforeIncrement)
        {
            layoutElement.preferredHeight += sizeIncrement;
        }

    }

    void RemoveLog(bool fromMainList, int indexToRemove)
    {
        if(fromMainList)
        {
            fullLogHolders[indexToRemove].DeleteSelf();
            fullLogHolders.RemoveAt(indexToRemove);
            fullLogContainers.RemoveAt(indexToRemove);
        }
        else
        {
            currentLogHolders[indexToRemove].DeleteSelf();
            currentLogHolders.RemoveAt(indexToRemove);
            currentLogContainers.RemoveAt(indexToRemove);
        }
    }
}

[System.Serializable]
public struct LogContainer
{
    public string speakerName;

    public Sprite speakerImage;

    public string formattedText;

    public LogContainer(string speakerName, Sprite speakerImage, string formattedText)
    {
        this.speakerName = speakerName;
        this.speakerImage = speakerImage;
        this.formattedText = formattedText;
    }
}
