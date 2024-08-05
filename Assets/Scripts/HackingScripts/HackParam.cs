using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HackParam
{
    public string name;

    public HackParamTypes type;

    [SerializeField] private string valueAsString;
    [SerializeField] private int valueAsInt;
    [SerializeField] private float valueAsFloat;
    [SerializeField] private Vector3 valueAsVector3;

    public HackParamValue GetValue()
    {
        HackParamValue paramValue = new HackParamValue();


        switch(type)
        {
            case HackParamTypes.String:

                paramValue = new HackParamValue<string>(valueAsString);


                break;

            case HackParamTypes.Int:

                paramValue = new HackParamValue<int>(valueAsInt);


                break;

            case HackParamTypes.Float:

                paramValue = new HackParamValue<float>(valueAsFloat);


                break;

            case HackParamTypes.Vector3:

                paramValue = new HackParamValue<Vector3>(valueAsVector3);


                break;

        }

        return paramValue;
    }
}

[Serializable]
public class HackParamValue
{
    
}

[Serializable]
public class HackParamValue<T> : HackParamValue
{
    public T value;

    public HackParamValue(T value)
    {
        this.value = value;
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
