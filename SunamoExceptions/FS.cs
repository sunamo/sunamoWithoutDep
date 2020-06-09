using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoExceptions
{
    public class FS
    {
        public static bool? ExistsDirectoryNull(string d)
        {
            return Directory.Exists(d);
        }

        public static List<string> GetFiles(string path, string v, SearchOption topDirectoryOnly)
        {
            return Directory.GetFiles(path, v, topDirectoryOnly).ToList();
        }

        public static string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }
    }
}