using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;

public class CharacterCreatorNew : MonoBehaviour
{
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


    /*Stat Showcase Variables*/
    [SerializeField] bool[] statToggle;

    public GameObject[] statButtons;
    [SerializeField] GameObject[] alignmentIcons;
    [SerializeField] MMFeedbacks[] mmFeedbacks;



    private void OnEnable()
    {
        Lua.RegisterFunction(nameof(GutStrats), this, SymbolExtensions.GetMethodInfo(() => GutStrats(false)));
        Lua.RegisterFunction(nameof(ImprovKnow), this, SymbolExtensions.GetMethodInfo(() => ImprovKnow(false)));
        Lua.RegisterFunction(nameof(Distraceptive), this, SymbolExtensions.GetMethodInfo(() => Distraceptive(false)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(GutStrats));
        Lua.UnregisterFunction(nameof(ImprovKnow));
        Lua.UnregisterFunction(nameof(Distraceptive));
    }

    public void GutStrats(bool value)
    {
        pStat1 = value;

        //statButtons[0].GetComponent<Button>().interactable = !pStat1;
        //statButtons[1].GetComponent<Button>().interactable = pStat1;

        statToggle[0] = true;

        pStatCount = StatToggleCount(statToggle[0], statToggle[1], statToggle[2]);

        if (pStat1 == true)
        {
            if(looseUnit < 3)
            {
                looseUnit++;
            }
            if (byTheBook > 0)
            {
                if((byTheBook - 1) + looseUnit == pStatCount)
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

        if(looseUnit >= 2 || byTheBook >= 2) aligned1 = true;
        else aligned1 = false;

        AlignmentAssignment();

        Debug.Log("PStat 1 is " + pStat1);
    }

    public void ImprovKnow(bool value)
    {
        pStat2 = value;

        //statButtons[2].GetComponent<Button>().interactable = !pStat2;
        //statButtons[3].GetComponent<Button>().interactable = pStat2;

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

        AlignmentAssignment();
    }

    public void Distraceptive(bool value)
    {
        pStat3 = value;

        //statButtons[4].GetComponent<Button>().interactable = !pStat3;
        //statButtons[5].GetComponent<Button>().interactable = pStat3;

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

        AlignmentAssignment();
    }

    public void FriendCold(bool value)
    {
        iStat1 = value;

        //statButtons[6].GetComponent<Button>().interactable = !iStat1;
        //statButtons[7].GetComponent<Button>().interactable = iStat1;

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
                if ((badCop - 1) + badCop == iStatCount)
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

        AlignmentAssignment();
    }

    public void TrustIntimidating(bool value)
    {
        iStat2 = value;

        //statButtons[8].GetComponent<Button>().interactable = !iStat2;
        //statButtons[9].GetComponent<Button>().interactable = iStat2;

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
                if ((badCop - 1) + badCop == iStatCount)
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

        AlignmentAssignment();
    }

    public void EmpathContra(bool value)
    {
        iStat3 = value;

        //statButtons[10].GetComponent<Button>().interactable = !iStat3;
        //statButtons[11].GetComponent<Button>().interactable = iStat3;

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
                if ((badCop - 1) + badCop == iStatCount)
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

        AlignmentAssignment();
    }

    public void MethodInteg(bool value)
    {
        intStat1 = value;

        //statButtons[12].GetComponent<Button>().interactable = !intStat1;
        //statButtons[13].GetComponent<Button>().interactable = intStat1;

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
                if ((hackerMan - 1) + hackerMan == intStatCount)
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

        AlignmentAssignment();
    }

    public void ShredSlend(bool value)
    {
        intStat2 = value;

        //statButtons[14].GetComponent<Button>().interactable = !intStat2;
        //statButtons[15].GetComponent<Button>().interactable = intStat2;

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
                if ((hackerMan - 1) + hackerMan == intStatCount)
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

        AlignmentAssignment();
    }

    public void WrenchCoded(bool value)
    {
        intStat3 = value;

        //statButtons[16].GetComponent<Button>().interactable = !intStat3;
        //statButtons[17].GetComponent<Button>().interactable = intStat3;

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
                if ((hackerMan - 1) + hackerMan == intStatCount)
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

        AlignmentAssignment();
    }

    void AlignmentAssignment()
    {
        if(aligned1 == true)
        {
            if(looseUnit >= 2)
            {
                alignmentIcons[0].SetActive(true);
                alignmentIcons[1].SetActive(false);
                alignmentIcons[3].SetActive(true);
                alignmentIcons[2].SetActive(false);

                DialogueLua.SetVariable("PlayerStats.looseUnitByTheBook", true);
            }
            else if(byTheBook >= 2)
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

}
