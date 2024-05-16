using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;

public enum CharacterTabStates { Overview, Policework, Interests, Interpersonal, Off }

#region STATENUMS

public enum Alignment_Policework { LooseUnit = -1, Unassigned = 0, ByTheBook = 1 }
public enum Stat_Approach { GutFeeling = -1, Unassigned = 0, Strategic = 1 }
public enum Stat_Procedure { Improvisor = -1, Unassigned = 0, Knowledge = 1 }
public enum Stat_Attentiveness { EasilyDistracted = -1, Unassigned = 0, Perceptive = 1 }

public enum Alignment_Interpersonal { GoodCop = -1, Unassigned = 0, BadCop = 1 }
public enum Stat_Attitude { Friendly = -1, Unassigned = 0, Cold = 1 }
public enum Stat_Impression { Trusting = -1, Unassigned = 0, Intimidating = 1 }
public enum Stat_Understanding { Empathetic = -1, Unassigned = 0, Contrarian = 1 }

public enum Alignment_Interests { MeatHead = -1, Unassigned = 0, HackerMan = 1 }
public enum Stat_Technology { MethodMan = -1, Unassigned = 0, Integrated = 1 }
public enum Stat_Fitness { Shredded = -1, Unassigned = 0, Slender = 1 }
public enum Stat_Expertise { WrenchMonkey = -1, Unassigned = 0, Coded = 1, NotApplicable = 2 }

#endregion

public class CharacterCreatorNew : MonoBehaviour
{
    #region STATVARIABLES
    /*Stat variables*/

    // Policework Stats //
    Alignment_Policework policework = Alignment_Policework.Unassigned;
    Stat_Approach approach = Stat_Approach.Unassigned;
    Stat_Procedure procedure = Stat_Procedure.Unassigned;
    Stat_Attentiveness attentiveness = Stat_Attentiveness.Unassigned;

    bool pStat1;        // true = gut feeling , false = strategic
    bool pStat2;        // true = improvisor , false = knowledge 
    bool pStat3;        // true = easily distracted , false = perceptive
    bool aligned1;
    int pStatCount = 0;
    int pStatTotal = 0;
    int looseUnit;
    int byTheBook;

    // Interpersonal Stats // 
    Alignment_Interpersonal interpersonal = Alignment_Interpersonal.Unassigned;
    Stat_Attitude attitude = Stat_Attitude.Unassigned;
    Stat_Impression impression = Stat_Impression.Unassigned;
    Stat_Understanding understanding = Stat_Understanding.Unassigned;

    bool iStat1;        // true = friendly , false = cold
    bool iStat2;        // true = trusting , false = intimidating
    bool iStat3;        // true = empathetic , false = contrarian
    bool aligned2;
    int iStatCount = 0;
    int iStatTotal = 0;
    int goodCop;
    int badCop;

    // Interests Stats // 
    Alignment_Interests interests = Alignment_Interests.Unassigned;
    Stat_Technology technology = Stat_Technology.Unassigned;
    Stat_Fitness fitness = Stat_Fitness.Unassigned;
    Stat_Expertise expertise = Stat_Expertise.Unassigned;

    bool intStat1;      // true = method man , false = integrated 
    bool intStat2;      // true = shredded , false = slender
    bool intStat3;      // true = wrench monkey , false = coded
    bool aligned3;
    int intStatCount = 0;
    int intStatTotal = 0;
    int meatHead;
    int hackerMan;
    #endregion

    #region OVERVIEWVARIABLES
    /*Stat Showcase Variables*/
    CharacterTabStates currentState = CharacterTabStates.Off;
    [Header("Overview Canvas Objects")]
    [SerializeField] GameObject overviewTitle;
    [SerializeField] GameObject policeTitle;
    [SerializeField] GameObject personalTitle;
    [SerializeField] GameObject interestTitle;
    [Space(10)]
    [SerializeField] GameObject overviewObj;
    [SerializeField] GameObject policeObj;
    [SerializeField] GameObject personalObj;
    [SerializeField] GameObject interestObj;

    [SerializeField] bool[] statToggle;

    public GameObject[] statButtons;
    [SerializeField] GameObject[] alignmentIcons;
    [SerializeField] GameObject[] traitDIcons;
    [SerializeField] GameObject[] traitVIcons;
    [SerializeField] GameObject[] traitDOverview;
    [SerializeField] GameObject[] traitVOverview;
    [SerializeField] MMFeedbacks[] mmFeedbacks;

    [SerializeField] Button[] nextButtons;
    [SerializeField] Button[] backButtons;
    #endregion

    private void Awake()
    {
        SetCharacterSheetState(CharacterTabStates.Off);
    }

    private void OnEnable()
    {
        Lua.RegisterFunction(nameof(SetCharStateLua), this, SymbolExtensions.GetMethodInfo(() => SetCharStateLua(0)));
        Lua.RegisterFunction(nameof(SetPlayerStat), this, SymbolExtensions.GetMethodInfo(() => SetPlayerStat(string.Empty, 0)));


    }

    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(SetCharStateLua));
        Lua.UnregisterFunction(nameof(SetPlayerStat));
    }

    /// <summary>Calls SetCharacterSheetState method from Lua</summary>
    /// <param name="stateDouble"></param>
    public void SetCharStateLua(double stateDouble)
    {
        int stateInt = (int)stateDouble;
        SetCharacterSheetState((CharacterTabStates)stateInt);
    }

    /// <summary>Sets CharacterTabStates as Int</summary>
    /// <param name="state"></param>
    public void SetCharacterSheetState(int state)
    {
        SetCharacterSheetState((CharacterTabStates)state);
    }

    /// <summary>Sets CharacterTabStates as Enum</summary>
    /// <param name="state"></param>
    public void SetCharacterSheetState(CharacterTabStates state)
    {
        currentState = state;

        switch(currentState)
        {
            case CharacterTabStates.Off:

                overviewTitle.SetActive(false);
                overviewObj.SetActive(false);

                policeTitle.SetActive(false);
                policeObj.SetActive(false);

                interestTitle.SetActive(false);
                interestObj.SetActive(false);

                personalTitle.SetActive(false);
                personalObj.SetActive(false);

                nextButtons[0].gameObject.SetActive(false);
                nextButtons[0].interactable = false;
                nextButtons[1].gameObject.SetActive(false);
                nextButtons[1].interactable = false;
                nextButtons[2].gameObject.SetActive(false);
                nextButtons[2].interactable = false;
                nextButtons[3].gameObject.SetActive(false);
                nextButtons[3].interactable = false;
                nextButtons[4].gameObject.SetActive(false);
                nextButtons[4].interactable = false;

                backButtons[0].gameObject.SetActive(false);
                backButtons[0].interactable = false;
                backButtons[1].gameObject.SetActive(false);
                backButtons[1].interactable = false;
                backButtons[2].gameObject.SetActive(false);
                backButtons[2].interactable = false;
                backButtons[3].gameObject.SetActive(false);
                backButtons[3].interactable = false;
                break;
            case CharacterTabStates.Overview:

                overviewTitle.SetActive(true);
                overviewObj.SetActive(true);

                policeTitle.SetActive(false);
                policeObj.SetActive(false);

                interestTitle.SetActive(false);
                interestObj.SetActive(false);

                personalTitle.SetActive(false);
                personalObj.SetActive(false);

                nextButtons[0].gameObject.SetActive(true);
                nextButtons[0].interactable = true;
                nextButtons[1].gameObject.SetActive(false);
                nextButtons[1].interactable = false;
                nextButtons[2].gameObject.SetActive(false);
                nextButtons[2].interactable = false;
                nextButtons[3].gameObject.SetActive(false);
                nextButtons[3].interactable = false;
                nextButtons[4].gameObject.SetActive(true);
                nextButtons[4].interactable = true;

                backButtons[0].gameObject.SetActive(false);
                backButtons[0].interactable = false;
                backButtons[1].gameObject.SetActive(false);
                backButtons[1].interactable = false;
                backButtons[2].gameObject.SetActive(true);
                backButtons[2].interactable = true;
                backButtons[3].gameObject.SetActive(false);
                backButtons[3].interactable = false;
                break;
            case CharacterTabStates.Policework:

                overviewTitle.SetActive(false);
                overviewObj.SetActive(false);

                policeTitle.SetActive(true);
                policeObj.SetActive(true);

                interestTitle.SetActive(false);
                interestObj.SetActive(false);

                personalTitle.SetActive(false);
                personalObj.SetActive(false);

                nextButtons[0].gameObject.SetActive(false);
                nextButtons[0].interactable = false;
                nextButtons[1].gameObject.SetActive(true);
                nextButtons[1].interactable = true;
                nextButtons[2].gameObject.SetActive(false);
                nextButtons[2].interactable = false;
                nextButtons[3].gameObject.SetActive(false);
                nextButtons[3].interactable = false;
                nextButtons[4].gameObject.SetActive(false);
                nextButtons[4].interactable = false;

                backButtons[0].gameObject.SetActive(false);
                backButtons[0].interactable = false;
                backButtons[1].gameObject.SetActive(false);
                backButtons[1].interactable = false;
                backButtons[2].gameObject.SetActive(false);
                backButtons[2].interactable = false;
                backButtons[3].gameObject.SetActive(true);
                backButtons[3].interactable = true;
                break;
            case CharacterTabStates.Interests:

                overviewTitle.SetActive(false);
                overviewObj.SetActive(false);

                policeTitle.SetActive(false);
                policeObj.SetActive(false);

                interestTitle.SetActive(true);
                interestObj.SetActive(true);

                personalTitle.SetActive(false);
                personalObj.SetActive(false);

                nextButtons[0].gameObject.SetActive(false);
                nextButtons[0].interactable = false;
                nextButtons[1].gameObject.SetActive(false);
                nextButtons[1].interactable = false;
                nextButtons[2].gameObject.SetActive(false);
                nextButtons[2].interactable = false;
                nextButtons[3].gameObject.SetActive(true);
                nextButtons[3].interactable = true;
                nextButtons[4].gameObject.SetActive(false);
                nextButtons[4].interactable = false;

                backButtons[0].gameObject.SetActive(false);
                backButtons[0].interactable = false;
                backButtons[1].gameObject.SetActive(true);
                backButtons[1].interactable = true;
                backButtons[2].gameObject.SetActive(false);
                backButtons[2].interactable = false;
                backButtons[3].gameObject.SetActive(false);
                backButtons[3].interactable = false;
                break;
            case CharacterTabStates.Interpersonal:

                overviewTitle.SetActive(false);
                overviewObj.SetActive(false);

                policeTitle.SetActive(false);
                policeObj.SetActive(false);

                interestTitle.SetActive(false);
                interestObj.SetActive(false);

                personalTitle.SetActive(true);
                personalObj.SetActive(true);

                nextButtons[0].gameObject.SetActive(false);
                nextButtons[0].interactable = false;
                nextButtons[1].gameObject.SetActive(false);
                nextButtons[1].interactable = false;
                nextButtons[2].gameObject.SetActive(true);
                nextButtons[2].interactable = true;
                nextButtons[3].gameObject.SetActive(false);
                nextButtons[3].interactable = false;
                nextButtons[4].gameObject.SetActive(false);
                nextButtons[4].interactable = false;

                backButtons[0].gameObject.SetActive(true);
                backButtons[0].interactable = true;
                backButtons[1].gameObject.SetActive(false);
                backButtons[1].interactable = false;
                backButtons[2].gameObject.SetActive(false);
                backButtons[2].interactable = false;
                backButtons[3].gameObject.SetActive(false);
                backButtons[3].interactable = false;
                break;
        }
    }

    public void SetPlayerStat(string stateName, double stateVal)
    {
        Debug.Log($"SETSTAT: Given stat name: {stateName}. State value is: {stateVal}");

        int stateValInt = (int)stateVal;

        switch(stateName)
        {
            case "Approach":

                SetPlayerStatEnum((Stat_Approach)stateValInt);

                break;

            case "Procedure":

                SetPlayerStatEnum((Stat_Procedure)stateValInt);

                break;

            case "Attentiveness":

                SetPlayerStatEnum((Stat_Attentiveness)stateValInt);

                break;

            case "Attitude":

                SetPlayerStatEnum((Stat_Attitude)stateValInt);

                break;

            case "Impression":

                SetPlayerStatEnum((Stat_Impression)stateValInt);

                break;

            case "Understanding":

                SetPlayerStatEnum((Stat_Understanding)stateValInt);

                break;

            case "Technology":

                SetPlayerStatEnum((Stat_Technology)stateValInt);

                break;

            case "Fitness":

                SetPlayerStatEnum((Stat_Fitness)stateValInt);

                break;

            case "Expertise":

                SetPlayerStatEnum((Stat_Expertise)stateValInt);

                break;

            default:

                Debug.Log($"SETSTAT: Could not find stat of name: {stateName}.");

                break;
        }
        
    }

    #region PlayerStatEnumOverloads

    public void SetPlayerStatEnum(Stat_Approach stat)
    {
        Debug.Log($"SETSTAT: Setting Approach Stat to {stat}.");

        approach = stat;

        Debug.Log($"SETSTAT: Approach Stat is set to {approach}.");

        switch (approach)
        {
            case Stat_Approach.GutFeeling:

                DialogueLua.SetActorField("Dayholt", "Approach", -1);
                DialogueLua.SetActorField("Vaughn", "Approach", 1);

                statButtons[0].GetComponent<Button>().interactable = false;
                statButtons[1].GetComponent<Button>().interactable = true;

                statToggle[0] = true;

                break;

            case Stat_Approach.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Approach", 0);
                DialogueLua.SetActorField("Vaughn", "Approach", 0);

                statButtons[0].GetComponent<Button>().interactable = true;
                statButtons[1].GetComponent<Button>().interactable = true;

                statToggle[0] = false;

                break;

            case Stat_Approach.Strategic:

                DialogueLua.SetActorField("Dayholt", "Approach", 1);
                DialogueLua.SetActorField("Vaughn", "Approach", -1);

                statButtons[0].GetComponent<Button>().interactable = true;
                statButtons[1].GetComponent<Button>().interactable = false;

                statToggle[0] = true;

                break;
        }

        SetAlignment(ref policework);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Procedure stat)
    {
        procedure = stat;

        switch (procedure)
        {
            case Stat_Procedure.Improvisor:

                DialogueLua.SetActorField("Dayholt", "Procedure", -1);
                DialogueLua.SetActorField("Vaughn", "Procedure", 1);

                statButtons[2].GetComponent<Button>().interactable = false;
                statButtons[3].GetComponent<Button>().interactable = true;

                statToggle[1] = true;

                break;

            case Stat_Procedure.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Procedure", 0);
                DialogueLua.SetActorField("Vaughn", "Procedure", 0);

                statButtons[2].GetComponent<Button>().interactable = true;
                statButtons[3].GetComponent<Button>().interactable = true;

                statToggle[1] = false;

                break;

            case Stat_Procedure.Knowledge:

                DialogueLua.SetActorField("Dayholt", "Procedure", 1);
                DialogueLua.SetActorField("Vaughn", "Procedure", -1);

                statButtons[2].GetComponent<Button>().interactable = true;
                statButtons[3].GetComponent<Button>().interactable = false;

                statToggle[1] = true;

                break;
        }

        SetAlignment(ref policework);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Attentiveness stat)
    {
        attentiveness = stat;

        switch (attentiveness)
        {
            case Stat_Attentiveness.EasilyDistracted:

                DialogueLua.SetActorField("Dayholt", "Attentiveness", -1);
                DialogueLua.SetActorField("Vaughn", "Attentiveness", 1);

                statButtons[4].GetComponent<Button>().interactable = false;
                statButtons[5].GetComponent<Button>().interactable = true;

                statToggle[2] = true;

                break;

            case Stat_Attentiveness.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Attentiveness", 0);
                DialogueLua.SetActorField("Vaughn", "Attentiveness", 0);

                statButtons[4].GetComponent<Button>().interactable = true;
                statButtons[5].GetComponent<Button>().interactable = true;

                statToggle[2] = false;

                break;

            case Stat_Attentiveness.Perceptive:

                DialogueLua.SetActorField("Dayholt", "Attentiveness", 1);
                DialogueLua.SetActorField("Vaughn", "Attentiveness", -1);

                statButtons[4].GetComponent<Button>().interactable = true;
                statButtons[5].GetComponent<Button>().interactable = false;

                statToggle[2] = true;

                break;
        }

        SetAlignment(ref policework);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Attitude stat)
    {
        attitude = stat;

        switch (attitude)
        {
            case Stat_Attitude.Friendly:

                DialogueLua.SetActorField("Dayholt", "Attitude", -1);
                DialogueLua.SetActorField("Vaughn", "Attitude", 1);

                statButtons[6].GetComponent<Button>().interactable = false;
                statButtons[15].GetComponent<Button>().interactable = true;

                statToggle[3] = true;

                break;

            case Stat_Attitude.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Attitude", 0);
                DialogueLua.SetActorField("Vaughn", "Attitude", 0);

                statButtons[6].GetComponent<Button>().interactable = true;
                statButtons[15].GetComponent<Button>().interactable = true;

                statToggle[3] = false;

                break;

            case Stat_Attitude.Cold:

                DialogueLua.SetActorField("Dayholt", "Attitude", 1);
                DialogueLua.SetActorField("Vaughn", "Attitude", -1);

                statButtons[6].GetComponent<Button>().interactable = true;
                statButtons[15].GetComponent<Button>().interactable = false;

                statToggle[3] = true;

                break;
        }

        SetAlignment(ref interpersonal);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Impression stat)
    {
        impression = stat;

        switch (impression)
        {
            case Stat_Impression.Trusting:

                DialogueLua.SetActorField("Dayholt", "Impression", -1);
                DialogueLua.SetActorField("Vaughn", "Impression", 1);

                statButtons[8].GetComponent<Button>().interactable = false;
                statButtons[9].GetComponent<Button>().interactable = true;

                statToggle[4] = true;

                break;

            case Stat_Impression.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Impression", 0);
                DialogueLua.SetActorField("Vaughn", "Impression", 0);

                statButtons[8].GetComponent<Button>().interactable = true;
                statButtons[9].GetComponent<Button>().interactable = true;

                statToggle[4] = false;

                break;

            case Stat_Impression.Intimidating:

                DialogueLua.SetActorField("Dayholt", "Impression", 1);
                DialogueLua.SetActorField("Vaughn", "Impression", -1);

                statButtons[8].GetComponent<Button>().interactable = true;
                statButtons[9].GetComponent<Button>().interactable = false;

                statToggle[4] = true;

                break;
        }

        SetAlignment(ref interpersonal);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Understanding stat)
    {
        understanding = stat;

        switch (understanding)
        {
            case Stat_Understanding.Empathetic:

                DialogueLua.SetActorField("Dayholt", "Understanding", -1);
                DialogueLua.SetActorField("Vaughn", "Understanding", 1);

                statButtons[10].GetComponent<Button>().interactable = false;
                statButtons[11].GetComponent<Button>().interactable = true;

                statToggle[5] = true;

                break;

            case Stat_Understanding.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Understanding", 0);
                DialogueLua.SetActorField("Vaughn", "Understanding", 0);

                statButtons[10].GetComponent<Button>().interactable = true;
                statButtons[11].GetComponent<Button>().interactable = true;

                statToggle[5] = false;

                break;

            case Stat_Understanding.Contrarian:

                DialogueLua.SetActorField("Dayholt", "Understanding", 1);
                DialogueLua.SetActorField("Vaughn", "Understanding", -1);

                statButtons[10].GetComponent<Button>().interactable = true;
                statButtons[11].GetComponent<Button>().interactable = false;

                statToggle[5] = true;

                break;
        }

        SetAlignment(ref interpersonal);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Technology stat)
    {
        technology = stat;

        switch (technology)
        {
            case Stat_Technology.MethodMan:

                DialogueLua.SetActorField("Dayholt", "Technology", -1);
                DialogueLua.SetActorField("Vaughn", "Technology", 1);

                statButtons[12].GetComponent<Button>().interactable = false;
                statButtons[13].GetComponent<Button>().interactable = true;

                statToggle[6] = true;

                break;

            case Stat_Technology.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Technology", 0);
                DialogueLua.SetActorField("Vaughn", "Technology", 0);

                statButtons[12].GetComponent<Button>().interactable = true;
                statButtons[13].GetComponent<Button>().interactable = true;

                statToggle[6] = false;

                break;

            case Stat_Technology.Integrated:

                DialogueLua.SetActorField("Dayholt", "Technology", 1);
                DialogueLua.SetActorField("Vaughn", "Technology", -1);

                statButtons[12].GetComponent<Button>().interactable = true;
                statButtons[13].GetComponent<Button>().interactable = false;

                statToggle[6] = true;

                break;
        }

        SetAlignment(ref interests);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Fitness stat)
    {
        fitness = stat;

        switch (fitness)
        {
            case Stat_Fitness.Shredded:

                DialogueLua.SetActorField("Dayholt", "Fitness", -1);
                DialogueLua.SetActorField("Vaughn", "Fitness", 1);

                statButtons[14].GetComponent<Button>().interactable = false;
                statButtons[15].GetComponent<Button>().interactable = true;

                statToggle[7] = true;

                break;

            case Stat_Fitness.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Fitness", 0);
                DialogueLua.SetActorField("Vaughn", "Fitness", 0);

                statButtons[14].GetComponent<Button>().interactable = true;
                statButtons[15].GetComponent<Button>().interactable = true;

                statToggle[7] = false;

                break;

            case Stat_Fitness.Slender:

                DialogueLua.SetActorField("Dayholt", "Fitness", 1);
                DialogueLua.SetActorField("Vaughn", "Fitness", -1);

                statButtons[14].GetComponent<Button>().interactable = true;
                statButtons[15].GetComponent<Button>().interactable = false;

                statToggle[7] = true;

                break;
        }

        SetAlignment(ref interests);
        NewOverviewSetter();
    }

    public void SetPlayerStatEnum(Stat_Expertise stat)
    {
        expertise = stat;

        switch (expertise)
        {
            case Stat_Expertise.WrenchMonkey:

                DialogueLua.SetActorField("Dayholt", "Expertise", -1);
                DialogueLua.SetActorField("Vaughn", "Expertise", 2);

                statButtons[16].GetComponent<Button>().interactable = false;
                statButtons[17].GetComponent<Button>().interactable = true;

                statToggle[8] = true;

                break;

            case Stat_Expertise.Unassigned:

                DialogueLua.SetActorField("Dayholt", "Expertise", 0);
                DialogueLua.SetActorField("Vaughn", "Expertise", 0);

                statButtons[16].GetComponent<Button>().interactable = true;
                statButtons[17].GetComponent<Button>().interactable = true;

                statToggle[8] = false;

                break;

            case Stat_Expertise.Coded:

                DialogueLua.SetActorField("Dayholt", "Expertise", 1);
                DialogueLua.SetActorField("Vaughn", "Expertise", 2);

                statButtons[16].GetComponent<Button>().interactable = true;
                statButtons[17].GetComponent<Button>().interactable = false;

                statToggle[8] = true;

                break;
        }

        SetAlignment(ref interests);
        NewOverviewSetter();
    }

    #endregion

    #region PlayerStatButtonMethods

    public void ApproachStat(int state)
    {
        SetPlayerStatEnum((Stat_Approach)state);
    }
    public void ProcedureStat(int state)
    {
        SetPlayerStatEnum((Stat_Procedure)state);
    }
    public void AttentivenessStat(int state)
    {
        SetPlayerStatEnum((Stat_Attentiveness)state);
    }
    public void AttitudeStat(int state)
    {
        SetPlayerStatEnum((Stat_Attitude)state);
    }
    public void ImpressionStat(int state)
    {
        SetPlayerStatEnum((Stat_Impression)state);
    }
    public void UnderstandingStat(int state)
    {
        SetPlayerStatEnum((Stat_Understanding)state);
    }
    public void TechnologyStat(int state)
    {
        SetPlayerStatEnum((Stat_Technology)state);
    }
    public void FitnessStat(int state)
    {
        SetPlayerStatEnum((Stat_Fitness)state);
    }
    public void ExpertiseStat(int state)
    {
        SetPlayerStatEnum((Stat_Expertise)state);
    }

    #endregion


    public void GutStrats(bool value)
    {
        pStat1 = value;

        statButtons[0].GetComponent<Button>().interactable = !pStat1;
        statButtons[1].GetComponent<Button>().interactable = pStat1;

        statToggle[0] = true;

        pStatCount = StatToggleCount(statToggle[0], statToggle[1], statToggle[2]);

        if (pStat1 == true)
        {
            if (looseUnit < 3)
            {
                looseUnit++;
            }
            if (byTheBook > 0)
            {
                if ((byTheBook - 1) + looseUnit == pStatCount)
                    byTheBook--;
            }
        }
        else
        {
            if (byTheBook < 3)
            {
                byTheBook++;
            }
            if (looseUnit > 0)
            {
                if ((looseUnit - 1) + byTheBook == pStatCount)
                    looseUnit--;
            }
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2) aligned1 = true;
        else aligned1 = false;

        OverviewSetter();
        AlignmentAssignment();

        Debug.Log("PStat 1 is " + pStat1);
    }

    public void ImprovKnow(bool value)
    {
        pStat2 = value;

        statButtons[2].GetComponent<Button>().interactable = !pStat2;
        statButtons[3].GetComponent<Button>().interactable = pStat2;

        statToggle[1] = true;

        pStatCount = StatToggleCount(statToggle[0], statToggle[1], statToggle[2]);

        if (pStat2 == true)
        {
            if (looseUnit < 3)
            {
                looseUnit++;
            }
            if (byTheBook > 0)
            {
                if ((byTheBook - 1) + looseUnit == pStatCount)
                    byTheBook--;
            }
        }
        else
        {
            if (byTheBook < 3)
            {
                byTheBook++;
            }
            if (looseUnit > 0)
            {
                if ((looseUnit - 1) + byTheBook == pStatCount)
                    looseUnit--;
            }
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2) aligned1 = true;
        else aligned1 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void Distraceptive(bool value)
    {
        pStat3 = value;

        statButtons[4].GetComponent<Button>().interactable = !pStat3;
        statButtons[5].GetComponent<Button>().interactable = pStat3;

        statToggle[2] = true;

        pStatCount = StatToggleCount(statToggle[0], statToggle[1], statToggle[2]);

        if (pStat3 == true)
        {
            if (looseUnit < 3)
            {
                looseUnit++;
            }
            if (byTheBook > 0)
            {
                if ((byTheBook - 1) + looseUnit == pStatCount)
                    byTheBook--;
            }
        }
        else
        {
            if (byTheBook < 3)
            {
                byTheBook++;
            }
            if (looseUnit > 0)
            {
                if ((looseUnit - 1) + byTheBook == pStatCount)
                    looseUnit--;
            }
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2) aligned1 = true;
        else aligned1 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void FriendCold(bool value)
    {
        iStat1 = value;

        statButtons[6].GetComponent<Button>().interactable = !iStat1;
        statButtons[15].GetComponent<Button>().interactable = iStat1;

        statToggle[3] = true;

        iStatCount = StatToggleCount(statToggle[3], statToggle[4], statToggle[5]);

        if (iStat1 == true)
        {
            if (goodCop < 3)
            {
                goodCop++;
            }
            if (badCop > 0)
            {
                if ((badCop - 1) + goodCop == iStatCount)
                    badCop--;
            }
        }
        else
        {
            if (badCop < 3)
            {
                badCop++;
            }
            if (goodCop > 0)
            {
                if ((goodCop - 1) + badCop == iStatCount)
                    goodCop--;
            }
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2) aligned2 = true;
        else aligned2 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void TrustIntimidating(bool value)
    {
        iStat2 = value;

        statButtons[8].GetComponent<Button>().interactable = !iStat2;
        statButtons[9].GetComponent<Button>().interactable = iStat2;

        statToggle[4] = true;

        iStatCount = StatToggleCount(statToggle[3], statToggle[4], statToggle[5]);

        if (iStat2 == true)
        {
            if (goodCop < 3)
            {
                goodCop++;
            }
            if (badCop > 0)
            {
                if ((badCop - 1) + goodCop == iStatCount)
                    badCop--;
            }
        }
        else
        {
            if (badCop < 3)
            {
                badCop++;
            }
            if (goodCop > 0)
            {
                if ((goodCop - 1) + badCop == iStatCount)
                    goodCop--;
            }
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2) aligned2 = true;
        else aligned2 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void EmpathContra(bool value)
    {
        iStat3 = value;

        statButtons[10].GetComponent<Button>().interactable = !iStat3;
        statButtons[11].GetComponent<Button>().interactable = iStat3;

        statToggle[5] = true;

        iStatCount = StatToggleCount(statToggle[3], statToggle[4], statToggle[5]);

        if (iStat3 == true)
        {
            if (goodCop < 3)
            {
                goodCop++;
            }
            if (badCop > 0)
            {
                if ((badCop - 1) + goodCop == iStatCount)
                    badCop--;
            }
        }
        else
        {
            if (badCop < 3)
            {
                badCop++;
            }
            if (goodCop > 0)
            {
                if ((goodCop - 1) + badCop == iStatCount)
                    goodCop--;
            }
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2) aligned2 = true;
        else aligned2 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void MethodInteg(bool value)
    {
        intStat1 = value;

        statButtons[12].GetComponent<Button>().interactable = !intStat1;
        statButtons[13].GetComponent<Button>().interactable = intStat1;

        statToggle[6] = true;

        intStatCount = StatToggleCount(statToggle[6], statToggle[7], statToggle[8]);

        if (intStat1 == true)
        {
            if (meatHead < 3)
            {
                meatHead++;
            }
            if (hackerMan > 0)
            {
                if ((hackerMan - 1) + meatHead == intStatCount)
                    hackerMan--;
            }
        }
        else
        {
            if (hackerMan < 3)
            {
                hackerMan++;
            }
            if (meatHead > 0)
            {
                if ((meatHead - 1) + hackerMan == intStatCount)
                    meatHead--;
            }
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2) aligned3 = true;
        else aligned3 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void ShredSlend(bool value)
    {
        intStat2 = value;

        statButtons[14].GetComponent<Button>().interactable = !intStat2;
        statButtons[15].GetComponent<Button>().interactable = intStat2;

        statToggle[7] = true;

        intStatCount = StatToggleCount(statToggle[6], statToggle[7], statToggle[8]);

        if (intStat2 == true)
        {
            if (meatHead < 3)
            {
                meatHead++;
            }
            if (hackerMan > 0)
            {
                if ((hackerMan - 1) + meatHead == intStatCount)
                    hackerMan--;
            }
        }
        else
        {
            if (hackerMan < 3)
            {
                hackerMan++;
            }
            if (meatHead > 0)
            {
                if ((meatHead - 1) + hackerMan == intStatCount)
                    meatHead--;
            }
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2) aligned3 = true;
        else aligned3 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    public void WrenchCoded(bool value)
    {
        intStat3 = value;

        statButtons[16].GetComponent<Button>().interactable = !intStat3;
        statButtons[17].GetComponent<Button>().interactable = intStat3;

        statToggle[8] = true;

        intStatCount = StatToggleCount(statToggle[6], statToggle[7], statToggle[8]);

        if (intStat3 == true)
        {
            if (meatHead < 3)
            {
                meatHead++;
            }
            if (hackerMan > 0)
            {
                if ((hackerMan - 1) + meatHead == intStatCount)
                    hackerMan--;
            }
        }
        else
        {
            if (hackerMan < 3)
            {
                hackerMan++;
            }
            if (meatHead > 0)
            {
                if ((meatHead - 1) + hackerMan == intStatCount)
                    meatHead--;
            }
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2) aligned3 = true;
        else aligned3 = false;

        OverviewSetter();
        AlignmentAssignment();
    }

    void AlignmentAssignment()
    {
        switch((int)policework)
        {
            case -1:

                alignmentIcons[0].SetActive(true);
                alignmentIcons[1].SetActive(false);
                alignmentIcons[3].SetActive(true);
                alignmentIcons[2].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", -1);
                DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 1);

                break;

            case 0:

                alignmentIcons[0].SetActive(false);
                alignmentIcons[1].SetActive(false);
                alignmentIcons[3].SetActive(false);
                alignmentIcons[2].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 0);

                break;

            case 1:

                alignmentIcons[2].SetActive(true);
                alignmentIcons[0].SetActive(false);
                alignmentIcons[1].SetActive(true);
                alignmentIcons[3].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 1);
                DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", -1);

                break;
        }

        if (aligned1 == true)
        {
            if (looseUnit >= 2)
            {
                alignmentIcons[0].SetActive(true);
                alignmentIcons[1].SetActive(false);
                alignmentIcons[3].SetActive(true);
                alignmentIcons[2].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.looseUnitByTheBook", true);
            }
            else if (byTheBook >= 2)
            {
                alignmentIcons[2].SetActive(true);
                alignmentIcons[0].SetActive(false);
                alignmentIcons[1].SetActive(true);
                alignmentIcons[3].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.looseUnitByTheBook", false);
            }
        }

        switch ((int)interpersonal)
        {
            case -1:

                alignmentIcons[4].SetActive(true);
                alignmentIcons[5].SetActive(false);
                alignmentIcons[15].SetActive(true);
                alignmentIcons[6].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", -1);
                DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 1);

                break;

            case 0:

                alignmentIcons[4].SetActive(false);
                alignmentIcons[5].SetActive(false);
                alignmentIcons[6].SetActive(false);
                alignmentIcons[15].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 0);

                break;

            case 1:

                alignmentIcons[5].SetActive(true);
                alignmentIcons[4].SetActive(false);
                alignmentIcons[6].SetActive(true);
                alignmentIcons[15].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 1);
                DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", -1);

                break;
        }

        if (aligned2 == true)
        {
            if (goodCop >= 2)
            {
                alignmentIcons[4].SetActive(true);
                alignmentIcons[5].SetActive(false);
                alignmentIcons[15].SetActive(true);
                alignmentIcons[6].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.goodCopBadCop", true);
            }
            else if (badCop >= 2)
            {
                alignmentIcons[6].SetActive(true);
                alignmentIcons[4].SetActive(false);
                alignmentIcons[5].SetActive(true);
                alignmentIcons[15].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.goodCopBadCop", false);
            }
        }

        switch ((int)interests)
        {
            case -1:

                alignmentIcons[8].SetActive(true);
                alignmentIcons[9].SetActive(false);
                alignmentIcons[11].SetActive(true);
                alignmentIcons[10].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterestsAlignment", -1);
                DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 1);

                break;

            case 0:

                alignmentIcons[8].SetActive(false);
                alignmentIcons[9].SetActive(false);
                alignmentIcons[10].SetActive(false);
                alignmentIcons[11].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 0);

                break;

            case 1:

                alignmentIcons[9].SetActive(true);
                alignmentIcons[8].SetActive(false);
                alignmentIcons[10].SetActive(true);
                alignmentIcons[11].SetActive(false);

                DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 1);
                DialogueLua.SetActorField("Vaughn", "InterestsAlignment", -1);

                break;
        }

        if (aligned3 == true)
        {
            if (meatHead >= 2)
            {
                alignmentIcons[8].SetActive(true);
                alignmentIcons[9].SetActive(false);
                alignmentIcons[11].SetActive(true);
                alignmentIcons[10].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.meatHeadHackerMan", true);
            }
            else if (hackerMan >= 2)
            {
                alignmentIcons[9].SetActive(true);
                alignmentIcons[8].SetActive(false);
                alignmentIcons[10].SetActive(true);
                alignmentIcons[11].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.meadHeadHackerMan", false);
            }
        }
    }

    void SetAlignment(ref Alignment_Policework alignment)
    {
        int statTotalValue = (int)approach + (int)procedure + (int)attentiveness;

        int statToggleCount = StatToggleCount(statToggle[0], statToggle[1], statToggle[2]);

        Debug.Log($"SETSTAT: Police stat value is set to {statTotalValue}.");

        switch (statToggleCount)
        {
            case 0:

                Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}.");

                alignment = Alignment_Policework.Unassigned;

                DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 0);
                break;

            case 1:

                Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}.");

                alignment = Alignment_Policework.Unassigned;

                DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 0);
                break;

            case 2:

                Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}.");

                switch (statTotalValue)
                {
                    case -2:

                        Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}, and statTotalValue is set to {statTotalValue}.");

                        alignment = Alignment_Policework.LooseUnit;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 1);
                        break;

                    case 0:

                        Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}, and statTotalValue is set to {statTotalValue}.");

                        alignment = Alignment_Policework.Unassigned;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 0);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 0);
                        break;

                    case 2:

                        Debug.Log($"SETSTAT: Policework statToggleCount is {statToggleCount}, and statTotalValue is set to {statTotalValue}.");

                        alignment = Alignment_Policework.ByTheBook;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", -1);
                        break;
                }

                break;

            case 3:

                switch (statTotalValue)
                {
                    case -3: //All 3 stats LU

                        alignment = Alignment_Policework.LooseUnit;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 1);
                        break;

                    case -1: //2 LU, 1 BtB

                        alignment = Alignment_Policework.LooseUnit;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", 1);
                        break;

                    case 1:
                        alignment = Alignment_Policework.ByTheBook;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", -1);
                        break;

                    case 3:
                        alignment = Alignment_Policework.ByTheBook;

                        DialogueLua.SetActorField("Dayholt", "PoliceworkAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "PoliceworkAlignment", -1);
                        break;
                }

                break;
        }

        LogAlignment(alignment);
    }

    void SetAlignment(ref Alignment_Interpersonal alignment)
    {
        int statTotalValue = (int)attitude + (int)impression + (int)understanding;

        switch (StatToggleCount(statToggle[3], statToggle[4], statToggle[5]))
        {
            case 0:

                alignment = Alignment_Interpersonal.Unassigned;

                DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 0);
                break;

            case 1:

                alignment = Alignment_Interpersonal.Unassigned;

                DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 0);
                break;

            case 2:

                switch (statTotalValue)
                {
                    case -2:

                        alignment = Alignment_Interpersonal.GoodCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 1);
                        break;

                    case 0:
                        alignment = Alignment_Interpersonal.Unassigned;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 0);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 0);
                        break;

                    case 2:
                        alignment = Alignment_Interpersonal.BadCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", -1);
                        break;
                }

                break;

            case 3:

                switch (statTotalValue)
                {
                    case -3: //All 3 stats LU

                        alignment = Alignment_Interpersonal.GoodCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 1);
                        break;

                    case -1: //2 LU, 1 BtB

                        alignment = Alignment_Interpersonal.GoodCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", 1);
                        break;

                    case 1:
                        alignment = Alignment_Interpersonal.BadCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", -1);
                        break;

                    case 3:
                        alignment = Alignment_Interpersonal.BadCop;

                        DialogueLua.SetActorField("Dayholt", "InterpersonalAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterpersonalAlignment", -1);
                        break;
                }

                break;
        }

        LogAlignment(alignment);
    }

    void SetAlignment(ref Alignment_Interests alignment)
    {
        int statTotalValue = (int)technology + (int)fitness + (int)expertise;

        switch (StatToggleCount(statToggle[6], statToggle[7], statToggle[8]))
        {
            case 0:

                alignment = Alignment_Interests.Unassigned;

                DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 0);
                break;

            case 1:

                alignment = Alignment_Interests.Unassigned;

                DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 0);
                DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 0);
                break;

            case 2:

                switch (statTotalValue)
                {
                    case -2:

                        alignment = Alignment_Interests.MeatHead;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 1);
                        break;

                    case 0:
                        alignment = Alignment_Interests.Unassigned;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 0);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 0);
                        break;

                    case 2:
                        alignment = Alignment_Interests.HackerMan;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", -1);
                        break;
                }

                break;

            case 3:

                switch (statTotalValue)
                {
                    case -3: //All 3 stats LU

                        alignment = Alignment_Interests.MeatHead;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 1);
                        break;

                    case -1: //2 LU, 1 BtB

                        alignment = Alignment_Interests.MeatHead;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", -1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", 1);
                        break;

                    case 1:
                        alignment = Alignment_Interests.HackerMan;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", -1);
                        break;

                    case 3:
                        alignment = Alignment_Interests.HackerMan;

                        DialogueLua.SetActorField("Dayholt", "InterestsAlignment", 1);
                        DialogueLua.SetActorField("Vaughn", "InterestsAlignment", -1);
                        break;
                }

                break;
        }

        LogAlignment(alignment);
    }

    int StatToggleCount(bool stat1, bool stat2, bool stat3)
    {
        int count = 0;

        if (stat1 == true) count++;
        if (stat2 == true) count++;
        if (stat3 == true) count++;

        return count;
    }

    void LogAlignment(Alignment_Policework alignment)
    {
        Alignment_Policework alignmentDayholt = (Alignment_Policework)DialogueLua.GetActorField("Dayholt", "PoliceworkAlignment").asInt;
        Alignment_Policework alignmentVaughn = (Alignment_Policework)DialogueLua.GetActorField("Vaughn", "PoliceworkAlignment").asInt;

        Debug.Log($"SETSTAT: Dayholt's Policework is aligned to {alignmentDayholt}.\nVaughn's Policework is aligned to {alignmentVaughn}.");
    }

    void LogAlignment(Alignment_Interpersonal alignment)
    {
        Alignment_Interpersonal alignmentDayholt = (Alignment_Interpersonal)DialogueLua.GetActorField("Dayholt", "InterpersonalAlignment").asInt;
        Alignment_Interpersonal alignmentVaughn = (Alignment_Interpersonal)DialogueLua.GetActorField("Vaughn", "InterpersonalAlignment").asInt;

        Debug.Log($"SETSTAT: Dayholt's Interpersonal is aligned to {alignmentDayholt}.\nVaughn's Interpersonal is aligned to {alignmentVaughn}.");
    }

    void LogAlignment(Alignment_Interests alignment)
    {
        Alignment_Interests alignmentDayholt = (Alignment_Interests)DialogueLua.GetActorField("Dayholt", "InterestsAlignment").asInt;
        Alignment_Interests alignmentVaughn = (Alignment_Interests)DialogueLua.GetActorField("Vaughn", "InterestsAlignment").asInt;

        Debug.Log($"SETSTAT: Dayholt's Interests is aligned to {alignmentDayholt}.\nVaughn's Interests is aligned to {alignmentVaughn}.");
    }

    public void OverviewSetter()
    {
        if (statToggle[0] == true)
        {
            traitDIcons[0].SetActive(pStat1);
            traitVIcons[0].SetActive(!pStat1);
            traitDIcons[1].SetActive(!pStat1);
            traitVIcons[1].SetActive(pStat1);

            traitDOverview[0].SetActive(pStat1);
            traitVOverview[0].SetActive(pStat1);
            traitDOverview[1].SetActive(!pStat1);
            traitVOverview[1].SetActive(!pStat1);
        }
        else
        {
            traitDIcons[0].SetActive(false);
            traitVIcons[0].SetActive(false);
            traitDIcons[1].SetActive(false);
            traitVIcons[1].SetActive(false);

            traitDOverview[0].SetActive(false);
            traitVOverview[0].SetActive(false);
            traitDOverview[1].SetActive(false);
            traitVOverview[1].SetActive(false);
        }

        if (statToggle[1] == true)
        {
            traitDIcons[2].SetActive(pStat2);
            traitVIcons[2].SetActive(!pStat2);
            traitDIcons[3].SetActive(!pStat2);
            traitVIcons[3].SetActive(pStat2);

            traitDOverview[2].SetActive(pStat2);
            traitVOverview[2].SetActive(pStat2);
            traitDOverview[3].SetActive(!pStat2);
            traitVOverview[3].SetActive(!pStat2);
        }
        else
        {
            traitDIcons[2].SetActive(false);
            traitVIcons[2].SetActive(false);
            traitDIcons[3].SetActive(false);
            traitVIcons[3].SetActive(false);

            traitDOverview[2].SetActive(false);
            traitVOverview[2].SetActive(false);
            traitDOverview[3].SetActive(false);
            traitVOverview[3].SetActive(false);
        }

        if (statToggle[2] == true)
        {
            traitDIcons[4].SetActive(pStat3);
            traitVIcons[4].SetActive(!pStat3);
            traitDIcons[5].SetActive(!pStat3);
            traitVIcons[5].SetActive(pStat3);

            traitDOverview[4].SetActive(pStat3);
            traitVOverview[4].SetActive(pStat3);
            traitDOverview[5].SetActive(!pStat3);
            traitVOverview[5].SetActive(!pStat3);
        }
        else
        {
            traitDIcons[4].SetActive(false);
            traitVIcons[4].SetActive(false);
            traitDIcons[5].SetActive(false);
            traitVIcons[5].SetActive(false);

            traitDOverview[4].SetActive(false);
            traitVOverview[4].SetActive(false);
            traitDOverview[5].SetActive(false);
            traitVOverview[5].SetActive(false);
        }

        if (statToggle[3] == true)
        {
            traitDIcons[6].SetActive(iStat1);
            traitVIcons[6].SetActive(!iStat1);
            traitDIcons[15].SetActive(!iStat1);
            traitVIcons[15].SetActive(iStat1);

            traitDOverview[6].SetActive(iStat1);
            traitVOverview[6].SetActive(iStat1);
            traitDOverview[7].SetActive(!iStat1);
            traitVOverview[7].SetActive(!iStat1);
        }
        else
        {
            traitDIcons[6].SetActive(false);
            traitVIcons[6].SetActive(false);
            traitDIcons[15].SetActive(false);
            traitVIcons[15].SetActive(false);

            traitDOverview[6].SetActive(false);
            traitVOverview[6].SetActive(false);
            traitDOverview[7].SetActive(false);
            traitVOverview[7].SetActive(false);
        }

        if (statToggle[4] == true)
        {
            traitDIcons[8].SetActive(iStat2);
            traitVIcons[8].SetActive(!iStat2);
            traitDIcons[9].SetActive(!iStat2);
            traitVIcons[9].SetActive(iStat2);

            traitDOverview[8].SetActive(iStat2);
            traitVOverview[8].SetActive(iStat2);
            traitDOverview[9].SetActive(!iStat2);
            traitVOverview[9].SetActive(!iStat2);
        }
        else
        {
            traitDIcons[8].SetActive(false);
            traitVIcons[8].SetActive(false);
            traitDIcons[9].SetActive(false);
            traitVIcons[9].SetActive(false);

            traitDOverview[8].SetActive(false);
            traitVOverview[8].SetActive(false);
            traitDOverview[9].SetActive(false);
            traitVOverview[9].SetActive(false);
        }

        if (statToggle[5] == true)
        {
            traitDIcons[10].SetActive(iStat3);
            traitVIcons[10].SetActive(!iStat3);
            traitDIcons[11].SetActive(!iStat3);
            traitVIcons[11].SetActive(iStat3);

            traitDOverview[10].SetActive(iStat3);
            traitVOverview[10].SetActive(iStat3);
            traitDOverview[11].SetActive(!iStat3);
            traitVOverview[11].SetActive(!iStat3);
        }
        else
        {
            traitDIcons[10].SetActive(false);
            traitVIcons[10].SetActive(false);
            traitDIcons[11].SetActive(false);
            traitVIcons[11].SetActive(false);

            traitDOverview[10].SetActive(false);
            traitVOverview[10].SetActive(false);
            traitDOverview[11].SetActive(false);
            traitVOverview[11].SetActive(false);
        }

        if (statToggle[6] == true)
        {
            traitDIcons[12].SetActive(intStat1);
            traitVIcons[12].SetActive(!intStat1);
            traitDIcons[13].SetActive(!intStat1);
            traitVIcons[13].SetActive(intStat1);

            traitDOverview[12].SetActive(intStat1);
            traitVOverview[12].SetActive(intStat1);
            traitDOverview[13].SetActive(!intStat1);
            traitVOverview[13].SetActive(!intStat1);
        }
        else
        {
            traitDIcons[12].SetActive(false);
            traitVIcons[12].SetActive(false);
            traitDIcons[13].SetActive(false);
            traitVIcons[13].SetActive(false);

            traitDOverview[12].SetActive(false);
            traitVOverview[12].SetActive(false);
            traitDOverview[13].SetActive(false);
            traitVOverview[13].SetActive(false);
        }

        if (statToggle[7] == true)
        {
            traitDIcons[14].SetActive(intStat2);
            traitVIcons[14].SetActive(!intStat2);
            traitDIcons[15].SetActive(!intStat2);
            traitVIcons[15].SetActive(intStat2);

            traitDOverview[14].SetActive(intStat2);
            traitVOverview[14].SetActive(intStat2);
            traitDOverview[15].SetActive(!intStat2);
            traitVOverview[15].SetActive(!intStat2);
        }
        else
        {
            traitDIcons[14].SetActive(false);
            traitVIcons[14].SetActive(false);
            traitDIcons[15].SetActive(false);
            traitVIcons[15].SetActive(false);

            traitDOverview[14].SetActive(false);
            traitVOverview[14].SetActive(false);
            traitDOverview[15].SetActive(false);
            traitVOverview[15].SetActive(false);
        }

        if (statToggle[8] == true)
        {
            traitDIcons[16].SetActive(intStat3);
            traitDIcons[17].SetActive(!intStat3);

            traitDOverview[16].SetActive(intStat3);
            traitDOverview[17].SetActive(!intStat3);
        }
        else
        {
            traitDIcons[16].SetActive(false);
            traitDIcons[17].SetActive(false);

            traitDOverview[16].SetActive(false);
            traitDOverview[17].SetActive(false);
        }
    }

    public void NewOverviewSetter()
    {
        traitDIcons[0].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == -1);
        traitDIcons[1].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == 1);

        traitDIcons[2].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == -1);
        traitDIcons[3].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == 1);

        traitDIcons[4].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == -1);
        traitDIcons[5].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == 1);

        traitDIcons[6].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == -1);
        traitDIcons[7].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == 1);

        traitDIcons[8].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == -1);
        traitDIcons[9].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == 1);

        traitDIcons[10].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == -1);
        traitDIcons[11].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == 1);

        traitDIcons[12].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == -1);
        traitDIcons[13].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == 1);

        traitDIcons[14].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == -1);
        traitDIcons[15].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == 1);

        traitDIcons[16].SetActive(DialogueLua.GetActorField("Dayholt", "Expertise").AsInt == -1);
        traitDIcons[17].SetActive(DialogueLua.GetActorField("Dayholt", "Expertise").AsInt == 1);


        traitVIcons[0].SetActive(DialogueLua.GetActorField("Vaughn", "Approach").AsInt == -1);
        traitVIcons[1].SetActive(DialogueLua.GetActorField("Vaughn", "Approach").AsInt == 1);

        traitVIcons[2].SetActive(DialogueLua.GetActorField("Vaughn", "Procedure").AsInt == -1);
        traitVIcons[3].SetActive(DialogueLua.GetActorField("Vaughn", "Procedure").AsInt == 1);

        traitVIcons[4].SetActive(DialogueLua.GetActorField("Vaughn", "Attentiveness").AsInt == -1);
        traitVIcons[5].SetActive(DialogueLua.GetActorField("Vaughn", "Attentiveness").AsInt == 1);

        traitVIcons[6].SetActive(DialogueLua.GetActorField("Vaughn", "Attitude").AsInt == -1);
        traitVIcons[7].SetActive(DialogueLua.GetActorField("Vaughn", "Attitude").AsInt == 1);

        traitVIcons[8].SetActive(DialogueLua.GetActorField("Vaughn", "Impression").AsInt == -1);
        traitVIcons[9].SetActive(DialogueLua.GetActorField("Vaughn", "Impression").AsInt == 1);

        traitVIcons[10].SetActive(DialogueLua.GetActorField("Vaughn", "Understanding").AsInt == -1);
        traitVIcons[11].SetActive(DialogueLua.GetActorField("Vaughn", "Understanding").AsInt == 1);

        traitVIcons[12].SetActive(DialogueLua.GetActorField("Vaughn", "Technology").AsInt == -1);
        traitVIcons[13].SetActive(DialogueLua.GetActorField("Vaughn", "Technology").AsInt == 1);

        traitVIcons[14].SetActive(DialogueLua.GetActorField("Vaughn", "Fitness").AsInt == -1);
        traitVIcons[15].SetActive(DialogueLua.GetActorField("Vaughn", "Fitness").AsInt == 1);


        traitDOverview[0].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == -1);
        traitVOverview[0].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == -1);
        traitDOverview[1].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == 1);
        traitVOverview[1].SetActive(DialogueLua.GetActorField("Dayholt", "Approach").AsInt == 1);

        traitDOverview[2].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == -1);
        traitVOverview[2].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == -1);
        traitDOverview[3].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == 1);
        traitVOverview[3].SetActive(DialogueLua.GetActorField("Dayholt", "Procedure").AsInt == 1);

        traitDOverview[4].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == -1);
        traitVOverview[4].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == -1);
        traitDOverview[5].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == 1);
        traitVOverview[5].SetActive(DialogueLua.GetActorField("Dayholt", "Attentiveness").AsInt == 1);

        traitDOverview[6].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == -1);
        traitVOverview[6].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == -1);
        traitDOverview[7].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == 1);
        traitVOverview[7].SetActive(DialogueLua.GetActorField("Dayholt", "Attitude").AsInt == 1);

        traitDOverview[8].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == -1);
        traitVOverview[8].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == -1);
        traitDOverview[9].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == 1);
        traitVOverview[9].SetActive(DialogueLua.GetActorField("Dayholt", "Impression").AsInt == 1);

        traitDOverview[10].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == -1);
        traitVOverview[10].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == -1);
        traitDOverview[11].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == 1);
        traitVOverview[11].SetActive(DialogueLua.GetActorField("Dayholt", "Understanding").AsInt == 1);

        traitDOverview[12].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == -1);
        traitVOverview[12].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == -1);
        traitDOverview[13].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == 1);
        traitVOverview[13].SetActive(DialogueLua.GetActorField("Dayholt", "Technology").AsInt == 1);

        traitDOverview[14].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == -1);
        traitVOverview[14].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == -1);
        traitDOverview[15].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == 1);
        traitVOverview[15].SetActive(DialogueLua.GetActorField("Dayholt", "Fitness").AsInt == 1);

        traitDOverview[16].SetActive(DialogueLua.GetActorField("Dayholt", "Expertise").AsInt == -1);
        traitDOverview[17].SetActive(DialogueLua.GetActorField("Dayholt", "Expertise").AsInt == 1);
    }
    

}
