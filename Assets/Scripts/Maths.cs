using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths
{
    public static float Remap(float x, float A, float B, float C, float D)
    {
        float remappedValue = C + (x - A) / (B - A) * (D - C);
        return remappedValue;
    }
}