using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
public class ParseFBXValues
{
	public static List<float> Parse(string regexpattern,string input ,List<float> float_list)
	{
        Regex rg = new Regex(regexpattern);
        MatchCollection matches = rg.Matches(input);
        string temp = matches[0].Groups[1].Value.ToString(); //This is the first real matching group
        float_list = (Array.ConvertAll(temp.Split(","), float.Parse)).ToList(); //Splits a string into an array of floats based on commas

        return float_list;
    }

    public static List<int> Parse(string regexpattern, string input, List<int> int_list)
    {
        Regex rg = new Regex(regexpattern);
        MatchCollection matches = rg.Matches(input);
        string temp = matches[0].Groups[1].Value.ToString(); //This is the first real matching group
        int_list = (Array.ConvertAll(temp.Split(","), int.Parse)).ToList(); //Splits a string into an array of floats based on commas

        return int_list;
    }

    public static List<string> Parse(string regexpattern, string input, List<string> string_list)
    {
        Regex rg = new Regex(regexpattern);
        MatchCollection matches = rg.Matches(input);
        string temp = matches[0].Groups[1].Value.ToString(); 
        string_list = temp.Split(",").ToList(); 

        return string_list;
    }
    public static List<Vector2> Parse(string regexpattern, string input, List<Vector2> v2_list)
    {
        Regex rg = new Regex(regexpattern);
        MatchCollection matches = rg.Matches(input);
        string temp = matches[0].Groups[1].Value.ToString(); 
        float[] temp_array = Array.ConvertAll(temp.Split(","), float.Parse); 
        for (int i = 0; i < temp_array.Length; i++)
        {
            Vector2 vec = new Vector2(temp_array[i], temp_array[++i]);
            v2_list.Add(vec);
        }
        return v2_list;
    }

}
