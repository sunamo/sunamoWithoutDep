using MimeDetective;
using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SunamoMimeHelper
    {
    static Dictionary<string, List<byte>> my4 = new Dictionary<string, List<byte>>();

    public static void Init()
    {
        my4.Add("webp", new List<byte>(new byte[] { 82, 73, 70, 70 }));
    }

        public static string FileType(Byte[] b)
    {
        var f4 = b.Take(4);
        foreach (var item in my4)
        {
            if (f4.SequenceEqual(item.Value))
            {
                return item.Key;
            }
        }
        //var inspector = new FileFormatInspector();
        //var stream = new MemoryStream(b);
        //var format = inspector.DetermineFileFormat(stream);
        //return format.Extension;

        var ft = b.GetFileType();
        return ft.Extension;
    }



    }
