using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Preprocessor directives
/// </summary>
public class PD
{
     static bool showMbDebug = true;
    public static Action<string> delShowMb = null;

    public static void ShowMb(string v)
    {
        if (showMbDebug)
        {
            delShowMb(v);
        }
    }
}