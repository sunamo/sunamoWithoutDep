using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SunamoExceptions
{
    public class TF
    {
        public static string ReadAllText(string filename)
        {
            return File.ReadAllText(filename);
        }

        public static List<byte> ReadAllBytes(string v)
        {
            return File.ReadAllBytes(v).ToList();
        }
    }
}