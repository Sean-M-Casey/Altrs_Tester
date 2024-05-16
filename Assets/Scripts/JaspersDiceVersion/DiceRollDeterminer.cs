using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollDeterminer : MonoBehaviour
{
    public GameObject determineUI;

    public Button critPassButton;
    public Button passButton;
    public Button failButton;
    public Button critFailButton;

    public bool resetDevDiceEachRoll = true;

    public static bool devDiceActive = false;

    bool canSwitchMode = true;

    void Update()
    {
        if (canSwitchMode)
        {
            if(Input.GetKey(KeyCode.Slash))
            {
                if(Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(DevDiceToggleBuffer());
                }
            }
        }
    }

    IEnumerator DevDiceToggleBuffer()
    {
        canSwitchMode = false;
        devDiceActive = !devDiceActive;

        yield return new WaitForSecondsRealtime(2f);

        canSwitchMode = true;
    }
}
