using System;
using System.Collections.Generic;
using System.Text;

public interface IJsSerializer
{
    //string Serialize(object o);
    object Deserialize(String o, Type targetType);
}

public interface IJsSerializer<T> : IJsSerializer
{
    string Serialize<T>(T o);
    //T Deserialize<T>(String o);
}