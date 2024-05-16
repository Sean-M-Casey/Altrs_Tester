
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandDiceRoll : SequencerCommand
    { 

        DiceRollRecord rollRecord;
        DiceRollDeterminer rollDeterminer;

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

        int determination = 0;
        bool hasDetermined = false;

        public DiceRoll WorkingRollAccess { get { return workingRoll; } }

        public bool WorkingRollComplete { get { return hasWorkingRollBegun == true && isWorkingRollFinished == true; } }


        public void Awake()
        {
            //Defining local variables for dice roll to be created
            string rollType;
            string rollTypeGroup;
            List<Die> diceTypes;

            if(rollRecord == null) rollRecord = FindObjectOfType<DiceRollRecord>();

            if (rollDeterminer == null) rollDeterminer = rollRecord.rollDeterminer.GetComponentInParent<DiceRollDeterminer>();

            variableName = GetParameter(0, string.Empty);

            Debug.Log($"NAMEBYIDACTOR: {GetCurrentEntryActor()}");
            Debug.Log($"NAMEBYIDCONVER: {GetCurrentEntryConversant()}");

            actorIndex = GetCurrentEntryActor();
            rollingActorName = DialogueLua.GetActorField(actorIndex, "Name").AsString;
            
            ParseRollType(variableName, out diceTypes, out rollType, out rollTypeGroup);

            #region CreatingDiceRoll

            if (rollTypeGroup == "Policework")
            {

            }
            else if (rollTypeGroup == "Interpersonal") //Creating Roll as Interpersonal
            {
                conversantIndex = GetCurrentEntryConversant();

                conversantName = DialogueLua.GetActorField(conversantIndex, "Name").AsString;

                diceEval = EvalByDisposition(conversantIndex);

                workingRoll = new DiceRoll(actorIndex, conversantIndex, rollType, RollType.Interpersonal, diceTypes.ToArray(), diceEval);
                workingRoll.isReady = true;

            }
            else if (rollTypeGroup == "Interests") //Creating roll as Interests
            {
                skillDC = GetParameterAsInt(1);

                diceEval = EvalBySkillDC(skillDC);

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
            StartCoroutine(PrepareDieBoardVisuals());
        }
        public void OnDestroy()
        {
            actorIndex = string.Empty;
            conversantIndex = string.Empty;
            
            rollingActorName = string.Empty;
            conversantName = string.Empty;
            variableName = string.Empty;
            skillDC = 10;

            hasWorkingRollBegun = false;
            isWorkingRollFinished = false;

            workingRoll.isReady = false;
            workingRoll.rollerIndex = string.Empty;
            workingRoll.conversantIndex = string.Empty;
            workingRoll.rollType = string.Empty;
            workingRoll.rollState = RollState.Unrolled;
            workingRoll.diceEval = new DiceEval();
            workingRoll.intendedDice = new Die[0];
            workingRoll.spawnedDice.Clear();
            workingRoll.finalRoll = 0;

        }


        public string GetCurrentEntryActor()
        {
            return DialogueManager.instance.initialDatabase.GetActor(DialogueManager.CurrentConversationState.subtitle.dialogueEntry.ActorID).Name;
        }

        public string GetCurrentEntryConversant()
        {
            return DialogueManager.instance.initialDatabase.GetActor(DialogueManager.CurrentConversationState.subtitle.dialogueEntry.ConversantID).Name;
        }

        /// <summary>Determines what dice to roll and the rollType/rollTypeGroup based on the input 'variable'.</summary>
        /// <param name="variable">Skill name as string; Used to determine what 'out variables' should be set to.</param>
        /// <param name="toRoll">Out List that dice should be added to.</param>
        /// <param name="rollType">Out string to set and hold display name of skill.</param>
        /// <param name="rollTypeGroup">Out string to set and hold skill group name.</param>
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

                    break;

                case "Strategic":

                    rollType = "Strategic";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Approach").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    break;

                case "Improvisor":

                    rollType = "Improvisor";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Procedure").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    break;

                case "Knowledge":

                    rollType = "Knowledge";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Procedure").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    break;

                case "EasilyDistracted":

                    rollType = "EasilyDistracted";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attentiveness").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    break;

                case "Perceptive":

                    rollType = "Perceptive";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attentiveness").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Policework";

                    break;

                case "Friendly":

                    rollType = "Friendly";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attitude").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "Cold":

                    rollType = "Cold";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Attitude").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "Trusting":

                    rollType = "Trusting";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Impression").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "Intimidating":

                    rollType = "Intimidating";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Interpersonal").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "Empathetic":

                    rollType = "Empathetic";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Understanding").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "Contrarian":

                    rollType = "Contrarian";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Understanding").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interpersonal";

                    break;

                case "MethodMan":

                    rollType = "MethodMan";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Technology").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                case "Integrated":

                    rollType = "Integrated";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Technology").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                case "Shredded":

                    rollType = "Shredded";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Fitness").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                case "Slender":

                    rollType = "Slender";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Fitness").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                case "WrenchMonkey":

                    rollType = "WrenchMonkey";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Expertise").AsInt == -1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                case "Coded":

                    rollType = "Coded";

                    toRoll.Add(rollRecord.D20);

                    if (DialogueLua.GetActorField(actorIndex, "Expertise").AsInt == 1)
                    {
                        toRoll.Add(rollRecord.D4);
                    }

                    rollTypeGroup = "Interests";

                    break;

                default:

                    Debug.LogError("SEQUENCER: Variable name given is invalid, or not usable.");

                    break;
            }

        }


        /// <summary>Creates Eval Object from conversant's disposition towards Player</summary>
        /// <param name="conversantIndex">Index of Conversant within Actor table.</param>
        /// <returns>DiceEval object</returns>
        public DiceEval EvalByDisposition(string conversantIndex)
        {

            int rollDC = 0;

            int fondnessNum = DialogueLua.GetActorField(conversantIndex, "DispoFondness").asInt;
            int moodNum = DialogueLua.GetActorField(conversantIndex, "DispoMood").asInt;


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

            Debug.Log($"DICEROLLDC: {conversantIndex}, {rollDC}");

            return new DiceEval(rollDC+6, rollDC, rollDC-6);
        }

        /// <summary>Creates Eval Object from a given skill DC.</summary>
        /// <param name="skillDC">Number needed to 'meet or beat' for roll to evaluate as a pass.</param>
        /// <returns>DiceEval object</returns>
        public DiceEval EvalBySkillDC(int skillDC)
        {
            return new DiceEval(skillDC + 6, skillDC, skillDC - 6);
        }

        /// <summary>Executes actions for showing DiceRoll visuals before roll execution.</summary>
        public IEnumerator PrepareDieBoardVisuals()
        {
            rollRecord.rollScreen.SetActive(true);

            yield return new WaitForEndOfFrame();

            if(DiceRollDeterminer.devDiceActive)
            {
                StartCoroutine(DetermineDiceRoll());
            }
            else
            {
                StartCoroutine(ProcessDiceRoll());
            }

        }

        /// <summary>Executes main DiceRoll functionality.</summary>
        public IEnumerator ProcessDiceRoll()
        {
            rollRecord.ClearSpawnPoints();

            //Wait to ensure that working roll is fully initialized.
            while (workingRoll.isReady == false)
            {
                yield return null;
            }

            //Gets a unique spawn point for each die in the roll
            foreach (Die die in workingRoll.intendedDice)
            {
                rollRecord.SelectSpawnPoints();
            }

            //Spawns each die in intendedDice at a selectedSpawnPoint, and adds it to spawnedDice
            for (int i = 0; i < workingRoll.intendedDice.Length; i++)
            {
                Die spawnedDie = Instantiate(workingRoll.intendedDice[i], rollRecord.selectedSpawnPoints[i].transform.position, Quaternion.identity);

                workingRoll.spawnedDice.Add(spawnedDie);
            }

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

            //Process roll result
            workingRoll.FinalRollValue();

            while(workingRoll.finalRoll == 0)
            {
                yield return null;
            }

            workingRoll.EvaluateDiceRoll();

            rollRecord.AddDiceRoll(workingRoll);

            int index = rollRecord.rolls.IndexOf(workingRoll);

            DialogueLua.SetVariable("DiceRoll.operativeIndex", index);

            workingRoll.LogRollDetails();
            workingRoll.CleanRoll();

            StartCoroutine(FinishRollVisuals());
        }

        public IEnumerator DetermineDiceRoll()
        {
            rollRecord.rollDeterminer.SetActive(true);

            rollDeterminer.critPassButton.onClick.AddListener(delegate { DetermineResult(2); });
            rollDeterminer.passButton.onClick.AddListener(delegate { DetermineResult(1); });
            rollDeterminer.failButton.onClick.AddListener(delegate { DetermineResult(-1); });
            rollDeterminer.critFailButton.onClick.AddListener(delegate { DetermineResult(-2); });

            while (hasDetermined == false)
            {
                yield return null;
            }

            workingRoll.rollState = RollState.Rolling;

            int rollValue;

            switch (determination)
            {
                case 2:

                    rollValue = workingRoll.diceEval.critSuccess;

                    break;

                case 1:

                    rollValue = workingRoll.diceEval.pass;

                    break;
                case -1:

                    rollValue = workingRoll.diceEval.pass - 1;

                    break;
                case -2:

                    rollValue = workingRoll.diceEval.critFailure - 1;

                    break;
                default:
                    rollValue = 0;

                    break;
            }

            workingRoll.FinalRollDetermination(rollValue);

            while (workingRoll.finalRoll == 0)
            {
                yield return null;
            }

            workingRoll.EvaluateDiceRoll();

            rollRecord.AddDiceRoll(workingRoll);

            int index = rollRecord.rolls.IndexOf(workingRoll);

            DialogueLua.SetVariable("DiceRoll.operativeIndex", index);

            workingRoll.LogRollDetails();
            workingRoll.CleanRoll();

            rollDeterminer.critPassButton.onClick.RemoveAllListeners();
            rollDeterminer.passButton.onClick.RemoveAllListeners();
            rollDeterminer.failButton.onClick.RemoveAllListeners();
            rollDeterminer.critFailButton.onClick.RemoveAllListeners();

            rollRecord.rollDeterminer.SetActive(false);

            hasDetermined = false;

            StartCoroutine(FinishRollVisuals());
        }

        /// <summary>Executes actions to finish visuals of newly finished DiceRoll</summary>
        public IEnumerator FinishRollVisuals() 
        {
            rollRecord.rollScreen.SetActive(false);

            yield return new WaitForSecondsRealtime(1);

            Stop();
        }


        public void CheckIfRollFinished(DiceRoll roll)
        {
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


        public void DetermineResult(int determine)
        {
            determination = determine;

            hasDetermined = true;
        }
    }

}

