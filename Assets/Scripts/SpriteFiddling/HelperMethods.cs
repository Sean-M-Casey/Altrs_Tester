using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{
    public static void ClampAngle(ref Vector3 eulerAngle)
    {
        if (eulerAngle.x < -180f) eulerAngle.x += 360f;
        else if (eulerAngle.x > 180f) eulerAngle.x -= 360f;
        if (eulerAngle.y < -180f) eulerAngle.y += 360f;
        else if (eulerAngle.y > 180f) eulerAngle.y -= 360f;
        if (eulerAngle.z < -180f) eulerAngle.z += 360f;
        else if (eulerAngle.z > 180f) eulerAngle.z -= 360f;
    }

    public static void FindOrientation(Vector2 rotation, ref Orientation orientation)
    {
        float rotationY = rotation.y;
        float absRotY = Mathf.Abs(rotationY);

        if (absRotY < 22.5f)
        {
            orientation = Orientation.North;
        }
        else if (absRotY < 67.5f)
        {
            orientation = rotationY < 0 ? Orientation.NorthWest : Orientation.NorthEast;
        }
        else if (absRotY < 112.5f)
        {
            orientation = rotationY < 0 ? Orientation.West : Orientation.East;
        }
        else if (absRotY < 157.5f)
        {
            orientation = rotationY < 0 ? Orientation.SouthWest : Orientation.SouthEast;
        }
        else
        {
            orientation = Orientation.South;
        }
    }

    public static float FindDistToHeightRatio(Vector3 spriteObjPos, Vector3 cameraPos)
    {
        
        Vector3 spriteFlatPos = new Vector3(spriteObjPos.x, 0, spriteObjPos.z);
        Vector3 camFlatPos = new Vector3(cameraPos.x, 0, cameraPos.z);

        float distanceDiff = Vector3.Distance(spriteFlatPos, camFlatPos);
        float distanceDiffRound = RoundToPlace(distanceDiff, 10);

        float heightDiff = cameraPos.y - spriteObjPos.y;
        float heightDiffRound = RoundToPlace(heightDiff, 10);

        //Debug.Log($"Planar Distance is: {distanceDiff}");
        //Debug.Log($"Rounded Planar Distance is: {distanceDiffRound}");
        //Debug.Log($"Height Difference is: {heightDiff}");
        //Debug.Log($"Round Height Difference is: {heightDiffRound}");

        float ratio = RoundToPlace(distanceDiffRound / heightDiffRound, 100);

        Debug.Log($"Distance/Height Ratio is: {ratio}");

        return ratio;

    }

    /// <summary>
    /// Rounds a float value to given place.
    /// </summary>
    /// <param name="toRound">Float to round</param>
    /// <param name="placeAsPos">Number of Places to round to, based on position. Example, rounding to the hundredth (0.00), would be represented as 100, the thousandth (0.000) as 1000, and so on.</param>
    /// <returns>Rounded float.</returns>
    public static float RoundToPlace(float toRound, int placeAsPos)
    {
        float roundedFloat = Mathf.Round(toRound * placeAsPos) / placeAsPos;

        return roundedFloat;
    }
}
