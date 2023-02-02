using System;

namespace AllieJoe.AdventOfCode2022
{
    public class Day4
    {

        public void Run(string input)
        {
            string[] list = input.Split("\n");
            int total = 0;
            foreach (string l in list)
            {
                //if (IsFullyContain(l)) //Part 1
                if (IsOverlap(l))
                    total++;
            }
            
            Console.WriteLine(total);
        }

        bool IsOverlap(string input)
        {
            string[] sections = input.Split(",");
            string[] first = sections[0].Split("-");
            string[] second = sections[1].Split("-");

            int firstMin = int.Parse(first[0]);
            int firstMax = int.Parse(first[1]);
                
            int secondMin = int.Parse(second[0]);
            int secondMax = int.Parse(second[1]);

            return firstMin <= secondMax && secondMin <= firstMax;
        }
        
        bool IsFullyContain(string input)
        {
            string[] sections = input.Split(",");
            string[] first = sections[0].Split("-");
            string[] second = sections[1].Split("-");

            int firstMin = int.Parse(first[0]);
            int firstMax = int.Parse(first[1]);
                
            int secondMin = int.Parse(second[0]);
            int secondMax = int.Parse(second[1]);

            return (firstMin >= secondMin && firstMax <= secondMax) ||
                   (secondMin >= firstMin && secondMax <= firstMax);
        }
    }
}