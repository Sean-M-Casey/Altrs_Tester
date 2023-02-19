using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
public class characterStats : MonoBehaviour
{


    bool pstat1;
    // true = gut feeling , false = strategic
    bool pstat2;
    // true = improvisor , false = knowledge 
    bool pstat3;
    // true = easily distracted , false = perceptive
    bool aligned1;
    int looseUnit;
    int byTheBook;


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

    public void AlignmentAssignment()
    {
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
            if(byTheBook >= 2 && looseUnit <=1)
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
    }
}
