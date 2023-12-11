using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2023
{
    public class Day9 : Solution
    {
        private const bool DEBUG = false;
        
        protected override string FileName => "day9";

        public override void Run()
        {
            string[] list = GetInputLines();

            int total = 0;
            foreach (string l in list)
            {
                string[] values = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> numbers = new List<int>(values.Length);
                foreach (string v in values)
                    numbers.Add(int.Parse(v));
                
                total += ProcessLine(numbers);
            }
            
            Console.WriteLine(total);
        }

        private int ProcessLine(List<int> numbers)
        {
            if (DEBUG)
            {
                foreach (int n in numbers)
                    Console.Write($"{n}   ");
                Console.WriteLine();
            }

            return ProcessLine(numbers, 1);
        }
        
        
        private int ProcessLine(List<int> numbers, int level)
        {
            int N = numbers.Count;
            bool foundCommonResult = true;

            if (DEBUG)
            {
                for (int i = 0; i < level; i++)
                    Console.Write("  ");
            }

            //Part-2
            int valueToTrack = numbers[0];
            
            for (int i = 0; i < N - 1; i++)
            {
                numbers[i] = (numbers[i + 1] - numbers[i]);
                
                if (DEBUG)
                    Console.Write($"{numbers[i]}   ");
                
                if (i > 0 && numbers[i] != numbers[i - 1])
                    foundCommonResult = false;
            }

            //Part-1
            // int valueToTrack = numbers[^1]; 
            numbers.RemoveAt(numbers.Count - 1);

            if (DEBUG)
                Console.WriteLine();
            
            //Part-1
            // if (foundCommonResult)
            //     return valueToTrack + numbers[0];
            // return valueToTrack + ProcessLine(numbers, level + 1);
            
            //Part-2
            if (foundCommonResult)
                return valueToTrack - numbers[0];
            return valueToTrack - ProcessLine(numbers, level + 1);
        }
    }
}