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
        private static Type type = typeof(FS);

        public static bool? ExistsDirectoryNull(string d)
        {
            return Directory.Exists(d);
        }

        public static bool IsWindowsPathFormat(string argValue)
        {
            if (string.IsNullOrWhiteSpace(argValue))
            {
                return false;
            }

            bool badFormat = false;

            if (argValue.Length < 3)
            {
                return badFormat;
            }
            if (!char.IsLetter(argValue[0]))
            {
                badFormat = true;
            }



            if (char.IsLetter(argValue[1]))
            {
                badFormat = true;
            }

            if (argValue.Length > 2)
            {
                if (argValue[1] != '\\' && argValue[2] != '\\')
                {
                    badFormat = true;
                }
            }

            return !badFormat;
        }

        public static List<string> GetFiles(string path, string v, SearchOption topDirectoryOnly)
        {
            return FS.GetFilesMoreMasc(path, v, topDirectoryOnly).ToList();
        }

        public static List<string> GetFilesMoreMasc(string path, string v, SearchOption topDirectoryOnly)
        {
            var c = AllStrings.comma;
            var sc = AllStrings.sc;
            List<string> result = new List<string>();
            List<string> masks = new List<string>();

            if (v.Contains(c))
            {
                masks.AddRange(SH.Split(v, c));
            }
            else if (v.Contains(sc))
            {
                masks.AddRange(SH.Split(v, sc));
            }
            else
            {
                masks.Add(v);
            }

            foreach (var item in masks)
            {
                result.AddRange(Directory.GetFiles(path, item, topDirectoryOnly));
            }

            return result;
        }

        public static string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }

        public static void CreateUpfoldersPsysicallyUnlessThere(string nad)
        {
            CreateFoldersPsysicallyUnlessThere(FS.GetDirectoryName(nad));
        }

        /// <summary>
        /// Create all upfolders of A1 with, if they dont exist 
        /// </summary>
        /// <param name="nad"></param>
        public static void CreateFoldersPsysicallyUnlessThere(string nad)
        {
            ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(), type, "CreateFoldersPsysicallyUnlessThere", "nad", nad);
            ThrowExceptions.IsNotWindowsPathFormat(Exc.GetStackTrace(), type, Exc.CallingMethod(), "nad", nad);

            FS.MakeUncLongPath(ref nad);
            if (FS.ExistsDirectory(nad))
            {
                return;
            }
            else
            {
                List<string> slozkyKVytvoreni = new List<string>();
                slozkyKVytvoreni.Add(nad);

                while (true)
                {
                    nad = FS.GetDirectoryName(nad);

                    // TODO: Tady to nefunguje pro UWP/UAP apps protoze nemaji pristup k celemu disku. Zjistit co to je UWP/UAP/... a jak v nem ziskat/overit jakoukoliv slozku na disku
                    if (FS.ExistsDirectory(nad))
                    {
                        break;
                    }

                    string kopia = nad;
                    slozkyKVytvoreni.Add(kopia);
                }

                slozkyKVytvoreni.Reverse();
                foreach (string item in slozkyKVytvoreni)
                {
                    string folder = FS.MakeUncLongPath(item);
                    if (!FS.ExistsDirectory(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }
            }
        }

        #region MyRegion
        private static string GetDirectoryName(string nad)
        {
            return Path.GetDirectoryName(nad);
        }

        private static void MakeUncLongPath(ref string nad)
        {

        }

        public static string MakeUncLongPath(string path)
        {
            return path;
        }

        private static bool ExistsDirectory(string nad)
        {
            return Directory.Exists(nad);
        } 
        #endregion
    }
}