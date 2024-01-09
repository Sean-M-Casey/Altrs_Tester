
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandDiceRoll : SequencerCommand
    { 

        DiceRollRecord rollRecord;

        string actorIndex;
        string conversantIndex;

        string rollingActorName;
        string conversantName;
        string variableName;
        int skillDC = 10;

        DiceRoll workingRoll;
        DiceEval diceEval;

        bool hasWorkingRollBegun = false;
        bool isWorkingRollFinished = false;

        public DiceRoll WorkingRollAccess { get { return workingRoll; } }

        public bool WorkingRollComplete { get { return hasWorkingRollBegun == true && isWorkingRollFinished == true; } }


        public void Awake()
        {
            string rollType;
            string rollTypeGroup;
            List<Die> diceTypes;

            Debug.Log("SEQUENCER: Commencing DiceRoll");
            if(rollRecord == null) rollRecord = FindObjectOfType<DiceRollRecord>();

            variableName = GetParameter(0, string.Empty);
            Debug.Log($"SEQUENCER: VariableName is {((variableName != null) ? variableName : "doing the nully")}");

            actorIndex = Lua.Run("return Variable['ActorIndex']").AsString;

            rollingActorName = DialogueLua.GetActorField(actorIndex, "Name").AsString;
            Debug.Log($"SEQUENCER: RollingActorName is {((rollingActorName != null) ? rollingActorName : "doing the nully")}");

            ParseRollType(variableName, out diceTypes, out rollType, out rollTypeGroup);

            #region CreatingDiceRoll

            if (rollTypeGroup == "Policework")
            {

            }
            else if (rollTypeGroup == "Interpersonal")
            {
                conversantIndex = Lua.Run("return Variable['ConversantIndex']").AsString;

                conversantName = DialogueLua.GetActorField(conversantIndex, "Name").AsString;
                Debug.Log($"SEQUENCER: ContestingNPCName is {((conversantName != null) ? conversantName : "doing the nully")}");

                diceEval = EvalByDisposition(conversantIndex);
                Debug.Log($"SEQUENCER: DiceEval (by Disposition) has values of {diceEval.critSuccess}, {diceEval.pass}, {diceEval.critFailure}");

                workingRoll = new DiceRoll(actorIndex, conversantIndex, rollType, RollType.Interpersonal, diceTypes.ToArray(), diceEval);
                workingRoll.isReady = true;

            }
            else if (rollTypeGroup == "Interests")
            {
                skillDC = GetParameterAsInt(1);

                diceEval = EvalBySkillDC(skillDC);
                Debug.Log($"SEQUENCER: DiceEval (by SkillDC) has values of {diceEval.critSuccess}, {diceEval.pass}, {diceEval.critFailure}");

                workingRoll = new DiceRoll(actorIndex, rollType, RollType.Interests, diceTypes.ToArray(), diceEval);
                workingRoll.isReady = true;
            } 
            else 
            {
                
            }

            #endregion

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
            actorIndex = string.Empty;
            conversantIndex = string.Empty;
            
            rollingActorName = string.Empty;
            conversantName = string.Empty;
            variableName = string.Empty;
            skillDC = 10;

            hasWorkingRollBegun = false;
            isWorkingRollFinished = false;
            Debug.Log("DESTROYSEQ: DestroyMid.");
            //workingRoll = new DiceRoll();

            workingRoll.isReady = false;
            workingRoll.rollerIndex = string.Empty;
            workingRoll.conversantIndex = string.Empty;
            workingRoll.rollType = string.Empty;
            workingRoll.rollState = RollState.Unrolled;
            workingRoll.diceEval = new DiceEval();
            workingRoll.intendedDice = new Die[0];
            workingRoll.spawnedDice.Clear();
            workingRoll.finalRoll = 0;

            Debug.Log("DESTROYSEQ: DestroyFin.");
        }

        void ParseRollType(string variable, out List<Die> toRoll, out string rollType, out string rollTypeGroup)
        {
            toRoll = new List<Die>();

            rollType = string.Empty;

            rollTypeGroup = string.Empty;

            switch(variable)
            {
                case "GutFeeling":

                    rollType = "GutFeeling";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Approach").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                        
                    }
                    rollTypeGroup = "Policework";

                    //if (DialogueLua.GetVariable("PlayerStats.gutFeelingStrategic").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Strategic":

                    rollType = "Strategic";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Approach").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    //if (!DialogueLua.GetVariable("PlayerStats.gutFeelingStrategic").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Improvisor":

                    rollType = "Improvisor";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Procedure").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    //if (DialogueLua.GetVariable("PlayerStats.improvisorKnowledge").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Knowledge":

                    rollType = "Knowledge";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Procedure").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    //if (!DialogueLua.GetVariable("PlayerStats.improvisorKnowledge").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "EasilyDistracted":

                    rollType = "EasilyDistracted";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attentiveness").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    //if (DialogueLua.GetVariable("PlayerStats.distractPercept").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Perceptive":

                    rollType = "Perceptive";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attentiveness").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    //if (!DialogueLua.GetVariable("PlayerStats.distractPercept").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Friendly":

                    rollType = "Friendly";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attitude").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (DialogueLua.GetVariable("PlayerStats.friendlyCold").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Cold":

                    rollType = "Cold";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attitude").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (!DialogueLua.GetVariable("PlayerStats.friendlyCold").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Trusting":

                    rollType = "Trusting";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Impression").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (DialogueLua.GetVariable("PlayerStats.trustingIntimidating").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Intimidating":

                    rollType = "Intimidating";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Interpersonal").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (!DialogueLua.GetVariable("PlayerStats.trustingIntimidating").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Empathetic":

                    rollType = "Empathetic";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Understanding").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (DialogueLua.GetVariable("PlayerStats.empathContrarian").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Contrarian":

                    rollType = "Contrarian";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Understanding").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";


                    //if (!DialogueLua.GetVariable("PlayerStats.empathContrarian").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "MethodMan":

                    rollType = "MethodMan";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Technology").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (DialogueLua.GetVariable("PlayerStats.methodManIntegrated").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Integrated":

                    rollType = "Integrated";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Technology").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (!DialogueLua.GetVariable("PlayerStats.methodManIntegrated").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Shredded":

                    rollType = "Shredded";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Fitness").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (DialogueLua.GetVariable("PlayerStats.shreddedSlender").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Slender":

                    rollType = "Slender";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Fitness").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (!DialogueLua.GetVariable("PlayerStats.shreddedSlender").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "WrenchMonkey":

                    rollType = "WrenchMonkey";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Expertise").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (DialogueLua.GetVariable("PlayerStats.wrenchMonkeyCoded").asBool) toRoll.Add(rollRecord.D4);

                    break;

                case "Coded":

                    rollType = "Coded";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Expertise").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";


                    //if (!DialogueLua.GetVariable("PlayerStats.wrenchMonkeyCoded").asBool) toRoll.Add(rollRecord.D4);

                    break;

                default:

                    Debug.LogError("SEQUENCER: Variable name given is invalid, or not usable.");

                    break;
            }

            Debug.Log($"SEQUENCER: RollType is now set to \"{rollType}\", in group \"{rollTypeGroup}\". Rolled by {DialogueLua.GetActorField(actorIndex, "Name").AsString}");
        }

        public DiceEval EvalByDisposition(string conversantIndex)
        {
            int rollDC = 0;

            int fondnessNum = DialogueLua.GetActorField(conversantIndex, "DispoFondness").asInt;
            int moodNum = DialogueLua.GetActorField(conversantIndex, "DispoMood").asInt;

            Debug.Log($"SEQUENCER: EvalByDisposition -- Fondness {fondnessNum}, Mood {moodNum}");

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

        public DiceEval EvalBySkillDC(int skillDC)
        {
            return new DiceEval(skillDC + 6, skillDC, skillDC - 6);
        }

        public IEnumerator PrepareDieBoardVisuals()
        {
            yield return null;
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
            hasWorkingRollBegun = true;


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

            Debug.Log($"ROLLREPORT: {workingRoll.rollerIndex} rolled for {workingRoll.rollType}. Result: Final Roll of {workingRoll.finalRoll}, evaluating as a {workingRoll.rollState}.");

            DialogueLua.SetVariable("DiceRoll.operativeIndex", index);

            Stop();

        }

        public IEnumerator FinaliseDiceRollVisuals()
        {
            yield return null;
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
    }

}

