using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System;
using System.Collections;

namespace UDMF_VertexTools
{


    public class StrUtils
    {



        public static string StripUDMFFluff(string str)
        {
            str = str.Substring(str.IndexOf("=") + 2);
            str = str.Substring(0, str.IndexOf(";"));

            return str;
        }

        public static string StripPath(string str)
        {
            if (!str.Contains("/") && !str.Contains("\\"))
            {
                Console.WriteLine("string is not path");
                return "str";
            }

            string b = str.Substring(str.LastIndexOf("/") + 1);
            b = b.Substring(str.LastIndexOf("\\") + 1);

            if (b.IndexOf(".") != -1)
                b = b.Substring(0, b.IndexOf("."));
            return b;

        }
        public static string[] GetArgumnets(string input)
        {
            input = input.Substring(input.IndexOf('(') + 1);
            input = input.Substring(0, input.LastIndexOf(')'));
            return input.Split(new char[] { ',' }, StringSplitOptions.None);
        }

        public static string StripFluff(string str, List<StrFormatterPair> pairs)//																		----------DERP
        {
            for (int q = 0; q < pairs.Count; q++)
            {
                if (str.Contains(pairs[q].stuffToRemove))
                {
                    if (!pairs[q].afterTheFact)
                    {
                        str = str.Substring(0, str.IndexOf(pairs[q].stuffToRemove));
                    }
                    else
                        str = str.Substring(str.IndexOf(pairs[q].stuffToRemove) + 1);
                }
            }

            return str;
        }
        public static string StripBeforeLastOccurence(string str, string token, bool occurenceIncluded)
        {
            int m = str.LastIndexOf(token);
            if (m == -1) return str;
            if (!occurenceIncluded) m += 1;
            return str.Substring(m);
        }

        public static string StripNameOfFluff(string str, string extension)
        {
            str = str.Substring(str.LastIndexOf("/") + 1);
            str = str.Substring(0, str.IndexOf(extension));
            return str;
        }

        public static string StripFluffFromString(string str)
        {
            str = str.Substring(0, str.LastIndexOf("\""));
            str = str.Substring(str.LastIndexOf("\"") + 1);
            return str;
        }

        public static TextBlock[] GetBlocks(string file, string keyword)
        {
            string[] lines = file.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            List<TextBlock> tb = new List<TextBlock>();

            for (int c = 0; c < lines.Length; c++)
            {
                bool fileBroken = false;
                if (lines[c].ToLower().Contains(keyword))
                {
                    int m = 1;
                    int count = c;
                    bool eof = false;

                    if (lines[c + 1].Contains("{")) count++;

                    while (eof == false)
                    {
                        count++;
                        if (count >= lines.Length) { eof = true; fileBroken = true; break; }
                        if (lines[count].ToLower().Contains("{")) m++;
                        if (lines[count].ToLower().Contains("}")) m--;

                        if (m == 0) eof = true;


                    }

                    if (fileBroken)
                    {
                        Console.WriteLine("File is broken, cannot find closing }");
                        break;
                    }
                    else
                    {
                        string[] subArr = new string[count - c];
                        Array.Copy(lines, c + 1, subArr, 0, count - c); //got actor's block
                        TextBlock nTb = new TextBlock(lines[c], subArr);

                        //				actors.Add(PopulateActor(subArr));
                        tb.Add(nTb);
                        c = count;
                    }
                }

                if (fileBroken)
                {
                    Console.WriteLine("File is broken, cannot find closing }");
                    break;
                }
            }
            return tb.ToArray();

        }


        public static Vector2 FormVector2FromList(string[] input)
        {
            Vector2 vect = new Vector2();
            foreach (string str in input)
            {
                if (str.Contains("x ="))
                {
                    vect.x = (float)System.Convert.ToDouble(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("y ="))
                {
                    vect.y = (float)System.Convert.ToDouble(StrUtils.StripUDMFFluff(str));
                }
            }
            return vect;
        }
    }

    public class TextBlock
    {
        public string header;
        public string[] blockLines;

        public TextBlock(string h, string[] lines)
        {
            header = h;
            blockLines = lines;
        }

    }

    public class StrFormatterPair//																													---------Herp
    {
        public string stuffToRemove;
        public bool afterTheFact;
        public StrFormatterPair(string str, bool aft)
        {
            stuffToRemove = str;
            afterTheFact = aft;
        }
    }
}
