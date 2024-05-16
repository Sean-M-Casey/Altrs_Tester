using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actorNameDisplay;
    [SerializeField] TextMeshProUGUI conversantNameDisplay;

    [SerializeField] RawImage actorImageDisplay;
    [SerializeField] RawImage conversantImageDisplay;

    [SerializeField] TextMeshProUGUI rollGroupDisplay;
    [SerializeField] TextMeshProUGUI rollTypeDisplay;

    [SerializeField] TextMeshProUGUI rollValueDisplay;
    [SerializeField] TextMeshProUGUI evaluationDisplay;

    public string ActorName { get { return actorNameDisplay.text; } set { actorNameDisplay.text = value; } }
    public string ConversantName { get { return conversantNameDisplay.text; } set { conversantNameDisplay.text = value; } }

    public string RollGroup { get {  return rollGroupDisplay.text; } set {  rollGroupDisplay.text = value; } }
    public string RollType { get { return rollTypeDisplay.text; } set { rollTypeDisplay.text = value; } }

    public string RollValue { get { return rollValueDisplay.text; } set { rollValueDisplay.text = value; } }
    public string Evaluation { get {  return evaluationDisplay.text; } set {  evaluationDisplay.text = value; } }
}
