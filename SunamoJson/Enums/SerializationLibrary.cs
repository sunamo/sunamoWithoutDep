using System;
using System.Collections.Generic;
using System.Text;


public enum SerializationLibrary
{
    /// <summary>
    /// Wont be implemented because MS has many json solutions
    /// </summary>
    Microsoft,
    Newtonsoft,
    SystemTextJson,
    JsonDanielCrenna,
    Utf8Json
}