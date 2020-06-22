using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class GridDebug
{
    public static void PrintActualHeightOfRowDefinitions(Grid g)
    {
        foreach (var item in g.RowDefinitions)
        {
            Debug.WriteLine("rd ActualHeight: " + item.ActualHeight);
        }
    }

    public static void PrintActualWidthOfColumnDefinitions(Grid g)
    {
        foreach (var item in g.ColumnDefinitions)
        {
            Debug.WriteLine("rd ActualHeight: " + item.ActualWidth);
        }
    }

}