using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
public class characterStats : MonoBehaviour
{

    // Policework Stats //
    // 
    bool pstat1;
    // true = gut feeling , false = strategic
    bool pstat2;
    // true = improvisor , false = knowledge 
    bool pstat3;
    // true = easily distracted , false = perceptive
    bool aligned1;
    int looseUnit;
    int byTheBook;

    // Interpersonal Stats // 
    bool istat1;
    // true = friendly , false = cold
    bool istat2;
    // true = trusting , false = intimidating
    bool istat3;
    // true = empathetic , false = contrarian
    bool aligned2;
    int goodCop;
    int badCop;

    // Interests Stats // 
    bool intstat1;
    // true = method man , false = integrated 
    bool intstat2;
    // true = shredded , false = slender
    bool intstat3;
    // true = wrench monkey , false = coded
    bool aligned3;
    int meatHead;
    int hackerMan;

    public GameObject[] buttons;
    [SerializeField] GameObject[] alignmentIcons;
    [SerializeField] MMFeedbacks mmFeedbacks;

    public void GutFeeling()
    {
        pstat1 = true;
        buttons[0].GetComponent<Button>().interactable = false;
        buttons[1].GetComponent<Button>().interactable = true;
        looseUnit++;
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
        intstat1 = true;
        buttons[14].GetComponent<Button>().interactable = false;
        buttons[15].GetComponent<Button>().interactable = true;
        meatHead++;
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
        intstat3 = false;
        buttons[14].GetComponent<Button>().interactable = true;
        buttons[15].GetComponent<Button>().interactable = false;
        hackerMan++;
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
        intstat1 = true;
        buttons[16].GetComponent<Button>().interactable = false;
        buttons[17].GetComponent<Button>().interactable = true;
        meatHead++;
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
    public void AlignmentAssignment()
    {
        // Loose Unit / By the Book Alignments
        if (looseUnit >= 2 && byTheBook <=1 && aligned1 == true)
        {
            alignmentIcons[0].SetActive(true);
            alignmentIcons[1].SetActive(false);
            alignmentIcons[3].SetActive(true);
            alignmentIcons[2].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
        if(byTheBook >=2 && looseUnit <= 1 && aligned1 == true)
        {
            alignmentIcons[2].SetActive(true);
            alignmentIcons[0].SetActive(false);
            alignmentIcons[1].SetActive(true);
            alignmentIcons[3].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
        // Good Cop / Bad Cop Alignments
        if (goodCop >= 2 && badCop <= 1 && aligned2 == true)
        {
            alignmentIcons[4].SetActive(true);
            alignmentIcons[5].SetActive(false);
            alignmentIcons[7].SetActive(true);
            alignmentIcons[6].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
        if (badCop >= 2 && goodCop <= 1 && aligned2 == true)
        {
            alignmentIcons[6].SetActive(true);
            alignmentIcons[4].SetActive(false);
            alignmentIcons[5].SetActive(true);
            alignmentIcons[7].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
        // Meat Head / Hacker Man Alignments
        if (meatHead >= 2 && hackerMan <= 1 && aligned3 == true)
        {
            alignmentIcons[8].SetActive(true);
            alignmentIcons[9].SetActive(false);
            alignmentIcons[11].SetActive(true);
            alignmentIcons[10].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
        if (hackerMan >= 2 && meatHead <= 1 && aligned3 == true)
        {
            alignmentIcons[9].SetActive(true);
            alignmentIcons[8].SetActive(false);
            alignmentIcons[10].SetActive(true);
            alignmentIcons[11].SetActive(false);
            mmFeedbacks.PlayFeedbacks();
        }
    }
    public void PlayerStats()
    {
        Debug.Log("Dayholt is: true = gut feeling , false = strategic: " + pstat1);
        Debug.Log("Dayholt is: true = improvisor , false = knowledge: " + pstat2);
        Debug.Log("Dayholt is: true = easily distracted , false = perceptive: " + pstat3);
        Debug.Log("Loose Unit is: " + looseUnit);
        Debug.Log("By The Book is: " + byTheBook);

        Debug.Log("Dayholt is: true = Friendly , false = Cold: " + istat1);
        Debug.Log("Dayholt is: true = Trusting , false = Intimidating: " + istat2);
        Debug.Log("Dayholt is: true = Empathetic , false = Contrarian: " + istat3);
        Debug.Log("Good Cop is: " + goodCop +" Bad Cop is: " + badCop);

        Debug.Log("Dayholt is: true = Method Man , false = Integrated: " + intstat1);
        Debug.Log("Dayholt is: true = Shredded , false = Slender: " + intstat2);
        Debug.Log("Dayholt is: true = Wrench Monkey , false = Coded: " + intstat3);
        Debug.Log("Meat Head is: " + meatHead + " Hacker Man is: " + hackerMan);
    }
}
