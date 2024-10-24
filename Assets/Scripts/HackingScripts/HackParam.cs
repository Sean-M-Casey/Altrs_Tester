using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem.Articy.Articy_1_4;

[Serializable]
public class HackParam
{
    public string name;

    public HackParamTypes type;

    [SerializeField] private string value;

    public string ValueAsString { get { return value; } }
    public int ValueAsInt { get { return int.Parse(value); } }
    public float ValueAsFloat { get { return float.Parse(value); } }
    public Vector3 ValueAsVector3 { get { return StaticMethods.StringToVector3(value); } }


}

[Serializable]
public class HackParamValue
{
    public Type type;

    public virtual string GetValueAsString()
    {
        return "Valueless";
    }
}

[Serializable]
public class HackParamValue<T> : HackParamValue
{
    public T value;

    public HackParamValue(T value)
    {
        this.value = value;

        type = typeof(T);
    }

    public override string GetValueAsString()
    {
        return value.ToString();
    }
}

//[Serializable]
//public class HackParamIntValue : HackParamValue
//{
//    [SerializeField] public int value;

//    public HackParamIntValue()
//    {
//        value = 0;
//    }
//}

//[Serializable]
//public class HackParamFloatValue : HackParamValue
//{
//    [SerializeField] public float value;

//    public HackParamFloatValue()
//    {
//        value = 0f;
//    }
//}

//[Serializable]
//public class HackParamVector3Value : HackParamValue
//{
//    [SerializeField] public Vector3 value;

//    public HackParamVector3Value()
//    {
//        value = Vector3.zero;
//    }
//}
