using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConversationLogDisplay : MonoBehaviour
{
    
}


public enum LogEntryType { PlayerSpeech, NonPlayerSpeech }
public struct LogEntry
{
    public LogEntryType type;

    public string ownerName;
    public string dialogueText;
    public Image ownerImage;
}
