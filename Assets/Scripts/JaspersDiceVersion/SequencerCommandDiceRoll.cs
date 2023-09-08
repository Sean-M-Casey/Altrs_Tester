
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandDiceRoll : SequencerCommand
    { // Rename to SequencerCommand<YourCommand>

        DiceRollRecord rollRecord;

        string rollingActorName;
        string contestingNPCName;
        string variableName;
        bool variableSide;

        DiceRoll workingRoll;
        DiceEval diceEval;

        bool hasWorkingRollBegan = false;
        bool isWorkingRollFinished = false;

        public DiceRoll WorkingRollAccess { get { return workingRoll; } }

        public bool WorkingRollComplete { get { return hasWorkingRollBegan == true && isWorkingRollFinished == true; } }


        // DiceRoll("Dayholt", "PlayerStats.gutFeelingStrategic", false, 15, 10, 5);
        // DiceRoll("Dayholt", "PlayerStats.gutFeelingStrategic", false, "Dmi");
        public void Awake()
        {
            //Debug.Log("SEQUENCER: Commencing DiceRoll");
            if(rollRecord == null) rollRecord = FindObjectOfType<DiceRollRecord>();

            rollingActorName = GetParameter(0, string.Empty);
            Debug.Log($"SEQUENCER: RollingActorName is {((rollingActorName != null) ? rollingActorName : "doing the nully")}");

            contestingNPCName = GetParameter(3, string.Empty);
            Debug.Log($"SEQUENCER: ContestingNPCName is {((contestingNPCName != null) ? contestingNPCName : "doing the nully")}");

            variableName = GetParameter(1, string.Empty);
            Debug.Log($"SEQUENCER: VariableName is {((variableName != null) ? variableName : "doing the nully")}");

            variableSide = GetParameterAsBool(2, false);
            Debug.Log($"SEQUENCER: VariableSide is {variableSide}");

            //diceEval = new DiceEval(GetParameterAsInt(3), GetParameterAsInt(4), GetParameterAsInt(5));
            diceEval = EvalByDisposition(contestingNPCName);
            Debug.Log($"SEQUENCER: DiceEval has values of {diceEval.critSuccess}, {diceEval.pass}, {diceEval.critFailure}");

            #region CreatingDiceRoll
            string rollType = string.Empty;
            List<Die> diceTypes = new List<Die>();
            //Debug.Log($"SEQUENCER: RollType is {((rollType == string.Empty) ? "Empty" : rollType)}.");

            switch (variableName)
            {
                case "PlayerStats.gutFeelingStrategic":
                    rollType = (variableSide) ? "GutFeeling" : "Strategic";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.improvisorKnowledge":
                    rollType = (variableSide) ? "Improvisor" : "Knowledge";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.distractPercept":
                    rollType = (variableSide) ? "EasilyDistracted" : "Perceptive";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.friendlyCold":
                    rollType = (variableSide) ? "Friendly" : "Cold";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.trustingIntimidating":
                    rollType = (variableSide) ? "Trusting" : "Intimidating";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.empathContrarian":
                    rollType = (variableSide) ? "Empathetic" : "Contrarian";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.methodManIntegrated":
                    rollType = (variableSide) ? "MethodMan" : "Integrated";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.shreddedSlender":
                    rollType = (variableSide) ? "Shredded" : "Slender";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                case "PlayerStats.wrenchMonkeyCoded":
                    rollType = (variableSide) ? "WrenchMonkey" : "Coded";
                    Debug.Log($"SEQUENCER: RollType is now set to ({rollType}).");

                    break;

                default:
                    Debug.LogError("SEQUENCER: Variable name given is invalid, or not usable.");

                    break;
            }

            diceTypes.Add(rollRecord.D20);

            if (DialogueLua.GetVariable(variableName).asBool == variableSide)
            {
                diceTypes.Add(rollRecord.D4);
            }

            workingRoll = new DiceRoll(rollingActorName, rollType, diceTypes.ToArray(), diceEval);
            workingRoll.isReady = true;

            #endregion

            //Debug.Log("SEQUENCER: Working roll Created");
        }

        public void Start()
        {
            StartCoroutine(ProcessDiceRoll());
        }
        public void OnDestroy()
        {
            // Add your finalization code here. This is critical. If the sequence is cancelled and this
            // command is marked as "required", then only Awake() and OnDestroy() will be called.
            // Use it to clean up whatever needs cleaning at the end of the sequencer command.
            // If you don't need to do anything at the end, you can delete this method.
            Debug.Log("DESTROYSEQ: DestroyStart.");
            rollingActorName = string.Empty;
            variableName = string.Empty;
            variableSide = false;

            hasWorkingRollBegan = false;
            isWorkingRollFinished = false;
            Debug.Log("DESTROYSEQ: DestroyMid.");
            //workingRoll = new DiceRoll();

            workingRoll.isReady = false;
            workingRoll.rollerName = string.Empty;
            workingRoll.rollType = string.Empty;
            workingRoll.rollState = RollState.Unrolled;
            workingRoll.diceEval = new DiceEval();
            workingRoll.bonusesToRoll = new float[0];
            workingRoll.intendedDice = new Die[0];
            workingRoll.spawnedDice.Clear();
            workingRoll.finalRoll = 0;

            Debug.Log("DESTROYSEQ: DestroyFin.");
        }

        public DiceEval EvalByDisposition(string actorName)
        {
            int rollDC = 0;

            int fondnessNum = DialogueLua.GetActorField(actorName, "DispoFondness").asInt;
            int moodNum = DialogueLua.GetActorField(actorName, "DispoMood").asInt;

            Debug.Log($"EvalByDisposition -- Fondness {fondnessNum}, Mood {moodNum}");

            switch(fondnessNum)
            {
                case 2: //Friend
                    rollDC = 6;
                    break;
                case 1: //Like
                    rollDC = 10;
                    break;
                case 0: //Indifferent
                    rollDC = 12;
                    break;
                case -1: //Dislike
                    rollDC = 14;
                    break;
                case -2: //Hatred
                    rollDC = 18;
                    break;
            }

            switch(moodNum)
            {
                case 2: //Happy
                    rollDC -= 4;
                    break;
                case 1: //Good
                    rollDC -= 2;
                    break;
                case 0: //Neutral
                    rollDC += 0;
                    break;
                case -1: //Poor
                    rollDC += 4;
                    break;
                case -2: //Bad
                    rollDC += 6;
                    break;
            }

            return new DiceEval(rollDC+6, rollDC, rollDC-6);
        }

        public IEnumerator ProcessDiceRoll()
        {
            Debug.Log("ROLLPROCESS: ProcessDiceRoll Coroutine called.");

            rollRecord.ClearSpawnPoints();

            //Wait to ensure that working roll is fully initialized.
            while (workingRoll.isReady == false)
            {
                yield return null;
            }

            Debug.Log("ROLLPROCESS: WorkingRoll is Ready.");

            //Gets a unique spawn point for each die in the roll
            foreach (Die die in workingRoll.intendedDice)
            {
                rollRecord.SelectSpawnPoints();
            }

            Debug.Log("ROLLPROCESS: SpawnPoints got.");

            //Spawns each die in intendedDice at a selectedSpawnPoint, and adds it to spawnedDice
            for (int i = 0; i < workingRoll.intendedDice.Length; i++)
            {
                Die spawnedDie = Instantiate(workingRoll.intendedDice[i], rollRecord.selectedSpawnPoints[i].transform.position, Quaternion.identity);

                workingRoll.spawnedDice.Add(spawnedDie);
            }

            Debug.Log("ROLLPROCESS: Spawned Dice.");

            //Rolls each individual spawned die
            for (int i = 0; i < workingRoll.spawnedDice.Count; i++)
            {
                StartCoroutine(workingRoll.spawnedDice[i].RollSelf(this, rollRecord.selectedSpawnPoints[i].transform.forward, Random.Range(50f, 100f)));
            }

            workingRoll.rollState = RollState.Rolling;
            hasWorkingRollBegan = true;


            //Waits until roll is finished
            while (WorkingRollComplete == false)
            {
                yield return null;
            }

            Debug.Log("ROLLPROCESS: Dice finished rolling.");


            //Process roll result
            workingRoll.FinalRollValue();

            while(workingRoll.finalRoll == 0)
            {
                yield return null;
            }

            workingRoll.EvaluateDiceRoll();

            rollRecord.rolls.Add(workingRoll);

            int index = rollRecord.rolls.IndexOf(workingRoll);

            Debug.Log($"ROLLREPORT: {workingRoll.rollerName} rolled for {workingRoll.rollType}. Result: Final Roll of {workingRoll.finalRoll}, evaluating as a {workingRoll.rollState}.");

            DialogueLua.SetVariable("DiceRoll.operativeIndex", index);

            Stop();

        }

        public void CheckIfRollFinished(DiceRoll roll)
        {
            Debug.Log("ROLLPROCESS: RollCheck Message Received");

            bool isFinished = true;

            for(int i = 0; i < roll.spawnedDice.Count; i++)
            {
                if (roll.spawnedDice[i].dieRoll == true)
                {
                    isFinished = false; break;
                }
            }

            isWorkingRollFinished = isFinished;
        }

        //public void PrintFullDiceRoll(DiceRoll dice)
        //{
        //    Debug.Log(
        //        $"PRINTING DICE ROLL VARS \nIsReady = {dice.isReady}\nRollerName = {dice.rollerName}\nRollType = {dice.rollType}\nRollState = {dice.rollState}\nDiceEval = {dice.diceEval.critSuccess}/{dice.diceEval.pass}/{dice.diceEval.critFailure}\nFinalRoll = {dice.finalRoll}\n");
        //}
    }

}

