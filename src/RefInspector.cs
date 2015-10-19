using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class RefInspector2
{
    private static Dictionary<int, bool> foldouts { get; set; }

    static RefInspector2()
    {
        foldouts = new Dictionary<int, bool>();
    }

    public static void Inspector(this object obj)
    {
        var props = obj.GetType().GetProperties();

        foreach(var prop in props)
        {
            RenderProperty(new Proxy(prop, obj), obj);
        }
    }

    private static void RenderProperty(Proxy p, object obj)
    {
        var val = p.GetValue();
        var enabled = GUI.enabled;

        if (!p.canWrite)
            GUI.enabled = false;

        var rendered = false;
        var methods = typeof(RefInspector2).GetMethods(
            BindingFlags.Static | BindingFlags.NonPublic);
        foreach(var method in methods)
        {
            if (method.ReturnType == p.type &&
                method.GetParameters().Length == 2 &&
                method.GetParameters()[1].ParameterType == p.type)
            {
                val = method.Invoke(null, new object[] { p.name, val });
                rendered = true;
                break;
            }
        }

        if (p.type.GetInterface("IList") != null)
        {
            OnList(p, (IList)val);
            return;
        }

        if (!rendered && val != null)
        {
            OnObject(p.name, val);
            return;
        }

        if (!p.canWrite)
            GUI.enabled = enabled;
        else
            p.SetValue(val);
    }
    private static String OnString(String name, String str)
    {
        return EditorGUILayout.TextField(name, str);
    }
    private static int OnInt(String name, int n)
    {
        return EditorGUILayout.IntField(name, n);
    }
    private static float OnFloat(String name, float n)
    {
        return EditorGUILayout.FloatField(name, n);
    }
    private static bool OnBool(String name, bool n)
    {
        return EditorGUILayout.Toggle(name, n);
    }

    private static bool Foldout(String name, object obj)
    {
        var hash = obj.GetHashCode();

        if (!foldouts.ContainsKey(hash))
            foldouts[hash] = false;

        foldouts[hash] = EditorGUILayout.Foldout(
            foldouts[hash], name);

        return foldouts[hash];
    }

    private static object OnObject(String name, object obj)
    {
        if (Foldout(name, obj))
        {
            EditorGUI.indentLevel++;
            obj.Inspector();
            EditorGUI.indentLevel--;
        }

        return null;
    }

    private static object OnList(Proxy p, IList list)
    {
        if (Foldout(p.name, list))
        {
            EditorGUI.indentLevel++;

            if (!list.IsFixedSize)
            {
                var size = EditorGUILayout.IntField("size", list.Count);

                if (size > list.Count)
                { // expand
                    for (var i = 0; i <= size - list.Count; i++)
                    {
                        list.Add(
                            Activator.CreateInstance(
                                list.GetType().GetGenericArguments()[0]));
                    }
                }
                else if (size < list.Count)
                { // shirnk
                    for (var i = 0; i <= list.Count - size; i++)
                        list.RemoveAt(list.Count - 1);
                }
            }

            var count = 0;
            foreach (var item in list)
            {
                RenderProperty(new Proxy(list, count, item.GetType()), item);

                count += 1;
            }

            EditorGUI.indentLevel--;
        }

        return null;
    }
}