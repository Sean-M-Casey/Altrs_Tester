using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using PixelCrushers.DialogueSystem;

public class QuickCharacterCreator : MonoBehaviour
{
    [SerializeField] private string policeworkDayholt;
    [SerializeField] private Stat_Approach stat_ApproachDayholt;
    [SerializeField] private Stat_Procedure stat_ProcedureDayholt;
    [SerializeField] private Stat_Attentiveness stat_AttentivenessDayholt;

    [SerializeField] private string interpersonalDayholt;
    [SerializeField] private Stat_Attitude stat_AttitudeDayholt;
    [SerializeField] private Stat_Impression stat_ImpressionDayholt;
    [SerializeField] private Stat_Understanding stat_UnderstandingDayholt;

    [SerializeField] private string interestsDayholt;
    [SerializeField] private Stat_Technology stat_TechnologyDayholt;
    [SerializeField] private Stat_Fitness stat_FitnessDayholt;
    [SerializeField] private Stat_Expertise stat_ExpertiseDayholt;

    [SerializeField] private string policeworkVaughn;
    [SerializeField] private Stat_Approach stat_ApproachVaughn;
    [SerializeField] private Stat_Procedure stat_ProcedureVaughn;
    [SerializeField] private Stat_Attentiveness stat_AttentivenessVaughn;

    [SerializeField] private string interpersonalVaughn;
    [SerializeField] private Stat_Attitude stat_AttitudeVaughn;
    [SerializeField] private Stat_Impression stat_ImpressionVaughn;
    [SerializeField] private Stat_Understanding stat_UnderstandingVaughn;

    [SerializeField] private string interestsVaughn;
    [SerializeField] private Stat_Technology stat_TechnologyVaughn;
    [SerializeField] private Stat_Fitness stat_FitnessVaughn;
    [SerializeField] private Stat_Expertise stat_ExpertiseVaughn;

    public string PoliceworkDAccessor { get { return policeworkDayholt; } set { policeworkDayholt = value; } }
    public Stat_Approach stat_ApproachDAccessor { get { return stat_ApproachDayholt; } set { stat_ApproachDayholt = value; } }
    public Stat_Procedure stat_ProcedureDAccessor { get { return stat_ProcedureDayholt; } set { stat_ProcedureDayholt = value; } }
    public Stat_Attentiveness stat_AttentivenessDAccessor { get { return stat_AttentivenessDayholt; } set { stat_AttentivenessDayholt = value; } }

    public string InterpersonalDAccessor { get { return interpersonalDayholt; } set { interpersonalDayholt = value; } }
    public Stat_Attitude stat_AttitudeDAccessor { get { return stat_AttitudeDayholt; } set { stat_AttitudeDayholt = value; } }
    public Stat_Impression stat_ImpressionDAccessor { get { return stat_ImpressionDayholt; } set { stat_ImpressionDayholt = value; } }
    public Stat_Understanding stat_UnderstandingDAccessor { get { return stat_UnderstandingDayholt; } set { stat_UnderstandingDayholt = value; } }

    public string InterestsDAccessor { get { return interestsDayholt; } set { interestsDayholt = value; } }
    public Stat_Technology stat_TechnologyDAccessor { get { return stat_TechnologyDayholt; } set { stat_TechnologyDayholt = value; } }
    public Stat_Fitness stat_FitnessDAccessor { get { return stat_FitnessDayholt; } set { stat_FitnessDayholt = value; } }
    public Stat_Expertise stat_ExpertiseDAccessor { get { return stat_ExpertiseDayholt; } set { stat_ExpertiseDayholt = value; } }


    public string PoliceworkVAccessor { get { return policeworkVaughn; } set { policeworkVaughn = value; } }
    public Stat_Approach stat_ApproachVAccessor { get { return stat_ApproachVaughn; } set { stat_ApproachVaughn = value; } }
    public Stat_Procedure stat_ProcedureVAccessor { get { return stat_ProcedureVaughn; } set { stat_ProcedureVaughn = value; } }
    public Stat_Attentiveness stat_AttentivenessVAccessor { get { return stat_AttentivenessVaughn; } set { stat_AttentivenessVaughn = value; } }

    public string InterpersonalVAccessor { get { return interpersonalVaughn; } set { interpersonalVaughn = value; } }
    public Stat_Attitude stat_AttitudeVAccessor { get { return stat_AttitudeVaughn; } set { stat_AttitudeVaughn = value; } }
    public Stat_Impression stat_ImpressionVAccessor { get { return stat_ImpressionVaughn; } set { stat_ImpressionVaughn = value; } }
    public Stat_Understanding stat_UnderstandingVAccessor { get { return stat_UnderstandingVaughn; } set { stat_UnderstandingVaughn = value; } }

    public string InterestsVAccessor { get { return interestsVaughn; } set { interestsVaughn = value; } }
    public Stat_Technology stat_TechnologyVAccessor { get { return stat_TechnologyVaughn; } set { stat_TechnologyVaughn = value; } }
    public Stat_Fitness stat_FitnessVAccessor { get { return stat_FitnessVaughn; } set { stat_FitnessVaughn = value; } }
    public Stat_Expertise stat_ExpertiseVAccessor { get { return stat_ExpertiseVaughn; } set { stat_ExpertiseVaughn = value; } }

    private CharacterCreatorNew characterCreator;

    private void Awake()
    {
        

        characterCreator = FindObjectOfType<CharacterCreatorNew>();

        policeworkDayholt = ((Alignment_Policework)DialogueLua.GetActorField("Dayholt", "PoliceworkAlignment").asInt).ToString();
        stat_ApproachDayholt = (Stat_Approach)DialogueLua.GetActorField("Dayholt", "Approach").asInt;
        stat_ProcedureDayholt = (Stat_Procedure)DialogueLua.GetActorField("Dayholt", "Procedure").asInt;
        stat_AttentivenessDayholt = (Stat_Attentiveness)DialogueLua.GetActorField("Dayholt", "Attentiveness").asInt;

        interpersonalDayholt = ((Alignment_Interpersonal)DialogueLua.GetActorField("Dayholt", "InterpersonalAlignment").asInt).ToString();
        stat_AttitudeDayholt = (Stat_Attitude)DialogueLua.GetActorField("Dayholt", "Attitude").asInt;
        stat_ImpressionDayholt = (Stat_Impression)DialogueLua.GetActorField("Dayholt", "Impression").asInt;
        stat_UnderstandingDayholt = (Stat_Understanding)DialogueLua.GetActorField("Dayholt", "Undersanding").asInt;

        interestsDayholt = ((Alignment_Interests)DialogueLua.GetActorField("Dayholt", "InterestsAlignment").asInt).ToString();
        stat_TechnologyDayholt = (Stat_Technology)DialogueLua.GetActorField("Dayholt", "Technology").asInt;
        stat_FitnessDayholt = (Stat_Fitness)DialogueLua.GetActorField("Dayholt", "Fitness").asInt;
        stat_ExpertiseDayholt = (Stat_Expertise)DialogueLua.GetActorField("Dayholt", "Expertise").asInt;

        policeworkVaughn = ((Alignment_Policework)DialogueLua.GetActorField("Vaughn", "PoliceworkAlignment").asInt).ToString();
        stat_ApproachVaughn = (Stat_Approach)DialogueLua.GetActorField("Vaughn", "Approach").asInt;
        stat_ProcedureVaughn = (Stat_Procedure)DialogueLua.GetActorField("Vaughn", "Procedure").asInt;
        stat_AttentivenessVaughn = (Stat_Attentiveness)DialogueLua.GetActorField("Vaughn", "Attentiveness").asInt;

        interpersonalVaughn = ((Alignment_Interpersonal)DialogueLua.GetActorField("Vaughn", "InterpersonalAlignment").asInt).ToString();
        stat_AttitudeVaughn = (Stat_Attitude)DialogueLua.GetActorField("Vaughn", "Attitude").asInt;
        stat_ImpressionVaughn = (Stat_Impression)DialogueLua.GetActorField("Vaughn", "Impression").asInt;
        stat_UnderstandingVaughn = (Stat_Understanding)DialogueLua.GetActorField("Vaughn", "Undersanding").asInt;

        interestsVaughn = ((Alignment_Interests)DialogueLua.GetActorField("Vaughn", "InterestsAlignment").asInt).ToString();
        stat_TechnologyVaughn = (Stat_Technology)DialogueLua.GetActorField("Vaughn", "Technology").asInt;
        stat_FitnessVaughn = (Stat_Fitness)DialogueLua.GetActorField("Vaughn", "Fitness").asInt;
        stat_ExpertiseVaughn = (Stat_Expertise)DialogueLua.GetActorField("Vaughn", "Expertise").asInt;
    }

    void GetPoliceworkStats()
    {
        policeworkDayholt = ((Alignment_Policework)DialogueLua.GetActorField("Dayholt", "PoliceworkAlignment").asInt).ToString();
        stat_ApproachDayholt = (Stat_Approach)DialogueLua.GetActorField("Dayholt", "Approach").asInt;
        stat_ProcedureDayholt = (Stat_Procedure)DialogueLua.GetActorField("Dayholt", "Procedure").asInt;
        stat_AttentivenessDayholt = (Stat_Attentiveness)DialogueLua.GetActorField("Dayholt", "Attentiveness").asInt;

        policeworkVaughn = ((Alignment_Policework)DialogueLua.GetActorField("Vaughn", "PoliceworkAlignment").asInt).ToString();
        stat_ApproachVaughn = (Stat_Approach)DialogueLua.GetActorField("Vaughn", "Approach").asInt;
        stat_ProcedureVaughn = (Stat_Procedure)DialogueLua.GetActorField("Vaughn", "Procedure").asInt;
        stat_AttentivenessVaughn = (Stat_Attentiveness)DialogueLua.GetActorField("Vaughn", "Attentiveness").asInt;
    }

    void GetInterpersonalStats()
    {
        interpersonalDayholt = ((Alignment_Interpersonal)DialogueLua.GetActorField("Dayholt", "InterpersonalAlignment").asInt).ToString();
        stat_AttitudeDayholt = (Stat_Attitude)DialogueLua.GetActorField("Dayholt", "Attitude").asInt;
        stat_ImpressionDayholt = (Stat_Impression)DialogueLua.GetActorField("Dayholt", "Impression").asInt;
        stat_UnderstandingDayholt = (Stat_Understanding)DialogueLua.GetActorField("Dayholt", "Undersanding").asInt;

        interpersonalVaughn = ((Alignment_Interpersonal)DialogueLua.GetActorField("Vaughn", "InterpersonalAlignment").asInt).ToString();
        stat_AttitudeVaughn = (Stat_Attitude)DialogueLua.GetActorField("Vaughn", "Attitude").asInt;
        stat_ImpressionVaughn = (Stat_Impression)DialogueLua.GetActorField("Vaughn", "Impression").asInt;
        stat_UnderstandingVaughn = (Stat_Understanding)DialogueLua.GetActorField("Vaughn", "Undersanding").asInt;
    }

    void GetInterestsStats()
    {
        interestsDayholt = ((Alignment_Interests)DialogueLua.GetActorField("Dayholt", "InterestsAlignment").asInt).ToString();
        stat_TechnologyDayholt = (Stat_Technology)DialogueLua.GetActorField("Dayholt", "Technology").asInt;
        stat_FitnessDayholt = (Stat_Fitness)DialogueLua.GetActorField("Dayholt", "Fitness").asInt;
        stat_ExpertiseDayholt = (Stat_Expertise)DialogueLua.GetActorField("Dayholt", "Expertise").asInt;
        Debug.Log(interestsDayholt);
        interestsVaughn = ((Alignment_Interests)DialogueLua.GetActorField("Vaughn", "InterestsAlignment").asInt).ToString();
        stat_TechnologyVaughn = (Stat_Technology)DialogueLua.GetActorField("Vaughn", "Technology").asInt;
        stat_FitnessVaughn = (Stat_Fitness)DialogueLua.GetActorField("Vaughn", "Fitness").asInt;
        stat_ExpertiseVaughn = (Stat_Expertise)DialogueLua.GetActorField("Vaughn", "Expertise").asInt;
    }

    public void SetPoliceworkStats()
    {
        if(characterCreator == null) characterCreator = FindObjectOfType<CharacterCreatorNew>();

        characterCreator.SetPlayerStatEnum(stat_ApproachDAccessor);
        characterCreator.SetPlayerStatEnum(stat_ProcedureDAccessor);
        characterCreator.SetPlayerStatEnum(stat_AttentivenessDAccessor);

        GetPoliceworkStats();
    }

    public void SetInterpersonalStats()
    {
        if (characterCreator == null) characterCreator = FindObjectOfType<CharacterCreatorNew>();

        characterCreator.SetPlayerStatEnum(stat_AttitudeDAccessor);
        characterCreator.SetPlayerStatEnum(stat_ImpressionDAccessor);
        characterCreator.SetPlayerStatEnum(stat_UnderstandingDAccessor);

        GetInterpersonalStats();
    }

    public void SetInterestsStats()
    {
        if (characterCreator == null) characterCreator = FindObjectOfType<CharacterCreatorNew>();

        characterCreator.SetPlayerStatEnum(stat_TechnologyDAccessor);
        characterCreator.SetPlayerStatEnum(stat_FitnessDAccessor);
        characterCreator.SetPlayerStatEnum(stat_ExpertiseDAccessor);

        GetInterestsStats();
    }
}
