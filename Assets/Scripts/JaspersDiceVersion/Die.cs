//Inspired by Darcy's Dice Manager script

using PixelCrushers.DialogueSystem.SequencerCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Die : MonoBehaviour
{
    public DiceType type;

    public Transform[] diceSides;

    private int currentRoll = 1;

    public int DiceRollOut { get { return currentRoll; } }

    public bool showUp;

    public int index;

    public bool setVector;

    public bool showRoll;

    public bool dieRoll;

    int rolledIndex = 0;


    private void Update()
    {
        if (showUp)
        {
            foreach (Transform obj in diceSides)
            {
                Debug.Log($"Side {obj.name}'s up vector is {obj.up.y}");
            }
        }

        if (setVector)
        {
            diceSides[index].up = Vector3.up;
            setVector = false;
        }

        if (dieRoll)
        {
            if (diceSides[rolledIndex].up.y <= 0.98f) CheckDiceRoll();
        }

        if (showRoll)
        {
            Debug.Log($"Dice Roll is {DiceRollOut}.");
            showRoll = false;
        }
    }

    public IEnumerator RollSelf(SequencerCommandDiceRoll commandDiceRoll, Vector3 direction, float forceStrength)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * forceStrength, ForceMode.Force);

        yield return new WaitForSecondsRealtime(0.3f);

        dieRoll = true;

        while (rb.velocity != Vector3.zero)
        {
            yield return null;
        }

        dieRoll = false;
        Debug.Log("DIE: Die Finished Rolling");
        commandDiceRoll.gameObject.SendMessage(nameof(commandDiceRoll.CheckIfRollFinished), commandDiceRoll.WorkingRollAccess);
    }

    public void CheckDiceRoll()
    {
        //Debug.Log("Dice is rolling.");
        foreach (Transform obj in diceSides)
        {
            if (diceSides[rolledIndex].up.y < obj.up.y)
            {
                rolledIndex = ArrayUtility.IndexOf(diceSides, obj);
            }
        }
        showRoll = true;
        currentRoll = int.Parse(diceSides[rolledIndex].name);
    }

}
