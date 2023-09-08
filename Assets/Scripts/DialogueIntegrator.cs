using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;

public class DialogueIntegrator : MonoBehaviour
{
    private void OnEnable()
    {
        Lua.RegisterFunction(nameof(PrintActorProperty), this, SymbolExtensions.GetMethodInfo(() => PrintActorProperty(string.Empty, string.Empty)));
        Lua.RegisterFunction(nameof(PrintVariable), this, SymbolExtensions.GetMethodInfo(() => PrintVariable(string.Empty)));

        Lua.RegisterFunction(nameof(CheckWorkingRollResult), this, SymbolExtensions.GetMethodInfo(() => CheckWorkingRollResult(0)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(PrintActorProperty));
        Lua.UnregisterFunction(nameof(PrintVariable));
        Lua.UnregisterFunction(nameof(CheckWorkingRollResult));
    }

    public void PrintActorProperty(string actor, string variable)
    {
        Debug.Log($"Printing value of {variable} on actor {actor}");

        Lua.Result value = DialogueLua.GetActorField(actor, variable);

        if (value.IsBool)
        {
            Debug.Log($"Variable of name \"{variable}\" found on Actor \"{actor}\" is a Bool with a value of \"{value.AsBool}\".");
        }
        else if (value.IsNumber)
        {
            Debug.Log($"Variable of name \"{variable}\" found on Actor \"{actor}\" is a Number with a value of \"{value.AsInt}\".");
        }
        else if (value.IsString)
        {
            Debug.Log($"Variable of name \"{variable}\" found on Actor \"{actor}\" is a String with a value of \"{value.AsString}\".");
        }
        else
        {
            Debug.Log($"Variable of name \"{variable}\" could not be found on Actor \"{actor}\".");
        }
    }

    public static void PrintVariable(string variable)
    {
        Lua.Result value = DialogueLua.GetVariable(variable);

        if (value.IsBool)
        {
            Debug.Log($"Global Variable of name \"{variable}\" is a Bool with a value of \"{value.AsBool}\".");
        }
        else if (value.IsNumber)
        {
            Debug.Log($"Global Variable of name \"{variable}\" is a Number with a value of \"{value.AsInt}\".");
        }
        else if (value.IsString)
        {
            Debug.Log($"Global Variable of name \"{variable}\" is a String with a value of \"{value.AsString}\".");
        }
        else
        {
            Debug.Log($"Global Variable of name \"{variable}\" could not be found.");
        }
    }

    public bool CheckWorkingRollResult(double evalIndex)
    {
        Debug.Log($"CHECKROLL: Called Roll Check");

        DiceRollRecord rollRecord = FindObjectOfType<DiceRollRecord>();

        int rollIndex = DialogueLua.GetVariable("DiceRoll.operativeIndex").asInt;

        Debug.Log($"CHECKROLL: Checking Roll at index {rollIndex}");

        bool result = false;

        switch((int)evalIndex)
        {
            case 0:

                result = rollRecord.rolls[rollIndex].finalRoll >= rollRecord.rolls[rollIndex].diceEval.critSuccess;

                Debug.Log($"CHECKROLL: Checking For CRITICALSUCCESS, is Final Roll of {rollRecord.rolls[rollIndex].finalRoll} >= to Eval.critSuccess of {rollRecord.rolls[rollIndex].diceEval.critSuccess}? Result is {result}");

                break;

            case 1:

                result = rollRecord.rolls[rollIndex].finalRoll >= rollRecord.rolls[rollIndex].diceEval.pass;

                Debug.Log($"CHECKROLL: Checking For PASS, is Final Roll of {rollRecord.rolls[rollIndex].finalRoll} >= to Eval.pass of {rollRecord.rolls[rollIndex].diceEval.pass}? Result is {result}");

                break;

            case 2:

                result = rollRecord.rolls[rollIndex].finalRoll < rollRecord.rolls[rollIndex].diceEval.critFailure;

                Debug.Log($"CHECKROLL: Checking For CRITICALFAILURE, is Final Roll of {rollRecord.rolls[rollIndex].finalRoll} >= to Eval.critFailure of {rollRecord.rolls[rollIndex].diceEval.critFailure}? Result is {result}");

                break;
        }

        Debug.Log($"CHECKROLL: Finished Checking Roll at index {rollIndex}, result was {result}");

        return result;
    }

}
