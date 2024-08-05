using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHackFuncWriteBanana : HackFunctionality
{


    public override IEnumerator ExecuteRoutine(IHackable target)
    {
        state = HackState.InProgress;

        /*Do the hacky wacky woo here*/

        while (state != HackState.Completed)
        {
            yield return null;
        }

        Resolve();
    }

}
