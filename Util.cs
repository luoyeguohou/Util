using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

class Util
{
    public static T Find<T>(List<T> list, T item) where T : class
    {
        foreach (T t in list)
        {
            if (t.Equals(item)) return t;
        }
        return null;
    }

    public delegate bool FindHandler<T>(T t) where T : class;
    public static T Find<T>(List<T> list, FindHandler<T> handler) where T : class
    {
        foreach (T t in list)
        {
            if (handler(t)) return t;
        }
        return null;
    }

    public static List<T2> Map<T1,T2>(List<T1> list, Func<T1,T2> handler)
    {
        List<T2> ret = new();
        foreach (T1 t in list)
        {
            ret.Add(handler(t));
        }
        return ret;
    }


    public static void RemoveValue<T>(List<T> lst, T val) where T : IEquatable<T>
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].Equals(val))
            {
                lst.RemoveAt(i);
                return;
            }
        }
    }

    public static void Shuffle<T>(T[] ary, Random rng)
    {
        int n = ary.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = ary[k];
            ary[k] = ary[n];
            ary[n] = value;
        }
    }

    public static void Shuffle<T>(List<T> list, Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T BiggerOne<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    public static T SmallOne<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? b : a;
    }

    public static bool Any<T>(List<T> lst, Func<T, bool> action)
    {
        foreach (T t in lst)
        {
            if (action(t))
            {
                return true;
            }
        }
        return false;
    }

    public static bool All<T>(List<T> lst, Func<T, bool> action)
    {
        foreach (T t in lst)
        {
            if (!action(t))
            {
                return false;
            }
        }
        return true;
    }

    public static List<T> Filter<T>(List<T> lst, Func<T, bool> action, int limitNum = -1)
    {
        List<T> ret = new List<T>();
        foreach (T t in lst)
        {
            if (action(t))
            {
                ret.Add(t);
                if (limitNum != -1 && ret.Count >= limitNum)
                {

                    break;
                }
            }
        }
        return ret;
    }

    public static int Count<T>(List<T> lst, Func<T, bool> action)
    {
        int cnt = 0;
        foreach (T t in lst)
        {
            if (action(t))
            {
                cnt++;
            }
        }
        return cnt;
    }

    public static int SetBit(int num, int pos)
    {
        return num | (1 << pos);
    }

    public static int GetMax(int[] vals)
    {
        int[] des = new int[vals.Length];
        Array.Copy(vals, des, vals.Length);
        Array.Sort(des);
        return des[des.Length-1];
    }

    public static int GetSecondMax(int[] vals)
    {
        if (vals.Length == 1) return vals[0];
        int[] des = new int[vals.Length];
        Array.Copy(vals, des, vals.Length);
        Array.Sort(des);
        return des[des.Length - 1];
    }
}

public static class ListExtensions
{
    public static T Shift<T>(this List<T> list)
    {
        if (list.Count == 0)  throw new InvalidOperationException("List is empty.");
        T first = list[0];
        list.RemoveAt(0);
        return first;
    }
}