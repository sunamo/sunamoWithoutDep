using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SqlServerHelperDebug
{
    static Type type = typeof(SqlServerHelperDebug);

    /// <summary>
    /// Musím zde předat přímo Dict a nezjištovat si počty řádků až zde - to bych musel referencovat SunamoSqlServer a nemohl bych tak používat SunamoDebugging in SunamoSqlServer
    /// 
    /// The same TextOutputGenerator in sunamo - 
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public static void RowsInTables(Dictionary<string, int> l)
    {
        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
    }
}
