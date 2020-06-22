using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class FrameworkElementDebug
{
    public static void ActualSize(FrameworkElement fe)
    {
        Debug.WriteLine($"{fe.Name} ActualHeight: {fe.ActualHeight}");
        Debug.WriteLine($"{fe.Name} ActualWidth: {fe.ActualWidth}");
    }
}