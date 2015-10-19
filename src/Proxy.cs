using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public class Proxy
{
    private PropertyInfo prop { get; set; }
    private FieldInfo field { get; set; }
    private object target { get; set; }
    private IList list { get; set; }
    private int index { get; set; }
    private Type _type { get; set; }

    private bool isProperty
    {
        get
        {
            return prop != null;
        }
    }
    private bool isField
    {
        get
        {
            return field != null;
        }
    }
    private bool isListItem
    {
        get
        {
            return list != null;
        }
    }
    public bool canWrite
    {
        get
        {
            if (isProperty)
                return prop.CanWrite;
            else if (isField)
                return true;
            else if (isListItem)
                return true;

            throw new InvalidOperationException();
        }
    }
    public String rawName
    {
        get
        {
            if (isProperty)
                return prop.Name;
            else if (isField)
                return field.Name;
            else if (isListItem)
                return index.ToString();

            throw new InvalidOperationException();
        }
    }
    public String name
    {
        get
        {
            return NameConverter.Convert(rawName);
        }
    }
    public Type type
    {
        get
        {
            if (isProperty)
                return prop.PropertyType;
            else if (isField)
                return field.FieldType;
            else if (isListItem)
                return _type;

            throw new InvalidOperationException();
        }
    }

    public Proxy(PropertyInfo prop, object target)
    {
        this.prop = prop;
        this.target = target;
    }
    public Proxy(FieldInfo field, object target)
    {
        this.field = field;
        this.target = target;
    }
    public Proxy(IList list, int index, Type type)
    {
        this.list = list;
        this.index = index;
        this._type = type;
    }

    public void SetValue(object val)
    {
        if (isProperty)
            prop.SetValue(target, val, null);
        else if (isField)
            field.SetValue(target, val);
        else if (isListItem)
            list[index] = val;
    }
    public object GetValue()
    {
        if (isProperty)
            return prop.GetValue(target, null);
        else if (isField)
            return field.GetValue(target);
        else if (isListItem)
            return list[index];

        throw new InvalidOperationException();
    }
}