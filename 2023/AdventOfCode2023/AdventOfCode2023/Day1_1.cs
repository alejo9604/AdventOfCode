using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day1_1 : Solution
    {
        protected override string FileName => "day1_1";

        public override void Run()
        {
            string[] list = GetInputLines();

            int total = 0;
            
            Regex regex1 = new Regex(@"\d+");
            Regex regex2 = new Regex(@"\d+", RegexOptions.RightToLeft);

            GetValue("jrnf3");
            
            foreach (var l in list)
            {
                (bool success, int value) = GetValue(l);
                if (success)
                    total += value;
            }
            
            Console.WriteLine($"Total: {total}");
        }

        private (bool, int) GetValue(string l)
        {
            char c1 = ' ';
            char c2 = ' ';
            for (int i = 0; i <= l.Length - 1; i++)
            {
                char c = l[i];
                if (c1 == ' ' && char.IsDigit(c))
                    c1 = c;
                    
                c = l[^(i+1)];
                if (c2 == ' ' && char.IsDigit(c))
                    c2 = c;

                if (c1 != ' ' && c2 != ' ')
                {
                    return (true, int.Parse($"{c1}{c2}"));
                }
            }

            return (false, 0);
        }
    }
    
}