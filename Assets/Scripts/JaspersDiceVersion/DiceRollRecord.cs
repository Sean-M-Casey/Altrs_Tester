//Inspired by Darcy's Dice Manager script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Linq;
using Unity.VisualScripting;

public enum DiceType { D20, D12, D10, D8, D6, D4 }
public enum RollState { Unrolled, Rolling, CritPass, Pass, Fail, CritFail }

public class DiceRollRecord : MonoBehaviour
{
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

    //Example Command -- Dayholt|WRK|20\15|10|5

    private void Awake()
    {
        //Debug.Log("Awakewy");
        InitializeSpawnDirection();
    }

    private void OnEnable()
    {
        //Debug.Log("Enbaba");
        Lua.RegisterFunction(nameof(ParseDiceCommand), this, SymbolExtensions.GetMethodInfo(() => ParseDiceCommand(string.Empty)));
        //Debug.Log("EnbabaFin");
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(ParseDiceCommand));
    }

    void InitializeSpawnDirection()
    {
        foreach (GameObject t in spawnPoints) 
        {
            t.transform.LookAt(throwPoint);
        }
    }

    public void ParseDiceCommand(string command)
    {
        Debug.Log(command);
        #region Split Command
        //Split Command into "DiceRoll" and "DiceEval" parts, then splits them further to get params
        string[] splitCommand = command.Split('\\');

        string[] rollCMD = splitCommand[0].Split('|');
        string[] evalCMD = splitCommand[1].Split('|');
        #endregion

        #region Parse Command
        string contActorName = rollCMD[0];

        string rollType = string.Empty;
        List<Die> diceTypes = new List<Die>();

        switch (rollCMD[1])
        {
            case "WRK":
                switch (rollCMD[2])
                {
                    case "11":

                        rollType = "GutFeeling";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.gutFeelingStrategic").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "10":

                        rollType = "Strategic";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.gutFeelingStrategic").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "21":

                        rollType = "Improvisor";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.improvisorKnowledge").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "20":

                        rollType = "Knowledge";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.improvisorKnowledge").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "31":

                        rollType = "EasilyDistracted";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.distractPercept").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "30":

                        rollType = "Perceptive";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.distractPercept").asBool == false) diceTypes.Add(dieFour);

                        break;

                    default:

                        Debug.LogError("Failed to parse command at Stat Level; " +
                            "Remember, Stat is a two-digit number, determining the stat pair and stat side. " +
                            "Eg, \"21\" in the WRK group would refer to the second pair's \"true\" stat, \"Improvisor\".");

                        break;
                }

                break;

            case "PER":
                switch (rollCMD[2])
                {
                    case "11":

                        rollType = "Friendly";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.friendlyCold").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "10":

                        rollType = "Cold";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.friendlyCold").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "21":

                        rollType = "Trusting";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.trustingIntimidating").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "20":

                        rollType = "Intimidating";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.trustingIntimidating").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "31":

                        rollType = "Empathetic";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.empathContrarian").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "30":

                        rollType = "Contrarian";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.empathContrarian").asBool == false) diceTypes.Add(dieFour);

                        break;

                    default:

                        Debug.LogError("Failed to parse command at Stat Level; " +
                            "Remember, Stat is a two-digit number, determining the stat pair and stat side. " +
                            "Eg, \"11\" in the PER group would refer to the second pair's \"true\" stat, \"Friendly\".");

                        break;
                }

                break;

            case "INT":
                switch (rollCMD[2])
                {
                    case "11":
                        rollType = "Method Man";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.methodManIntegrated").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "10":
                        rollType = "Integrated";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.methodManIntegrated").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "21":
                        rollType = "Shredded";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.shreddedSlender").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "20":
                        rollType = "Slender";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.shreddedSlender").asBool == false) diceTypes.Add(dieFour);

                        break;

                    case "31":
                        rollType = "Wrench Monkey";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.wrenchMonkeyCoded").asBool == true) diceTypes.Add(dieFour);

                        break;

                    case "30":
                        rollType = "Coded";

                        diceTypes.Add(dieTwenty);

                        if (DialogueLua.GetVariable("PlayerStats.wrenchMonkeyCoded").asBool == false) diceTypes.Add(dieFour);

                        break;

                    default:
                        Debug.LogError("Failed to parse command at Stat Level; " +
                            "Remember, Stat is a two-digit number, determining the stat pair and stat side. " +
                            "Eg, \"30\" in the INT group would refer to the third pair's \"false\" stat, \"Coded\".");

                        break;
                }

                break;

            default:
                Debug.LogError("Failed to parse command at Group Level; Remember, Group is a three-letter code, determining which Stat Group to look in. The three accepted codes are \"WRK\" (Policework), \"PER\" (Interpersonal), \"INT\" (Interests).");

                break;
        }

        DiceEval diceEval = new DiceEval(int.Parse(evalCMD[0]), int.Parse(evalCMD[1]), int.Parse(evalCMD[2]));

        #endregion

        Debug.Log($"Creating DiceRoll, where {contActorName} is attempting a {rollType} roll, and needs to roll higher than {evalCMD[1]} to pass.");

        
        CreateDiceRoll(contActorName, rollType, diceTypes, diceEval);
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
            //Debug.Log("Added Point");
        }
    }

    public void ClearSpawnPoints()
    {
        selectedSpawnPoints.Clear();
    }

    
    public void CreateDiceRoll(string rollingActor, string rollType, Die[] dice, DiceEval diceEval)
    {
        //ClearSpawnPoints();

        DiceRoll roll = new DiceRoll(rollingActor, rollType, dice, diceEval);

        //foreach (Die d in roll.dice)
        //{
        //    SelectSpawnPoints();
        //}

    }

    public void CreateDiceRoll(string rollingActor, string rollType, List<Die> dice, DiceEval diceEval)
    {
        DiceRoll roll = new DiceRoll(rollingActor, rollType, dice.ToArray(), diceEval);
    }

    public void CreateDiceRoll(string rollingActor, string rollType, Die[] dice, DiceEval diceEval, float[] bonusesToRoll)
    {
        DiceRoll roll = new DiceRoll(rollingActor, rollType, dice, diceEval, bonusesToRoll);
    }

    public void CreateDiceRoll(string rollingActor, string rollType, List<Die> dice, DiceEval diceEval, float[] bonusesToRoll)
    {
        DiceRoll roll = new DiceRoll(rollingActor, rollType, dice.ToArray(), diceEval, bonusesToRoll);
    }

    

}


[System.Serializable]
public struct DiceEval
{
    public int critSuccess, pass, critFailure;

    /// <summary>
    /// Contains threshold values for evaluating DiceRoll.
    /// </summary>
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

    public string rollerName;

    public string rollType;

    public RollState rollState;

    public DiceEval diceEval;

    public float[] bonusesToRoll;

    public Die[] intendedDice;

    public List<Die> spawnedDice;

    public float finalRoll;

    #region DiceRoll Constructors
    
    public DiceRoll(string rollerName, string rollType, Die[] dice, DiceEval diceEval, float[] bonusesToRoll)
    {
        this.isReady = false;
        this.rollerName = rollerName;
        this.rollType = rollType;
        this.rollState = RollState.Unrolled;
        this.diceEval = diceEval;
        this.bonusesToRoll = bonusesToRoll;

        this.intendedDice = dice;
        this.spawnedDice = new List<Die>();

        this.finalRoll = 0;

        Debug.Log($"DICEROLL: RollType is {rollType}.");

    }

    public DiceRoll(string rollerName, string rollType, Die[] dice, DiceEval diceEval)
    {
        this.isReady = false;
        this.rollerName = rollerName;
        this.rollType = rollType;
        this.rollState = RollState.Unrolled;
        this.diceEval = diceEval;
        this.bonusesToRoll = new float[0];

        this.intendedDice = dice;
        this.spawnedDice = new List<Die>();

        this.finalRoll = 0;

        Debug.Log($"DICEROLL: RollType is {rollType}.");
    }
    #endregion

    public void FinalRollValue()
    {
        int rollTotal = 0;

        foreach(Die rolledDie in this.spawnedDice)
        {
            rollTotal += rolledDie.DiceRollOut;
        }

        if(this.bonusesToRoll.Length > 0)
        {
            foreach(float bonus in this.bonusesToRoll)
            {
                rollTotal += (int)bonus;
            }
        }

        this.finalRoll = rollTotal;
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
    

}
