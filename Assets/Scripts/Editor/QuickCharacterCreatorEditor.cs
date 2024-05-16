using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;

[CustomEditor(typeof(QuickCharacterCreator))]
public class QuickCharacterCreatorEditor : Editor
{
    public VisualTreeAsset inspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement visualElement = new VisualElement();

        inspectorXML.CloneTree(visualElement);

        #region ENUMFIELDS

        //TextField alignPoliceworkDField = (TextField)visualElement.Q("DayholtPolicework").Q("AlignmentField").Q("FieldEnum");
        EnumField approachDField = (EnumField)visualElement.Q("DayholtPolicework").Q("ApproachField").Q("FieldEnum");
        EnumField procedureDField = (EnumField)visualElement.Q("DayholtPolicework").Q("ProcedureField").Q("FieldEnum");
        EnumField attentivenessDField = (EnumField)visualElement.Q("DayholtPolicework").Q("AttentivenessField").Q("FieldEnum");

        //TextField alignInterpersonalDField = (TextField)visualElement.Q("DayholtInterpersonal").Q("AlignmentField").Q("FieldEnum");
        EnumField attitudeDField = (EnumField)visualElement.Q("DayholtInterpersonal").Q("AttitudeField").Q("FieldEnum");
        EnumField impressionDField = (EnumField)visualElement.Q("DayholtInterpersonal").Q("ImpressionField").Q("FieldEnum");
        EnumField understandingDField = (EnumField)visualElement.Q("DayholtInterpersonal").Q("UnderstandingField").Q("FieldEnum");

        //TextField alignInterestsDField = (TextField)visualElement.Q("DayholtInterests").Q("AlignmentField").Q("FieldEnum");
        EnumField technologyDField = (EnumField)visualElement.Q("DayholtInterests").Q("TechnologyField").Q("FieldEnum");
        EnumField fitnessDField = (EnumField)visualElement.Q("DayholtInterests").Q("FitnessField").Q("FieldEnum");
        EnumField expertiseDField = (EnumField)visualElement.Q("DayholtInterests").Q("ExpertiseField").Q("FieldEnum");

        //TextField alignPoliceworkVField = (TextField)visualElement.Q("VaughnPolicework").Q("AlignmentField").Q("FieldEnum");
        EnumField approachVField = (EnumField)visualElement.Q("VaughnPolicework").Q("ApproachField").Q("FieldEnum");
        EnumField procedureVField = (EnumField)visualElement.Q("VaughnPolicework").Q("ProcedureField").Q("FieldEnum");
        EnumField attentivenessVField = (EnumField)visualElement.Q("VaughnPolicework").Q("AttentivenessField").Q("FieldEnum");

        //TextField alignInterpersonalVField = (TextField)visualElement.Q("VaughnInterpersonal").Q("AlignmentField").Q("FieldEnum");
        EnumField attitudeVField = (EnumField)visualElement.Q("VaughnInterpersonal").Q("AttitudeField").Q("FieldEnum");
        EnumField impressionVField = (EnumField)visualElement.Q("VaughnInterpersonal").Q("ImpressionField").Q("FieldEnum");
        EnumField understandingVField = (EnumField)visualElement.Q("VaughnInterpersonal").Q("UnderstandingField").Q("FieldEnum");

        //TextField alignInterestsVField = (TextField)visualElement.Q("VaughnInterests").Q("AlignmentField").Q("FieldEnum");
        EnumField technologyVField = (EnumField)visualElement.Q("VaughnInterests").Q("TechnologyField").Q("FieldEnum");
        EnumField fitnessVField = (EnumField)visualElement.Q("VaughnInterests").Q("FitnessField").Q("FieldEnum");
        EnumField expertiseVField = (EnumField)visualElement.Q("VaughnInterests").Q("ExpertiseField").Q("FieldEnum");

        approachDField.RegisterValueChangedCallback(OnApproachDEnumChange);
        procedureDField.RegisterValueChangedCallback(OnProcedureDEnumChange);
        attentivenessDField.RegisterValueChangedCallback(OnAttentivenessDEnumChange);

        attitudeDField.RegisterValueChangedCallback(OnAttitudeDEnumChange);
        impressionDField.RegisterValueChangedCallback(OnImpressionDEnumChange);
        understandingDField.RegisterValueChangedCallback(OnUnderstandingDEnumChange);

        technologyDField.RegisterValueChangedCallback(OnTechnologyDEnumChange);
        fitnessDField.RegisterValueChangedCallback(OnFitnessDEnumChange);
        expertiseDField.RegisterValueChangedCallback(OnExpertiseDEnumChange);

        approachVField.RegisterValueChangedCallback(OnApproachVEnumChange);
        procedureVField.RegisterValueChangedCallback(OnProcedureVEnumChange);
        attentivenessVField.RegisterValueChangedCallback(OnAttentivenessVEnumChange);

        attitudeVField.RegisterValueChangedCallback(OnAttitudeVEnumChange);
        impressionVField.RegisterValueChangedCallback(OnImpressionVEnumChange);
        understandingVField.RegisterValueChangedCallback(OnUnderstandingVEnumChange);

        technologyVField.RegisterValueChangedCallback(OnTechnologyVEnumChange);
        fitnessVField.RegisterValueChangedCallback(OnFitnessVEnumChange);
        expertiseVField.RegisterValueChangedCallback(OnExpertiseVEnumChange);

        #endregion



        return visualElement;
    }


    void OnApproachDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch(evt.newValue)
        {
            case Stat_Approach.GutFeeling:
                
                if(target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor != Stat_Approach.Strategic) target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor = Stat_Approach.Strategic;
                break;

            case Stat_Approach.Unassigned:
                if (target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor != Stat_Approach.Unassigned)
                    target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor = Stat_Approach.Unassigned;
                break;

            case Stat_Approach.Strategic:

                if (target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor != Stat_Approach.GutFeeling)
                    target.GetComponent<QuickCharacterCreator>().stat_ApproachVAccessor = Stat_Approach.GutFeeling;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnProcedureDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Procedure.Improvisor:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor != Stat_Procedure.Knowledge) target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor = Stat_Procedure.Knowledge;
                break;

            case Stat_Procedure.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor != Stat_Procedure.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor = Stat_Procedure.Unassigned;
                break;

            case Stat_Procedure.Knowledge:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor != Stat_Procedure.Improvisor) target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor = Stat_Procedure.Improvisor;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnAttentivenessDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Attentiveness.EasilyDistracted:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor != Stat_Attentiveness.Perceptive) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor = Stat_Attentiveness.Perceptive;
                break;

            case Stat_Attentiveness.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor != Stat_Attentiveness.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor = Stat_Attentiveness.Unassigned;
                break;

            case Stat_Attentiveness.Perceptive:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor != Stat_Attentiveness.EasilyDistracted) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessVAccessor = Stat_Attentiveness.EasilyDistracted;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnAttitudeDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Attitude.Friendly:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor != Stat_Attitude.Cold) target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor = Stat_Attitude.Cold;
                break;

            case Stat_Attitude.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor != Stat_Attitude.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor = Stat_Attitude.Unassigned;
                break;

            case Stat_Attitude.Cold:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor != Stat_Attitude.Friendly) target.GetComponent<QuickCharacterCreator>().stat_AttitudeVAccessor = Stat_Attitude.Friendly;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnImpressionDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Impression.Trusting:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor != Stat_Impression.Intimidating) target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor = Stat_Impression.Intimidating;
                break;

            case Stat_Impression.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor != Stat_Impression.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor = Stat_Impression.Unassigned;
                break;

            case Stat_Impression.Intimidating:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor != Stat_Impression.Trusting) target.GetComponent<QuickCharacterCreator>().stat_ImpressionVAccessor = Stat_Impression.Trusting;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnUnderstandingDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Understanding.Empathetic:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor != Stat_Understanding.Contrarian) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor = Stat_Understanding.Contrarian;
                break;

            case Stat_Understanding.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor != Stat_Understanding.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor = Stat_Understanding.Unassigned;
                break;

            case Stat_Understanding.Contrarian:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor != Stat_Understanding.Empathetic) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingVAccessor = Stat_Understanding.Empathetic;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnTechnologyDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Technology.MethodMan:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor != Stat_Technology.Integrated) target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor = Stat_Technology.Integrated;
                break;

            case Stat_Technology.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor != Stat_Technology.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor = Stat_Technology.Unassigned;
                break;

            case Stat_Technology.Integrated:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor != Stat_Technology.MethodMan) target.GetComponent<QuickCharacterCreator>().stat_TechnologyVAccessor = Stat_Technology.MethodMan;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }

    void OnFitnessDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Fitness.Shredded:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor != Stat_Fitness.Slender) target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor = Stat_Fitness.Slender;
                break;

            case Stat_Fitness.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor != Stat_Fitness.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor = Stat_Fitness.Unassigned;
                break;

            case Stat_Fitness.Slender:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor != Stat_Fitness.Shredded) target.GetComponent<QuickCharacterCreator>().stat_FitnessVAccessor = Stat_Fitness.Shredded;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }

    void OnExpertiseDEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Expertise.WrenchMonkey:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != Stat_Expertise.NotApplicable) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = Stat_Expertise.NotApplicable;
                break;

            case Stat_Expertise.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != Stat_Expertise.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = Stat_Expertise.Unassigned;
                break;

            case Stat_Expertise.Coded:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != Stat_Expertise.NotApplicable) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = Stat_Expertise.NotApplicable;
                break;

            case Stat_Expertise.NotApplicable:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor != (Stat_Expertise)evt.previousValue) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor = (Stat_Expertise)evt.previousValue;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }

    void OnApproachVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Approach.GutFeeling:

                if (target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor != Stat_Approach.Strategic) target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor = Stat_Approach.Strategic;
                break;

            case Stat_Approach.Unassigned:
                if (target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor != Stat_Approach.Unassigned)
                    target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor = Stat_Approach.Unassigned;
                break;

            case Stat_Approach.Strategic:

                if (target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor != Stat_Approach.GutFeeling)
                    target.GetComponent<QuickCharacterCreator>().stat_ApproachDAccessor = Stat_Approach.GutFeeling;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnProcedureVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Procedure.Improvisor:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureDAccessor != Stat_Procedure.Knowledge) target.GetComponent<QuickCharacterCreator>().stat_ProcedureDAccessor = Stat_Procedure.Knowledge;
                break;

            case Stat_Procedure.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureDAccessor != Stat_Procedure.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ProcedureDAccessor = Stat_Procedure.Unassigned;
                break;

            case Stat_Procedure.Knowledge:

                if (target.GetComponent<QuickCharacterCreator>().stat_ProcedureDAccessor != Stat_Procedure.Improvisor) target.GetComponent<QuickCharacterCreator>().stat_ProcedureVAccessor = Stat_Procedure.Improvisor;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnAttentivenessVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Attentiveness.EasilyDistracted:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor != Stat_Attentiveness.Perceptive) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor = Stat_Attentiveness.Perceptive;
                break;

            case Stat_Attentiveness.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor != Stat_Attentiveness.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor = Stat_Attentiveness.Unassigned;
                break;

            case Stat_Attentiveness.Perceptive:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor != Stat_Attentiveness.EasilyDistracted) target.GetComponent<QuickCharacterCreator>().stat_AttentivenessDAccessor = Stat_Attentiveness.EasilyDistracted;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetPoliceworkStats();
    }

    void OnAttitudeVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Attitude.Friendly:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor != Stat_Attitude.Cold) target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor = Stat_Attitude.Cold;
                break;

            case Stat_Attitude.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor != Stat_Attitude.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor = Stat_Attitude.Unassigned;
                break;

            case Stat_Attitude.Cold:

                if (target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor != Stat_Attitude.Friendly) target.GetComponent<QuickCharacterCreator>().stat_AttitudeDAccessor = Stat_Attitude.Friendly;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnImpressionVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Impression.Trusting:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor != Stat_Impression.Intimidating) target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor = Stat_Impression.Intimidating;
                break;

            case Stat_Impression.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor != Stat_Impression.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor = Stat_Impression.Unassigned;
                break;

            case Stat_Impression.Intimidating:

                if (target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor != Stat_Impression.Trusting) target.GetComponent<QuickCharacterCreator>().stat_ImpressionDAccessor = Stat_Impression.Trusting;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnUnderstandingVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Understanding.Empathetic:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor != Stat_Understanding.Contrarian) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor = Stat_Understanding.Contrarian;
                break;

            case Stat_Understanding.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor != Stat_Understanding.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor = Stat_Understanding.Unassigned;
                break;

            case Stat_Understanding.Contrarian:

                if (target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor != Stat_Understanding.Empathetic) target.GetComponent<QuickCharacterCreator>().stat_UnderstandingDAccessor = Stat_Understanding.Empathetic;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterpersonalStats();
    }

    void OnTechnologyVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Technology.MethodMan:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor != Stat_Technology.Integrated) target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor = Stat_Technology.Integrated;
                break;

            case Stat_Technology.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor != Stat_Technology.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor = Stat_Technology.Unassigned;
                break;

            case Stat_Technology.Integrated:

                if (target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor != Stat_Technology.MethodMan) target.GetComponent<QuickCharacterCreator>().stat_TechnologyDAccessor = Stat_Technology.MethodMan;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }

    void OnFitnessVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Fitness.Shredded:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor != Stat_Fitness.Slender) target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor = Stat_Fitness.Slender;
                break;

            case Stat_Fitness.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor != Stat_Fitness.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor = Stat_Fitness.Unassigned;
                break;

            case Stat_Fitness.Slender:

                if (target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor != Stat_Fitness.Shredded) target.GetComponent<QuickCharacterCreator>().stat_FitnessDAccessor = Stat_Fitness.Shredded;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }

    void OnExpertiseVEnumChange(ChangeEvent<Enum> evt)
    {
        Debug.Log($"Toggle changed. Old value: {evt.previousValue}, new value: {evt.newValue}");

        switch (evt.newValue)
        {
            case Stat_Expertise.WrenchMonkey:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != Stat_Expertise.NotApplicable) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = Stat_Expertise.NotApplicable;

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor != Stat_Expertise.Coded) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor = Stat_Expertise.Coded;
                break;

            case Stat_Expertise.Unassigned:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor != Stat_Expertise.Unassigned) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor = Stat_Expertise.Unassigned;
                break;

            case Stat_Expertise.Coded:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != Stat_Expertise.NotApplicable) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = Stat_Expertise.NotApplicable;

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor != Stat_Expertise.WrenchMonkey) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseDAccessor = Stat_Expertise.WrenchMonkey;
                break;

            case Stat_Expertise.NotApplicable:

                if (target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor != (Stat_Expertise)evt.previousValue) target.GetComponent<QuickCharacterCreator>().stat_ExpertiseVAccessor = (Stat_Expertise)evt.previousValue;
                break;
        }

        target.GetComponent<QuickCharacterCreator>().SetInterestsStats();
    }
}
