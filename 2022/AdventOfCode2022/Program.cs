﻿using System.IO;

namespace AllieJoe.AdventOfCode2022
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = File.ReadAllText("D:/Work/Personal/AdventOfCode/2022/AdventOfCode2022/Inputs/day7_input.txt");

            var solution = new Day7();
            solution.Run(input);
            

        }
    }
}