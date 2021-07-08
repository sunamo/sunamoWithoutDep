using System;
using System.Collections.Generic;
using System.Text;

public class EnumHelperVcf
{
    public static T Parse<T>(object i)
    {
        return (T)Enum.Parse(typeof(T), i.ToString());
    }
}
