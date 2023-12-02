using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day1 : Solution
    {
        protected override string FileName => "day1";

        private string[] numberStrings = new[]
            { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public override void Run()
        {
            string[] list = GetInputLines();

            int total = 0;
            
            // Regex regex1 = new Regex(@"\d+");
            // Regex regex2 = new Regex(@"\d+", RegexOptions.RightToLeft);
            //
            // GetValue("jrnf3");
            
            foreach (var l in list)
            {
                //(bool success, int value) = GetValue(l);
                (bool success, int value) = GetValueIncludingString(l);
                if (success)
                    total += value;
                else 
                    Console.WriteLine($"Failed for {l}");
            }
            
            Console.WriteLine($"Total: {total}");
        }

        // Part-1
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
        
        //Part-2
        private (bool, int) GetValueIncludingString(string l)
        {
            int minValue = 10;
            int minIndex = int.MaxValue;
            int maxValue = -1;
            int maxIndex = -1;
            for (int i = 0; i < numberStrings.Length; i++)
            {
                (int firstOccurence, int lastOccurrence) = GetIndexOf(l, numberStrings[i], i+1);

                if (firstOccurence >= 0 && firstOccurence < minIndex)
                {
                    minIndex = firstOccurence;
                    minValue = i + 1;
                }
                
                if (lastOccurrence >= 0 && lastOccurrence > maxIndex)
                {
                    maxIndex = lastOccurrence;
                    maxValue = i + 1;
                }
            }

            if (minIndex < 0 || maxIndex < 0)
            {
                return (false, 0);
            }
            
            return (true, minValue * 10 + maxValue);
        }

        private (int firstOccurrence, int lastOccurence) GetIndexOf(string l, string substring, int number)
        {
            (int firstStringOccurence, int lastStringOccurrence) = GetIndexOf(l, substring);
            (int firstNumberOccurence, int lastNumberOccurrence) = GetIndexOf(l, number.ToString());

            int firstOccurence = -1;
            if (firstStringOccurence >= 0 && firstNumberOccurence >= 0)
                firstOccurence = Math.Min(firstStringOccurence, firstNumberOccurence);
            else
                firstOccurence = Math.Max(firstStringOccurence, firstNumberOccurence);
            return (firstOccurence, Math.Max(lastStringOccurrence, lastNumberOccurrence));
        }

        private (int firstOccurrence, int lastOccurence) GetIndexOf(string l, string substring)
        {
            return (l.IndexOf(substring, StringComparison.Ordinal), l.LastIndexOf(substring, StringComparison.Ordinal));
        }
    }
    
}