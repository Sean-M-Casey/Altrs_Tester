using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HackableObject : MonoBehaviour, IHackable
{
    private HackingScreen screenInstance;

    [SerializeField] private List<HackScriptable> hacks = new List<HackScriptable>();

    private List<HackExec> hackExecs = new List<HackExec>();

    public List<HackScriptable> Hacks { get { return hacks; } }
    public List<HackExec> HackExecs { get { return hackExecs; } }

    public HackingScreen ScreenInstance { get { return screenInstance; } }

    public void OnHackingStart()
    {
        StartCoroutine(StaggerStart());

    }

    public void OnHackingFinish()
    {
        StartCoroutine(StaggerFinish());

        //HackingMain.instance.updateHackScreen -= OnHackingFinish;
        //HackingMain.instance.closeHackScreen -= OnHackingFinish;

        //Debug.Log("HACKTEST: Subtracted OnHackingFinish");
        //foreach (HackExec exec in hackExecs)
        //{
        //    exec.DestroyHack();
        //}

        //hackExecs.Clear();

        //screenInstance.CloseScreen();
    }

    public void OpenScreen()
    {
        screenInstance = Instantiate(HackingMain.instance.HackingScreenPrefab, transform);
        screenInstance.parent = this;
        screenInstance.OpenScreen();
    }

    public void AddButtons()
    {
        Debug.Log("HACKTEST: AddButtonsCalled");

        foreach (HackScriptable hack in hacks)
        {
            InstantiateHack(hack);
        }

        hackExecs[0].HackButton.Select();
    }

    /// <summary>Instantiates a HackExec, initialising it with the given HackScriptable.</summary>
    /// <param name="hack">Hack to initialise HackExec with.</param>
    void InstantiateHack(HackScriptable hack)
    {

        HackExec hackExec = Instantiate(HackingMain.instance.HackExecPrefab, screenInstance.Panel.transform);

        hackExec.InitialiseHack(hack);

        hackExec.Hack.SetTarget(this);

        hackExecs.Add(hackExec);
    }


    /// <summary>Staggers hack screen opening across 2 frames.</summary>
    /// <returns>Returns a coroutine.</returns>
    public IEnumerator StaggerStart()
    {
        OpenScreen();

        HackingMain.instance.closeHackScreen += OnHackingFinish;

        Debug.Log("HACKTEST: Added OnHackingFinish");

        yield return null;

        AddButtons();

        HackingMain.instance.updateHackScreen -= OnHackingStart;
    }

    public IEnumerator StaggerFinish()
    {
        HackingMain.instance.updateHackScreen -= OnHackingFinish;
        HackingMain.instance.closeHackScreen -= OnHackingFinish;

        Debug.Log("HACKTEST: Subtracted OnHackingFinish");

        yield return null;

        foreach (HackExec exec in hackExecs)
        {
            exec.DestroyHack();
        }

        hackExecs.Clear();

        yield return null;

        screenInstance.CloseScreen();
    }

    /// <summary>Closes and Destroys Hack Screen.</summary>
    public void CloseScreen()
    {
        Debug.Log("HACKTEST: HacksScreenDelete");
        Destroy(screenInstance.gameObject);
        if(screenInstance != null) screenInstance = null;
    }

}
