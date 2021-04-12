using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunamoExceptions { 
    public class CA
    {
        public static bool IsEqualToAnyElement<T>(T p, params T[] prvky)
        {
            return IsEqualToAnyElement(p, prvky.ToList());
        }

        public static bool IsEqualToAnyElement<T>(T p, IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                if (EqualityComparer<T>.Default.Equals(p, item))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> ToListString(params string[] v)
        {
            return new List<string>(v);
        }

        public static List<string> RemoveStringsEmpty2(List<string> mySites)
        {
            for (int i = mySites.Count - 1; i >= 0; i--)
            {
                if (mySites[i].Trim() == string.Empty)
                {
                    mySites.RemoveAt(i);
                }
            }
            return mySites;
        }

        public static int Count(IEnumerable e)
        {
            if (e == null)
            {
                return 0;
            }

            if (e is IList)
            {
                return (e as IList).Count;
            }

            if (e is Array)
            {
                return (e as Array).Length;
            }

            int count = 0;

            foreach (var item in e)
            {
                count++;
            }

            return count;
        }

        public static void RemoveDefaultT<T>(List<T> vr)
        {
            for (int i = vr.Count - 1; i >= 0; i--)
            {
                if (EqualityComparer<T>.Default.Equals(vr[i], default(T)))
                {
                    vr.RemoveAt(i);
                }
            }
        }

        #region ChangeContent for easy copy


        /// <summary>
        /// Direct edit
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        private static List<TResult> ChangeContent<T1, TResult>(List<T1> files_in, Func<T1, TResult> func)
        {
            List<TResult> result = new List<TResult>(files_in.Count);
            for (int i = 0; i < files_in.Count; i++)
            {
                result.Add(func.Invoke(files_in[i]));
            }
            return result;
        }

        /// <summary>
        /// TResult is the same type as T1 (output collection is the same generic as input)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        private static List<TResult> ChangeContent<T1, T2, TResult>(ChangeContentArgs a, Func<T1, T2, TResult> func, List<T1> files_in, T2 t2)
        {
            List<TResult> result = new List<TResult>(files_in.Count);
            for (int i = 0; i < files_in.Count; i++)
            {
                // Fully generic - no strict string can't return the same collection
                result.Add(func.Invoke(files_in[i], t2));
            }

            CA.RemoveDefaultT<TResult>(result);
            return result;
        }



        /// <summary>
        /// Direct edit
        /// If not every element fullfil pattern, is good to remove null (or values returned if cant be changed) from result
        /// </summary>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        public static List<string> ChangeContent(ChangeContentArgs a, List<string> files_in, Func<string, string> func)
        {
            for (int i = 0; i < files_in.Count; i++)
            {
                files_in[i] = func.Invoke(files_in[i]);
            }

            RemoveNullOrEmpty(a, files_in);

            return files_in;
        }

        /// <summary>
        /// Direct edit input collection
        /// </summary>
        /// <typeparam name="Arg1"></typeparam>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        public static List<string> ChangeContent<Arg1>(ChangeContentArgs a, List<string> files_in, Func<string, Arg1, string> func, Arg1 arg)
        {
            for (int i = 0; i < files_in.Count; i++)
            {
                files_in[i] = func.Invoke(files_in[i], arg);
            }

            RemoveNullOrEmpty(a, files_in);

            return files_in;
        }

        /// <summary>
        /// Direct edit
        /// </summary>
        /// <typeparam name="Arg1"></typeparam>
        /// <typeparam name="Arg2"></typeparam>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static List<string> ChangeContent<Arg1, Arg2>(ChangeContentArgs a, List<string> files_in, Func<string, Arg1, Arg2, string> func, Arg1 arg1, Arg2 arg2)
        {
            for (int i = 0; i < files_in.Count; i++)
            {
                files_in[i] = func.Invoke(files_in[i], arg1, arg2);
            }

            RemoveNullOrEmpty(a, files_in);

            return files_in;
        }

        /// <summary>
        /// Direct edit
        /// </summary>
        /// <param name="files_in"></param>
        /// <param name="func"></param>
        public static bool ChangeContent(ChangeContentArgs a, List<string> files_in, Predicate<string> predicate, Func<string, string> func)
        {
            bool changed = false;
            for (int i = 0; i < files_in.Count; i++)
            {
                if (predicate.Invoke(files_in[i]))
                {
                    files_in[i] = func.Invoke(files_in[i]);
                    changed = true;
                }
            }

            RemoveNullOrEmpty(a, files_in);

            return changed;
        }
        #endregion

        private static void RemoveNullOrEmpty(ChangeContentArgs a, List<string> files_in)
        {
            if (a != null)
            {
                if (a.removeNull)
                {
                    CA.RemoveDefaultT<string>(files_in);
                }

                if (a.removeEmpty)
                {
                    CA.RemoveStringsEmpty2(files_in);
                }
            }
        }
    }
}