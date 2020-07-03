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
        Debug.WriteLine($"{fe.Name} DesiredSize: {fe.DesiredSize}");
        Debug.WriteLine($"{fe.Name} RenderSize: {fe.RenderSize}");
        Debug.WriteLine($"{fe.Name} Height: {fe.Height}");
        Debug.WriteLine($"{fe.Name} Width: {fe.Width}");
        Debug.WriteLine($"{fe.Name} MaxHeight: {fe.MaxHeight}");
        Debug.WriteLine($"{fe.Name} MaxWidth: {fe.MaxWidth}");
        Debug.WriteLine($"{fe.Name} MinHeight: {fe.MinHeight}");
        Debug.WriteLine($"{fe.Name} MinWidth: {fe.MinWidth}");

    }
}