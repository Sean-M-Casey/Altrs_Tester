using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
public class CharacterCreator : MonoBehaviour
{

    // Policework Stats //
    // 
    bool pstat1;        // true = gut feeling , false = strategic
    bool pstat2;        // true = improvisor , false = knowledge 
    bool pstat3;        // true = easily distracted , false = perceptive
    bool aligned1;
    int looseUnit;
    int byTheBook;

    // Interpersonal Stats // 
    bool istat1;        // true = friendly , false = cold
    bool istat2;        // true = trusting , false = intimidating
    bool istat3;        // true = empathetic , false = contrarian
    bool aligned2;
    int goodCop;
    int badCop;

    // Interests Stats // 
    bool intstat1;      // true = method man , false = integrated 
    bool intstat2;      // true = shredded , false = slender
    bool intstat3;      // true = wrench monkey , false = coded
    bool aligned3;
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


    public void GutFeeling()
    {
        pstat1 = true;
        buttons[0].GetComponent<Button>().interactable = false;
        buttons[1].GetComponent<Button>().interactable = true;
        looseUnit++;
        stattoggle[0] = true;
        if(byTheBook > 0)
        {
            byTheBook--;
        }
        if(looseUnit ==2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();
    }
    public void Strategic()
    {
        pstat1 = false;
        buttons[0].GetComponent<Button>().interactable = true;
        buttons[1].GetComponent<Button>().interactable = false;
        byTheBook++;
        stattoggle[0] = true;
        if (looseUnit > 0)
        {
            looseUnit--;
        }
        if (byTheBook == 2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();
    }
    public void Improvisor()
    {
        pstat2 = true;
        buttons[2].GetComponent<Button>().interactable = false;
        buttons[3].GetComponent<Button>().interactable = true;
        looseUnit++;
        stattoggle[1] = true;
        if (byTheBook > 0)
        {
            byTheBook--;
        }
        if (looseUnit == 2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();
    }
    public void Knowledge()
    {
        pstat2 = false;
        buttons[2].GetComponent<Button>().interactable = true;
        buttons[3].GetComponent<Button>().interactable = false;
        byTheBook++;
        stattoggle[1] = true;
        if (looseUnit > 0)
        {
            looseUnit--;
        }
        if (byTheBook == 2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();
    }
    public void EasilyDistracted()
    {
        pstat3 = true;
        buttons[4].GetComponent<Button>().interactable = false;
        buttons[5].GetComponent<Button>().interactable = true;
        looseUnit++; 
        stattoggle[2] = true;
        if (byTheBook > 0)
        {
            byTheBook--;
        }
        if (looseUnit == 2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();

    }
    public void Perceptive()
    {
        pstat3 = false;
        buttons[4].GetComponent<Button>().interactable = true;
        buttons[5].GetComponent<Button>().interactable = false;
        byTheBook++;
        stattoggle[2] = true;
        if (looseUnit > 0)
        {
            looseUnit--;
        }
        if (byTheBook == 2)
        {
            aligned1 = true;
        }
        else
        {
            aligned1 = false;
        }
        AlignmentAssignment();

    }
    // Interpersonal Methods // 
    public void Friendly()
    {
        istat1 = true;
        buttons[6].GetComponent<Button>().interactable = false;
        buttons[7].GetComponent<Button>().interactable = true;
        goodCop++;
        stattoggle[3] = true;
        if (badCop > 0)
        {
            badCop--;
        }
        if (goodCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }
    public void Cold()
    {
        istat1 = false;
        buttons[6].GetComponent<Button>().interactable = true;
        buttons[7].GetComponent<Button>().interactable = false;
        badCop++;
        stattoggle[3] = true;
        if (goodCop > 0)
        {
            goodCop --;
        }
        if (badCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }
    public void Trusting()
    {
        istat2 = true;
        buttons[8].GetComponent<Button>().interactable = false;
        buttons[9].GetComponent<Button>().interactable = true;
        goodCop++;
        stattoggle[4] = true;
        if (badCop > 0)
        {
            badCop--;
        }
        if (goodCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }
    public void Intimidating()
    {
        istat2 = false;
        buttons[8].GetComponent<Button>().interactable = true;
        buttons[9].GetComponent<Button>().interactable = false;
        badCop++;
        stattoggle[4] = true;
        if (goodCop > 0)
        {
            goodCop--;
        }
        if (badCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }
    public void Empathetic()
    {
        istat3 = true;
        buttons[10].GetComponent<Button>().interactable = false;
        buttons[11].GetComponent<Button>().interactable = true;
        goodCop++;
        stattoggle[5] = true;
        if (badCop > 0)
        {
            badCop--;
        }
        if (goodCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }
    public void Contrarian()
    {
        istat3 = false;
        buttons[10].GetComponent<Button>().interactable = true;
        buttons[11].GetComponent<Button>().interactable = false;
        badCop++;
        stattoggle[5] = true;
        if (goodCop > 0)
        {
            goodCop--;
        }
        if (badCop == 2)
        {
            aligned2 = true;
        }
        else
        {
            aligned2 = false;
        }
        AlignmentAssignment();
    }

    // INTERESTS // 
    public void MethodMan()
    {
        intstat1 = true;
        buttons[12].GetComponent<Button>().interactable = false;
        buttons[13].GetComponent<Button>().interactable = true;
        meatHead++;
        stattoggle[6] = true;
        if (hackerMan > 0)
        {
            hackerMan--;
        }
        if (meatHead == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
    public void Integrated()
    {
        intstat3 = false;
        buttons[12].GetComponent<Button>().interactable = true;
        buttons[13].GetComponent<Button>().interactable = false;
        hackerMan++;
        stattoggle[6] = true;
        if (meatHead > 0)
        {
            meatHead--;
        }
        if (hackerMan == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
    public void Shredded()
    {
        intstat2 = true;
        buttons[14].GetComponent<Button>().interactable = false;
        buttons[15].GetComponent<Button>().interactable = true;
        meatHead++;
        stattoggle[7] = true;
        if (hackerMan > 0)
        {
            hackerMan--;
        }
        if (meatHead == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
    public void Slender()
    {
        intstat2= false;
        buttons[14].GetComponent<Button>().interactable = true;
        buttons[15].GetComponent<Button>().interactable = false;
        hackerMan++;
        stattoggle[7] = true;
        if (meatHead > 0)
        {
            meatHead--;
        }
        if (hackerMan == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
    public void WrenchMonkey()
    {
        intstat3 = true;
        buttons[16].GetComponent<Button>().interactable = false;
        buttons[17].GetComponent<Button>().interactable = true;
        meatHead++;
        stattoggle[8] = true;
        if (hackerMan > 0)
        {
            hackerMan--;
        }
        if (meatHead == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
    public void Coded()
    {
        intstat3 = false;
        buttons[16].GetComponent<Button>().interactable = true;
        buttons[17].GetComponent<Button>().interactable = false;
        hackerMan++;
        stattoggle[8] = true;
        if (meatHead > 0)
        {
            meatHead--;
        }
        if (hackerMan == 2)
        {
            aligned3 = true;
        }
        else
        {
            aligned3 = false;
        }
        AlignmentAssignment();
    }
  
    //This method is called upon every trait button press. It will check to see if the character has enough traits to be aligned yet. 
    // Anything >=2 is aligned to the segments axis eg: Good cop / bad cop
    public void AlignmentAssignment()
    {
        ToggleNext();
        // Loose Unit / By the Book Alignments
        if (looseUnit >= 2 && byTheBook <=1 && aligned1 == true)
        {
            alignmentIcons[0].SetActive(true);
            alignmentIcons[1].SetActive(false);
            alignmentIcons[3].SetActive(true);
            alignmentIcons[2].SetActive(false);
            mmFeedbacks[0].PlayFeedbacks();
         
        }
        if(byTheBook >=2 && looseUnit <= 1 && aligned1 == true)
        {
            alignmentIcons[2].SetActive(true);
            alignmentIcons[0].SetActive(false);
            alignmentIcons[1].SetActive(true);
            alignmentIcons[3].SetActive(false);
            mmFeedbacks[0].PlayFeedbacks();
        }
        // Good Cop / Bad Cop Alignments
        if (goodCop >= 2 && badCop <= 1 && aligned2 == true)
        {
            alignmentIcons[4].SetActive(true);
            alignmentIcons[5].SetActive(false);
            alignmentIcons[7].SetActive(true);
            alignmentIcons[6].SetActive(false);
            mmFeedbacks[1].PlayFeedbacks();
        }
        if (badCop >= 2 && goodCop <= 1 && aligned2 == true)
        {
            alignmentIcons[6].SetActive(true);
            alignmentIcons[4].SetActive(false);
            alignmentIcons[5].SetActive(true);
            alignmentIcons[7].SetActive(false);
            mmFeedbacks[1].PlayFeedbacks();
        }
        // Meat Head / Hacker Man Alignments
        if (meatHead >= 2 && hackerMan <= 1 && aligned3 == true)
        {
            alignmentIcons[8].SetActive(true);
            alignmentIcons[9].SetActive(false);
            alignmentIcons[11].SetActive(true);
            alignmentIcons[10].SetActive(false);
            mmFeedbacks[2].PlayFeedbacks();
        }
        if (hackerMan >= 2 && meatHead <= 1 && aligned3 == true)
        {
            alignmentIcons[9].SetActive(true);
            alignmentIcons[8].SetActive(false);
            alignmentIcons[10].SetActive(true);
            alignmentIcons[11].SetActive(false);
            mmFeedbacks[2].PlayFeedbacks();
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
        if (pstat1 == true)
        {
            ovTraits[0].SetActive(true);
            ovTraits[1].SetActive(true);
        }
        else
        {
            ovTraits[2].SetActive(true);
            ovTraits[3].SetActive(true);
        }
        if (pstat2 == true)
        {
            ovTraits[4].SetActive(true);
            ovTraits[5].SetActive(true);
        }
        else
        {
            ovTraits[6].SetActive(true);
            ovTraits[7].SetActive(true);
        }
        if (pstat3 == true)
        {
            ovTraits[8].SetActive(true);
            ovTraits[9].SetActive(true);
        }
        else
        {
            ovTraits[10].SetActive(true);
            ovTraits[11].SetActive(true);
        }
        if (istat1 == true)
        {
            ovTraits[12].SetActive(true);
            ovTraits[13].SetActive(true);
        }
        else
        {
            ovTraits[14].SetActive(true);
            ovTraits[15].SetActive(true);
        }
        if (istat2 == true)
        {
            ovTraits[16].SetActive(true);
            ovTraits[17].SetActive(true);
        }
        else
        {
            ovTraits[18].SetActive(true);
            ovTraits[19].SetActive(true);
        }
        if (istat3 == true)
        {
            ovTraits[20].SetActive(true);
            ovTraits[21].SetActive(true);
        }
        else
        {
            ovTraits[22].SetActive(true);
            ovTraits[23].SetActive(true);
        }
        if (intstat1 == true)
        {
            ovTraits[24].SetActive(true);
            ovTraits[25].SetActive(true);
        }
        else
        {
            ovTraits[26].SetActive(true);
            ovTraits[27].SetActive(true);
        }
        if (intstat2 == true)
        {
            ovTraits[28].SetActive(true);
            ovTraits[29].SetActive(true);
        }
        else
        {
            ovTraits[30].SetActive(true);
            ovTraits[31].SetActive(true);
        }
        if (intstat3 == true)
        {
            ovTraits[32].SetActive(true);
        }
        else
        {
            ovTraits[33].SetActive(true);
        }
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
        if(pstat1 == true)
        {
            Debug.Log("Dayholt has the Gut Feeling Trait");
            Debug.Log("Vaughn has the Strategic Feeling Trait");

        }
    }
}
