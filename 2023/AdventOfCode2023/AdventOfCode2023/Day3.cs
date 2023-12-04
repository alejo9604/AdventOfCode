using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day3 : Solution
    {
        protected override string FileName => "day3";
        const char DOT = '.';
        const char START = '*';
        private int LINE_LENGHT = 0;

        public override void Run()
        {
            string[] list = GetInputLines();

            LINE_LENGHT = list[0].Length;
            
            int total = 0;
            int lineIndex = -1;
            foreach (string l in list)
            {
                lineIndex++;
                for (int i = 0; i < l.Length; i++)
                {
                    char c = l[i];
                    // Part-1
                    // if(char.IsDigit(c) || c == DOT)
                    //     continue;
                    
                    // Part-2
                    if(c != START)
                        continue;

                    var numbersFound = CheckNumber(list, lineIndex, i);
                    // Part-1
                    // foreach (int n in numbersFound)
                    //     total += n;
                    
                    // Part-2
                    if(numbersFound.Count == 2)
                        total += (numbersFound[0]*numbersFound[1]);
                }
            }
            Console.WriteLine(total);
        }

        private List<int> CheckNumber(string[] list, int lineIndex, int symbolIndex)
        {
            int initLine = Math.Max(0, lineIndex - 1);
            int endLine = Math.Min(list.Length - 1, lineIndex + 1);
            
            int initIndex = Math.Max(0, symbolIndex - 1);
            int endIndex = Math.Min(LINE_LENGHT - 1, symbolIndex + 1);

            List<int> numbersFound = new();
            for (int line = initLine; line <= endLine; line++)
            {
                for (int index = initIndex; index <= endIndex; index++)
                {
                    if (!char.IsDigit(list[line][index]))
                        continue;

                    (int value, int maxIndexExplored) = GetNumber(list[line], index);
                    index = maxIndexExplored;
                    numbersFound.Add(value);
                }
            }

            return numbersFound;
        }

        private (int value, int maxIndexExplored) GetNumber(string line, int index)
        {
            //Left
            int temp = index;
            int minIndex = -1;
            int maxIndex = -1;
            while (temp >= 0)
            {
                if(!char.IsDigit(line[temp]))
                    break;
                minIndex = temp;
                temp--;
            }
            
            //Right
            temp = index;
            while (temp < line.Length)
            {
                if(!char.IsDigit(line[temp]))
                    break;
                maxIndex = temp;
                temp++;
            }

            return (int.Parse(line.Substring(minIndex, maxIndex - minIndex + 1)), maxIndex);
        }
    }
}