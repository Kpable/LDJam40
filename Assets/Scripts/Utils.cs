using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils  {

    public static string ArrayOutput(this int[] array)
    {
        string output = "[";

        for (int i = 0; i < array.Length; i++)
        {
            output += array[i] + ((i == array.Length - 1) ? "" : ", ");
        }

        output += "]";

        return output;
    }
}
