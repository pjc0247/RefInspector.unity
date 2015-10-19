using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NameConverter
{
    public static String Convert(String original)
    {
        if (String.IsNullOrEmpty(original))
            throw new ArgumentException("original");

        var result = char.ToUpper(original[0]).ToString();

        for(var i = 1; i < original.Length - 1; i++)
        {
            if (char.IsUpper(original[i]))
            {
                if (char.IsLower(original[i + 1]))
                    result += " ";

                result += original[i];
            }
            if (char.IsLower(original[i]))
            {
                result += original[i];

                if (char.IsUpper(original[i + 1]))
                    result += " ";
            }
        }

        if(original.Length != 1)
            result += original[original.Length - 1];

        return result;
    }
}