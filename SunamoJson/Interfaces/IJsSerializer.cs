using System;
using System.Collections.Generic;
using System.Text;

public interface IJsSerializer
{
    //string Serialize(object o);
    object Deserialize(String o, Type targetType);
    string Serialize<T>(T o);
}

public interface IJsSerializer<T> : IJsSerializer
{
    
    //T Deserialize<T>(String o);
}