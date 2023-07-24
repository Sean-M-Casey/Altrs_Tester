using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Lofelt.NiceVibrations;
using PixelCrushers.DialogueSystem;
using PixelCrushers;

public class CharacterCreator : MonoBehaviour
{

    // Policework Stats //
    // 
    bool pStat1;        // true = gut feeling , false = strategic
    bool pStat2;        // true = improvisor , false = knowledge 
    bool pStat3;        // true = easily distracted , false = perceptive
    bool aligned1;
    int pStatCount = 0; // Counts police stat toggles
    int pStatTotal = 0; // Current police stat total values
    int looseUnit;
    int byTheBook;

    // Interpersonal Stats // 
    bool iStat1;        // true = friendly , false = cold
    bool iStat2;        // true = trusting , false = intimidating
    bool iStat3;        // true = empathetic , false = contrarian
    bool aligned2;
    int iStatCount = 0;
    int iStatTotal = 0; // Current interpersonal stat total values
    int goodCop;
    int badCop;

    // Interests Stats // 
    bool intStat1;      // true = method man , false = integrated 
    bool intStat2;      // true = shredded , false = slender
    bool intStat3;      // true = wrench monkey , false = coded
    bool aligned3;
    int intStatCount = 0;
    int intStatTotal = 0; // Current interest stat total values
    int meatHead;
    int hackerMan;

    [SerializeField] bool[] stattoggle; //these are turned on once the player presses a button. Used to qualify that 3 traits are chosen. 0-2 Policework | 3-5 Interpersonal | 6-8 Interests
    public GameObject[] buttons;
    [SerializeField] GameObject[] alignmentIcons;
    [SerializeField] MMFeedbacks[] mmFeedbacks;
    [SerializeField] Button[] next;

    // Overview Variables //
    // These get toggled before player locks them in
    [SerializeField] GameObject[] ovTraits; //Odd = Dayholt || Even = Vaughn
    [SerializeField] GameObject[] ovAlignments; //Odd = Dayholt || Even = Vaughn

    private void OnEnable()
    {
        //Shoober
    }

    private void OnDisable()
    {
        
    }

    public void GutFeeling()
    {
        pStat1 = true;
        buttons[0].GetComponent<Button>().interactable = false;
        buttons[1].GetComponent<Button>().interactable = true;
        //looseUnit++;

        if (looseUnit < 3)
        {
            looseUnit++;
        }

        stattoggle[0] = true;
        //if(byTheBook > 0)
        //{
        //    byTheBook--;
        //}
        //if(looseUnit ==2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        
        if (byTheBook > 0)
        {
            if((byTheBook - 1) + looseUnit == pStatCount)
            byTheBook--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();
    }
    public void Strategic()
    {
        pStat1 = false;
        buttons[0].GetComponent<Button>().interactable = true;
        buttons[1].GetComponent<Button>().interactable = false;
        //byTheBook++;

        if (byTheBook < 3)
        {
            byTheBook++;
        }

        stattoggle[0] = true;
        //if (looseUnit > 0)
        //{
        //    looseUnit--;
        //}
        //if (byTheBook == 2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        if (looseUnit > 0)
        {
            if ((looseUnit - 1) + byTheBook == pStatCount)
                looseUnit--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();
    }
    public void Improvisor()
    {
        pStat2 = true;
        buttons[2].GetComponent<Button>().interactable = false;
        buttons[3].GetComponent<Button>().interactable = true;
        //looseUnit++;

        if (looseUnit < 3)
        {
            looseUnit++;
        }

        stattoggle[1] = true;
        //if (byTheBook > 0)
        //{
        //    byTheBook--;
        //}
        //if (looseUnit == 2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        if (byTheBook > 0)
        {
            if ((byTheBook - 1) + looseUnit == pStatCount)
                byTheBook--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();
    }
    public void Knowledge()
    {
        pStat2 = false;
        buttons[2].GetComponent<Button>().interactable = true;
        buttons[3].GetComponent<Button>().interactable = false;
        //byTheBook++;

        if (byTheBook < 3)
        {
            byTheBook++;
        }

        stattoggle[1] = true;
        //if (looseUnit > 0)
        //{
        //    looseUnit--;
        //}
        //if (byTheBook == 2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        if (looseUnit > 0)
        {
            if ((looseUnit - 1) + byTheBook == pStatCount)
                looseUnit--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();
    }
    public void EasilyDistracted()
    {
        pStat3 = true;
        buttons[4].GetComponent<Button>().interactable = false;
        buttons[5].GetComponent<Button>().interactable = true;
        //looseUnit++; 

        if (looseUnit < 3)
        {
            looseUnit++;
        }

        stattoggle[2] = true;
        //if (byTheBook > 0)
        //{
        //    byTheBook--;
        //}
        //if (looseUnit == 2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        if (byTheBook > 0)
        {
            if ((byTheBook - 1) + looseUnit == pStatCount)
                byTheBook--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();

    }
    public void Perceptive()
    {
        pStat3 = false;
        buttons[4].GetComponent<Button>().interactable = true;
        buttons[5].GetComponent<Button>().interactable = false;
        //byTheBook++;

        if (byTheBook < 3)
        {
            byTheBook++;
        }

        stattoggle[2] = true;
        //if (looseUnit > 0)
        //{
        //    looseUnit--;
        //}
        //if (byTheBook == 2)
        //{
        //    aligned1 = true;
        //}
        //else
        //{
        //    aligned1 = false;
        //}

        pStatCount = StatToggleCount(stattoggle[0], stattoggle[1], stattoggle[2]);

        if (looseUnit > 0)
        {
            if ((looseUnit - 1) + byTheBook == pStatCount)
                looseUnit--;
        }

        pStatTotal = looseUnit + byTheBook;

        if (looseUnit >= 2 || byTheBook >= 2)
        {
            aligned1 = true;
        }
        else aligned1 = false;

        Debug.Log($"LooseUnit value is {looseUnit}, and ByTheBook value is {byTheBook}.");
        Debug.Log($"Current Policework toggle count is {pStatCount}.");
        Debug.Log($"Current Policework stat total is {pStatTotal}.");

        AlignmentAssignment();

    }
    // Interpersonal Methods // 
    public void Friendly()
    {
        iStat1 = true;
        buttons[6].GetComponent<Button>().interactable = false;
        buttons[7].GetComponent<Button>().interactable = true;
        //goodCop++;
        if (goodCop < 3)
        {
            goodCop++;
        }

        stattoggle[3] = true;
        //if (badCop > 0)
        //{
        //    badCop--;
        //}
        //if (goodCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (badCop > 0)
        {
            if ((badCop - 1) + goodCop == iStatCount)
                badCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }
    public void Cold()
    {
        iStat1 = false;
        buttons[6].GetComponent<Button>().interactable = true;
        buttons[7].GetComponent<Button>().interactable = false;
        //badCop++;

        if (badCop < 3)
        {
            badCop++;
        }

        stattoggle[3] = true;
        //if (goodCop > 0)
        //{
        //    goodCop --;
        //}
        //if (badCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (goodCop > 0)
        {
            if ((goodCop - 1) + badCop == iStatCount)
                goodCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }
    public void Trusting()
    {
        iStat2 = true;
        buttons[8].GetComponent<Button>().interactable = false;
        buttons[9].GetComponent<Button>().interactable = true;
        //goodCop++;

        if (goodCop < 3)
        {
            goodCop++;
        }

        stattoggle[4] = true;
        //if (badCop > 0)
        //{
        //    badCop--;
        //}
        //if (goodCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (badCop > 0)
        {
            if ((badCop - 1) + goodCop == pStatCount)
                badCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }
    public void Intimidating()
    {
        iStat2 = false;
        buttons[8].GetComponent<Button>().interactable = true;
        buttons[9].GetComponent<Button>().interactable = false;
        //badCop++;

        if (badCop < 3)
        {
            badCop++;
        }

        stattoggle[4] = true;
        //if (goodCop > 0)
        //{
        //    goodCop--;
        //}
        //if (badCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (goodCop > 0)
        {
            if ((goodCop - 1) + badCop == iStatCount)
                goodCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }
    public void Empathetic()
    {
        iStat3 = true;
        buttons[10].GetComponent<Button>().interactable = false;
        buttons[11].GetComponent<Button>().interactable = true;
        //goodCop++;

        if (goodCop < 3)
        {
            goodCop++;
        }

        stattoggle[5] = true;
        //if (badCop > 0)
        //{
        //    badCop--;
        //}
        //if (goodCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (badCop > 0)
        {
            if ((badCop - 1) + goodCop == iStatCount)
                badCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }
    public void Contrarian()
    {
        iStat3 = false;
        buttons[10].GetComponent<Button>().interactable = true;
        buttons[11].GetComponent<Button>().interactable = false;
        //badCop++;

        if (badCop < 3)
        {
            badCop++;
        }

        stattoggle[5] = true;
        //if (goodCop > 0)
        //{
        //    goodCop--;
        //}
        //if (badCop == 2)
        //{
        //    aligned2 = true;
        //}
        //else
        //{
        //    aligned2 = false;
        //}

        iStatCount = StatToggleCount(stattoggle[3], stattoggle[4], stattoggle[5]);

        if (goodCop > 0)
        {
            if ((goodCop - 1) + badCop == iStatCount)
                goodCop--;
        }

        iStatTotal = goodCop + badCop;

        if (goodCop >= 2 || badCop >= 2)
        {
            aligned2 = true;
        }
        else aligned2 = false;

        Debug.Log($"GoodCop value is {goodCop}, and BadCop value is {badCop}.");
        Debug.Log($"Current Interpersonal toggle count is {iStatCount}.");
        Debug.Log($"Current Interpersonal stat total is {iStatTotal}.");

        AlignmentAssignment();
    }

    // INTERESTS // 
    public void MethodMan()
    {
        intStat1 = true;
        buttons[12].GetComponent<Button>().interactable = false;
        buttons[13].GetComponent<Button>().interactable = true;
        //meatHead++;

        if (meatHead < 3)
        {
            meatHead++;
        }

        stattoggle[6] = true;
        //if (hackerMan > 0)
        //{
        //    hackerMan--;
        //}
        //if (meatHead == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (hackerMan > 0)
        {
            if ((hackerMan - 1) + meatHead == intStatCount)
                hackerMan--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
    public void Integrated()
    {
        intStat3 = false;
        buttons[12].GetComponent<Button>().interactable = true;
        buttons[13].GetComponent<Button>().interactable = false;
        //hackerMan++;

        if (hackerMan < 3)
        {
            hackerMan++;
        }

        stattoggle[6] = true;
        //if (meatHead > 0)
        //{
        //    meatHead--;
        //}
        //if (hackerMan == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (meatHead > 0)
        {
            if ((meatHead - 1) + hackerMan == intStatCount)
                meatHead--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
    public void Shredded()
    {
        intStat2 = true;
        buttons[14].GetComponent<Button>().interactable = false;
        buttons[15].GetComponent<Button>().interactable = true;
        //meatHead++;

        if (meatHead < 3)
        {
            meatHead++;
        }

        stattoggle[7] = true;
        //if (hackerMan > 0)
        //{
        //    hackerMan--;
        //}
        //if (meatHead == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (hackerMan > 0)
        {
            if ((hackerMan - 1) + meatHead == intStatCount)
                hackerMan--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
    public void Slender()
    {
        intStat2= false;
        buttons[14].GetComponent<Button>().interactable = true;
        buttons[15].GetComponent<Button>().interactable = false;
        //hackerMan++;

        if (hackerMan < 3)
        {
            hackerMan++;
        }

        stattoggle[7] = true;
        //if (meatHead > 0)
        //{
        //    meatHead--;
        //}
        //if (hackerMan == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (meatHead > 0)
        {
            if ((meatHead - 1) + hackerMan == intStatCount)
                meatHead--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
    public void WrenchMonkey()
    {
        intStat3 = true;
        buttons[16].GetComponent<Button>().interactable = false;
        buttons[17].GetComponent<Button>().interactable = true;
        //meatHead++;

        if (meatHead < 3)
        {
            meatHead++;
        }

        stattoggle[8] = true;
        //if (hackerMan > 0)
        //{
        //    hackerMan--;
        //}
        //if (meatHead == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (hackerMan > 0)
        {
            if ((hackerMan - 1) + meatHead == intStatCount)
                hackerMan--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
    public void Coded()
    {
        intStat3 = false;
        buttons[16].GetComponent<Button>().interactable = true;
        buttons[17].GetComponent<Button>().interactable = false;
        //hackerMan++;

        if (hackerMan < 3)
        {
            hackerMan++;
        }

        stattoggle[8] = true;
        //if (meatHead > 0)
        //{
        //    meatHead--;
        //}
        //if (hackerMan == 2)
        //{
        //    aligned3 = true;
        //}
        //else
        //{
        //    aligned3 = false;
        //}

        intStatCount = StatToggleCount(stattoggle[6], stattoggle[7], stattoggle[8]);

        if (meatHead > 0)
        {
            if ((meatHead - 1) + hackerMan == intStatCount)
                meatHead--;
        }

        intStatTotal = meatHead + hackerMan;

        if (meatHead >= 2 || hackerMan >= 2)
        {
            aligned3 = true;
        }
        else aligned3 = false;

        Debug.Log($"MeatHead value is {meatHead}, and HackerMan value is {hackerMan}.");
        Debug.Log($"Current Interests toggle count is {intStatCount}.");
        Debug.Log($"Current Interests stat total is {intStatTotal}.");

        AlignmentAssignment();
    }
  
    //This method is called upon every trait button press. It will check to see if the character has enough traits to be aligned yet. 
    // Anything >=2 is aligned to the segments axis eg: Good cop / bad cop
    public void AlignmentAssignment()
    {
        ToggleNext();
        // Loose Unit / By the Book Alignments
        if(aligned1 == true)
        {
            if (looseUnit >= 2)
            {
                Debug.Log("First Alignment is leaning towards LooseUnit.");

                alignmentIcons[0].SetActive(true);
                alignmentIcons[1].SetActive(false);
                alignmentIcons[3].SetActive(true);
                alignmentIcons[2].SetActive(false);
                mmFeedbacks[0].PlayFeedbacks();

            }
            else if (byTheBook >= 2)
            {
                Debug.Log("First Alignment is leaning towards ByTheBook.");

                alignmentIcons[2].SetActive(true);
                alignmentIcons[0].SetActive(false);
                alignmentIcons[1].SetActive(true);
                alignmentIcons[3].SetActive(false);
                mmFeedbacks[0].PlayFeedbacks();
            }
        }
        
        // Good Cop / Bad Cop Alignments
        if(aligned2 == true)
        {
            if (goodCop >= 2)
            {
                Debug.Log("Second Alignment is leaning towards GoodCop.");

                alignmentIcons[4].SetActive(true);
                alignmentIcons[5].SetActive(false);
                alignmentIcons[7].SetActive(true);
                alignmentIcons[6].SetActive(false);
                mmFeedbacks[1].PlayFeedbacks();
            }
            else if (badCop >= 2)
            {
                Debug.Log("Second Alignment is leaning towards BadCop.");

                alignmentIcons[6].SetActive(true);
                alignmentIcons[4].SetActive(false);
                alignmentIcons[5].SetActive(true);
                alignmentIcons[7].SetActive(false);
                mmFeedbacks[1].PlayFeedbacks();
            }
        }
        
        // Meat Head / Hacker Man Alignments
        if(aligned3 == true)
        {
            if (meatHead >= 2)
            {
                Debug.Log("Third Alignment is leaning towards MeatHead.");

                alignmentIcons[8].SetActive(true);
                alignmentIcons[9].SetActive(false);
                alignmentIcons[11].SetActive(true);
                alignmentIcons[10].SetActive(false);
                mmFeedbacks[2].PlayFeedbacks();
            }
            else if (hackerMan >= 2)
            {
                Debug.Log("Third Alignment is leaning towards HackerMan.");

                alignmentIcons[9].SetActive(true);
                alignmentIcons[8].SetActive(false);
                alignmentIcons[10].SetActive(true);
                alignmentIcons[11].SetActive(false);
                mmFeedbacks[2].PlayFeedbacks();
            }
        }
        
    }

    //This method purely toggles the next buttons based on the player pressing 1 button from the 3 rows (so 3 buttons total)
    public void ToggleNext()
    {
        if (stattoggle[0] == true && stattoggle[1] == true && stattoggle[2] == true)
        {
            next[0].interactable = true;
        }
        if (stattoggle[3] == true && stattoggle[4] == true && stattoggle[5] == true)
        {
            next[1].interactable = true;
        }
        if (stattoggle[6] == true && stattoggle[7] == true && stattoggle[8] == true)
        {
            next[2].interactable = true;
        }
    }

    //This method creates the character sheet before player confirms
    public void Overview()
    {
        if (pStat1 == true)
        {
            ovTraits[0].SetActive(true);
            ovTraits[1].SetActive(true);
        }
        else
        {
            ovTraits[2].SetActive(true);
            ovTraits[3].SetActive(true);
        }
        if (pStat2 == true)
        {
            ovTraits[4].SetActive(true);
            ovTraits[5].SetActive(true);
        }
        else
        {
            ovTraits[6].SetActive(true);
            ovTraits[7].SetActive(true);
        }
        if (pStat3 == true)
        {
            ovTraits[8].SetActive(true);
            ovTraits[9].SetActive(true);
        }
        else
        {
            ovTraits[10].SetActive(true);
            ovTraits[11].SetActive(true);
        }
        if (iStat1 == true)
        {
            ovTraits[12].SetActive(true);
            ovTraits[13].SetActive(true);
        }
        else
        {
            ovTraits[14].SetActive(true);
            ovTraits[15].SetActive(true);
        }
        if (iStat2 == true)
        {
            ovTraits[16].SetActive(true);
            ovTraits[17].SetActive(true);
        }
        else
        {
            ovTraits[18].SetActive(true);
            ovTraits[19].SetActive(true);
        }
        if (iStat3 == true)
        {
            ovTraits[20].SetActive(true);
            ovTraits[21].SetActive(true);
        }
        else
        {
            ovTraits[22].SetActive(true);
            ovTraits[23].SetActive(true);
        }
        if (intStat1 == true)
        {
            ovTraits[24].SetActive(true);
            ovTraits[25].SetActive(true);
        }
        else
        {
            ovTraits[26].SetActive(true);
            ovTraits[27].SetActive(true);
        }
        if (intStat2 == true)
        {
            ovTraits[28].SetActive(true);
            ovTraits[29].SetActive(true);
        }
        else
        {
            ovTraits[30].SetActive(true);
            ovTraits[31].SetActive(true);
        }
        if (intStat3 == true)
        {
            ovTraits[32].SetActive(true);
        }
        else
        {
            ovTraits[33].SetActive(true);
        }

        PlayerStats();
    }

    int StatToggleCount(bool stat1, bool stat2, bool stat3)
    {
        int count = 0;

        if (stat1 == true) count++;
        if (stat2 == true) count++;
        if (stat3 == true) count++;

        return count;
    }

    public void ResetOverview()
    {
        foreach(GameObject data in ovTraits)
        {
            data.SetActive(false);
        }
    }
    public void PlayerStats()
    {
        SetPlayerStats();

        PrintPlayerStats();
    }

    public void SetPlayerStats()
    {
        //Setting Policework Stats
        DialogueLua.SetVariable("PlayerStats.gutFeeling", pStat1);
        DialogueLua.SetVariable("PlayerStats.strategic", !pStat1);

        DialogueLua.SetVariable("PlayerStats.improvisor", pStat2);
        DialogueLua.SetVariable("PlayerStats.knowledge", !pStat2);

        DialogueLua.SetVariable("PlayerStats.easilyDistracted", pStat3);
        DialogueLua.SetVariable("PlayerStats.perceptive", !pStat3);

        //Setting Interpersonal Stats
        DialogueLua.SetVariable("PlayerStats.friendly", iStat1);
        DialogueLua.SetVariable("PlayerStats.cold", !iStat1);

        DialogueLua.SetVariable("PlayerStats.trusting", iStat2);
        DialogueLua.SetVariable("PlayerStats.intimidating", !iStat2);

        DialogueLua.SetVariable("PlayerStats.empathetic", iStat3);
        DialogueLua.SetVariable("PlayerStats.contrarian", !iStat3);

        //Setting Interests Stats
        DialogueLua.SetVariable("PlayerStats.methodMan", intStat1);
        DialogueLua.SetVariable("PlayerStats.integrated", !intStat1);

        DialogueLua.SetVariable("PlayerStats.shredded", intStat2);
        DialogueLua.SetVariable("PlayerStats.slenderMan", !intStat2);

        DialogueLua.SetVariable("PlayerStats.wrenchMonkey", intStat3);
        DialogueLua.SetVariable("PlayerStats.coded", !intStat3);

        //Setting Alignment Stats
        DialogueLua.SetVariable("PlayerStats.looseUnit", aligned1);
        DialogueLua.SetVariable("PlayerStats.byTheBook", !aligned1);

        DialogueLua.SetVariable("PlayerStats.goodCop", aligned2);
        DialogueLua.SetVariable("PlayerStats.badCop", !aligned2);

        DialogueLua.SetVariable("PlayerStats.boof", aligned3);
        DialogueLua.SetVariable("PlayerStats.hackerMan", !aligned3);
    }


    public void PrintPlayerStat(string statName)
    {
        Debug.Log($"Variable {statName} is {DialogueLua.GetVariable(statName).asBool}");
    }

    public void PrintPlayerStats()
    {
        //Print Policework Stats
        PrintPlayerStat("PlayerStats.gutFeeling");
        PrintPlayerStat("PlayerStats.strategic");

        PrintPlayerStat("PlayerStats.improvisor");
        PrintPlayerStat("PlayerStats.knowledge");

        PrintPlayerStat("PlayerStats.easilyDistracted");
        PrintPlayerStat("PlayerStats.perceptive");

        //Print Interpersonal Stats
        PrintPlayerStat("PlayerStats.friendly");
        PrintPlayerStat("PlayerStats.cold");

        PrintPlayerStat("PlayerStats.trusting");
        PrintPlayerStat("PlayerStats.intimidating");

        PrintPlayerStat("PlayerStats.empathetic");
        PrintPlayerStat("PlayerStats.contrarian");

        //Print Interests Stats
        PrintPlayerStat("PlayerStats.methodMan");
        PrintPlayerStat("PlayerStats.integrated");

        PrintPlayerStat("PlayerStats.shredded");
        PrintPlayerStat("PlayerStats.slenderMan");

        PrintPlayerStat("PlayerStats.wrenchMonkey");
        PrintPlayerStat("PlayerStats.coded");

        //Print Alignment Stats
        PrintPlayerStat("PlayerStats.looseUnit");
        PrintPlayerStat("PlayerStats.byTheBook");

        PrintPlayerStat("PlayerStats.goodCop");
        PrintPlayerStat("PlayerStats.badCop");

        PrintPlayerStat("PlayerStats.boof");
        PrintPlayerStat("PlayerStats.hackerMan");
    }
}
