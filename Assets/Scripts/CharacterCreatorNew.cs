using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;

public enum CharacterTabStates { Overview, Policework, Interests, Interpersonal, Off }

public class CharacterCreatorNew : MonoBehaviour
{
    #region STATVARIABLES
    /*Stat variables*/

    // Policework Stats //
    bool pStat1;        // true = gut feeling , false = strategic
    bool pStat2;        // true = improvisor , false = knowledge 
    bool pStat3;        // true = easily distracted , false = perceptive
    bool aligned1;
    int pStatCount = 0;
    int pStatTotal = 0;
    int looseUnit;
    int byTheBook;

    // Interpersonal Stats // 
    bool iStat1;        // true = friendly , false = cold
    bool iStat2;        // true = trusting , false = intimidating
    bool iStat3;        // true = empathetic , false = contrarian
    bool aligned2;
    int iStatCount = 0;
    int iStatTotal = 0;
    int goodCop;
    int badCop;

    // Interests Stats // 
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
        Lua.RegisterFunction(nameof(GutStrats), this, SymbolExtensions.GetMethodInfo(() => GutStrats(false)));
        Lua.RegisterFunction(nameof(ImprovKnow), this, SymbolExtensions.GetMethodInfo(() => ImprovKnow(false)));
        Lua.RegisterFunction(nameof(Distraceptive), this, SymbolExtensions.GetMethodInfo(() => Distraceptive(false)));

        Lua.RegisterFunction(nameof(FriendCold), this, SymbolExtensions.GetMethodInfo(() => FriendCold(false)));
        Lua.RegisterFunction(nameof(TrustIntimidating), this, SymbolExtensions.GetMethodInfo(() => TrustIntimidating(false)));
        Lua.RegisterFunction(nameof(EmpathContra), this, SymbolExtensions.GetMethodInfo(() => EmpathContra(false)));

        Lua.RegisterFunction(nameof(MethodInteg), this, SymbolExtensions.GetMethodInfo(() => MethodInteg(false)));
        Lua.RegisterFunction(nameof(ShredSlend), this, SymbolExtensions.GetMethodInfo(() => ShredSlend(false)));
        Lua.RegisterFunction(nameof(WrenchCoded), this, SymbolExtensions.GetMethodInfo(() => WrenchCoded(false)));

        Lua.RegisterFunction(nameof(SetCharStateLua), this, SymbolExtensions.GetMethodInfo(() => SetCharStateLua(0)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(GutStrats));
        Lua.UnregisterFunction(nameof(ImprovKnow));
        Lua.UnregisterFunction(nameof(Distraceptive));

        Lua.UnregisterFunction(nameof(FriendCold));
        Lua.UnregisterFunction(nameof(TrustIntimidating));
        Lua.UnregisterFunction(nameof(EmpathContra));

        Lua.UnregisterFunction(nameof(MethodInteg));
        Lua.UnregisterFunction(nameof(ShredSlend));
        Lua.UnregisterFunction(nameof(WrenchCoded));

        Lua.UnregisterFunction(nameof(SetCharStateLua));
    }

    /// <summary>Calls SetCharacterSheetState method from Lua</summary>
    /// <param name="stateDouble"></param>
    public void SetCharStateLua(double stateDouble)
    {
        int stateInt = (int)stateDouble;
        SetCharacterSheetState((CharacterTabStates)stateInt);
    }

    public void SetCharacterSheetState(int state)
    {
        SetCharacterSheetState((CharacterTabStates)state);
    }

    /// <summary>Sets CharacterTabState</summary>
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
        statButtons[7].GetComponent<Button>().interactable = iStat1;

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

        if (aligned2 == true)
        {
            if (goodCop >= 2)
            {
                alignmentIcons[4].SetActive(true);
                alignmentIcons[5].SetActive(false);
                alignmentIcons[7].SetActive(true);
                alignmentIcons[6].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.goodCopBadCop", true);
            }
            else if (badCop >= 2)
            {
                alignmentIcons[6].SetActive(true);
                alignmentIcons[4].SetActive(false);
                alignmentIcons[5].SetActive(true);
                alignmentIcons[7].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.goodCopBadCop", false);
            }
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

    int StatToggleCount(bool stat1, bool stat2, bool stat3)
    {
        int count = 0;

        if (stat1 == true) count++;
        if (stat2 == true) count++;
        if (stat3 == true) count++;

        return count;
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
            traitDIcons[7].SetActive(!iStat1);
            traitVIcons[7].SetActive(iStat1);

            traitDOverview[6].SetActive(iStat1);
            traitVOverview[6].SetActive(iStat1);
            traitDOverview[7].SetActive(!iStat1);
            traitVOverview[7].SetActive(!iStat1);
        }
        else
        {
            traitDIcons[6].SetActive(false);
            traitVIcons[6].SetActive(false);
            traitDIcons[7].SetActive(false);
            traitVIcons[7].SetActive(false);

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


    //public void SetStatOverviewState(double overviewState)
    //{
    //    int overviewInt = (int)overviewState;

    //    switch(overviewInt)
    //    {
    //        case 0:

    //            break;
    //    }
    //}

}
