//Inspired by Darcy's Dice Manager script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Linq;
using Unity.VisualScripting;

public enum DiceType { D20, D12, D10, D8, D6, D4 }

public enum RollType { Untype, Policework, Interpersonal, Interests }

public enum RollState { Unrolled, Rolling, CritPass, Pass, Fail, CritFail }

public class DiceRollRecord : MonoBehaviour
{
    public GameObject rollDeterminer;

    public GameObject rollScreen;
    public GameObject rollDisplayer;
    public RollDisplay rollDisplayPrefab;

    public List<DiceRoll> rolls = new List<DiceRoll>();

    public GameObject[] spawnPoints;
    public List<GameObject> selectedSpawnPoints = new List<GameObject>();
    public Transform throwPoint;
    

    [SerializeField] Die dieTwenty;
    [SerializeField] Die dieTwelve;
    [SerializeField] Die dieTen;
    [SerializeField] Die dieEight;
    [SerializeField] Die dieSix;
    [SerializeField] Die dieFour;

    public Die D20 { get { return dieTwenty; } }
    public Die D12 { get { return dieTwelve; } }
    public Die D10 { get { return dieTen; } }
    public Die D8 { get { return dieEight; } }
    public Die D6 { get { return dieSix; } }
    public Die D4 { get { return dieFour; } }


    private void Awake()
    {
        InitializeSpawnDirection();
    }

    /// <summary>Initializes facing direction of dice spawn points.</summary>
    void InitializeSpawnDirection()
    {
        foreach (GameObject t in spawnPoints) 
        {
            t.transform.LookAt(throwPoint);
        }
    }


    public void SelectSpawnPoints()
    {
        int point = Random.Range(0, spawnPoints.Length);

        if (selectedSpawnPoints.Contains(spawnPoints[point]))
        {
            SelectSpawnPoints();
            return;
        }
        else
        {
            selectedSpawnPoints.Add(spawnPoints[point]);
        }
    }

    public void ClearSpawnPoints()
    {
        selectedSpawnPoints.Clear();
    }

    public void AddDiceRoll(DiceRoll diceRoll)
    {
        rolls.Add(diceRoll);

        RollDisplay display = GameObject.Instantiate(rollDisplayPrefab, rollDisplayer.transform);

        display.ActorName = DialogueLua.GetActorField(diceRoll.rollerIndex, "Name").AsString;
        Debug.Log($"ROLLDISPLAY: {display.ActorName}");

        display.ConversantName = DialogueLua.GetActorField(diceRoll.conversantIndex, "Name").AsString;
        Debug.Log($"ROLLDISPLAY: {display.ConversantName}");

        display.RollGroup = diceRoll.rollTypeGroup.ToString();
        Debug.Log($"ROLLDISPLAY: {display.RollGroup}");

        display.RollType = diceRoll.rollType;
        Debug.Log($"ROLLDISPLAY: {display.RollType}");

        display.RollValue = diceRoll.finalRoll.ToString();
        Debug.Log($"ROLLDISPLAY: {display.RollValue}");

        display.Evaluation = diceRoll.rollState.ToString();
        Debug.Log($"ROLLDISPLAY: {display.Evaluation}");
    }

}


[System.Serializable]
public struct DiceEval
{
    public int critSuccess, pass, critFailure;

    /// <summary>Contains threshold values for evaluating DiceRoll.</summary>
    /// <param name="critSuccess">Roll is Critical Success if roll is equal or greater than this value.</param>
    /// <param name="pass">Roll is Success if roll is equal or greater than this value, and Fail if roll is less than this value.</param>
    /// <param name="critFailure">Roll is Critical Fail if roll is less than this value.</param>
    public DiceEval(int critSuccess, int pass, int critFailure)
    {
        this.critSuccess = critSuccess;
        this.pass = pass;
        this.critFailure = critFailure;
    }
}


[System.Serializable]
public struct DiceRoll
{
    public bool isReady;

    public string rollerIndex;

    public string conversantIndex;

    public string rollType;

    public RollType rollTypeGroup;

    public RollState rollState;

    public DiceEval diceEval;

    public Die[] intendedDice;

    public List<Die> spawnedDice;

    public float finalRoll;

    #region DiceRoll Constructors
    
    public DiceRoll(string rollerIndex, string conversantIndex, string rollType, RollType rollTypeGroup, Die[] dice, DiceEval diceEval)
    {
        this.isReady = false;
        this.rollerIndex = rollerIndex;
        this.conversantIndex = conversantIndex;
        this.rollType = rollType;
        this.rollTypeGroup = rollTypeGroup;
        this.rollState = RollState.Unrolled;
        this.diceEval = diceEval;

        this.intendedDice = dice;
        this.spawnedDice = new List<Die>();

        this.finalRoll = 0;

    }

    public DiceRoll(string rollerIndex, string rollType, RollType rollTypeGroup, Die[] dice, DiceEval diceEval)
    {
        this.isReady = false;
        this.rollerIndex = rollerIndex;
        this.conversantIndex = string.Empty;
        this.rollType = rollType;
        this.rollTypeGroup = rollTypeGroup;
        this.rollState = RollState.Unrolled;
        this.diceEval = diceEval;

        this.intendedDice = dice;
        this.spawnedDice = new List<Die>();

        this.finalRoll = 0;

    }

    #endregion

    public void FinalRollValue()
    {
        int rollTotal = 0;

        foreach(Die rolledDie in this.spawnedDice)
        {
            rollTotal += rolledDie.DiceRollOut;
        }

        this.finalRoll = rollTotal;
    }

    public void FinalRollDetermination(int roll)
    {
        this.finalRoll = roll;
    }

    public void EvaluateDiceRoll()
    {
        if(this.finalRoll >= this.diceEval.pass)
        {
            this.rollState = (this.finalRoll >= this.diceEval.critSuccess) ? RollState.CritPass : RollState.Pass;
        }
        else
        {
            this.rollState = (this.finalRoll >= this.diceEval.critFailure) ? RollState.Fail : RollState.CritFail;
        }
    }

    public void CleanRoll()
    {
        foreach(Die die in this.spawnedDice)
        {
            GameObject.Destroy(die.gameObject, 1f);
        }
        this.spawnedDice.Clear();
    }

    public void LogRollDetails()
    {
        if (this.rollTypeGroup == RollType.Policework) 
        { 
            Debug.Log($"DICEROLLLOG:\n "); 
        }
        else if (this.rollTypeGroup == RollType.Interpersonal)
        {
            Debug.Log($"DICEROLLLOG:\n Rolling Actor: {DialogueLua.GetActorField(rollerIndex, "Name").AsString} \n Contesting Actor: {DialogueLua.GetActorField(conversantIndex, "Name").AsString} \n Roll Type: {rollType} \n Roll State: {rollState} \n Rolled Value: {finalRoll} \n");
        }
        else if (this.rollTypeGroup == RollType.Interests)
        {
            Debug.Log($"DICEROLLLOG:\n ");
        }
    }
    

}
