using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Msg
{
    private static Dictionary<int, Dictionary<int, List<Action<object[]>>>> messages = new ();

    public static void Init() {
        messages = new();
    }

    public static void Bind(int name, Action<object[]> f, int id = -1)
    {
        if (!messages.ContainsKey(name))
            messages[name] = new Dictionary<int, List<Action<object[]>>>();
        if (!messages[name].ContainsKey(id))
            messages[name][id] = new List<Action<object[]>>();
        if (id != -1 && messages[name][id].Count >= 1)
            Debug.LogErrorFormat("msg name %s id %s is used.", name, id);
        messages[name][id].Add(f);
    }

    public static void UnBind(int name, Action<object[]> f, int id = -1)
    {
        if (!messages.ContainsKey(name)) return;
        if (!messages[name].ContainsKey(id)) return;

        if (id == -1)
            messages[name][id].Remove(f);
        else
            messages[name][id].Clear();
    }

    public static void UnBind(int name, int id = -1)
    {
        if (!messages.ContainsKey(name)) return;
        if (!messages[name].ContainsKey(id)) return;

        messages[name][id].Clear();
    }

    public static void Dispatch(int name, object[] param = null)
    {
        if (!messages.ContainsKey(name))
            return;

        List<Action<object[]>> listeners = new();
        foreach (KeyValuePair<int, List<Action<object[]>>> itemKV in messages[name])
            listeners.AddRange(itemKV.Value);

        foreach (Action<object[]> f in listeners)
            f(param);
    }
}
