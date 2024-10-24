using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMethods
{
    public static Vector3 StringToVector3(string vectorString)
    {
        if(vectorString.StartsWith("(") && vectorString.EndsWith(")"))
        {
            vectorString = vectorString.Substring(1, vectorString.Length - 2);
        }

        string[] array = vectorString.Split(',');

        Vector3 vectorOut = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));

        return vectorOut;
    }
}
