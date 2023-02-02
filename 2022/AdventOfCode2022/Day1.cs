using System;

namespace AllieJoe.AdventOfCode2022
{
    /// <summary>
    /// https://adventofcode.com/2022/day/1
    /// </summary>
    public class Day1
    {
        /// <summary>
        ///  Part 2
        /// </summary>
        public void Run(string input)
        {
            string inputProcessed = input.Replace("\r", "");
            string[] list = inputProcessed.Split("\n");
            
            int[] maxCalories = new []{0, 0, 0};
            int currentCount = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(list[i]))
                {
                    TryAddMax(ref maxCalories, currentCount);
                    currentCount = 0;
                }
                else
                {
                    currentCount += int.Parse(list[i]);
                }
                
            }

            int total = 0;
            for (int i = 0; i < maxCalories.Length; i++)
            {
                total += maxCalories[i];
                Console.WriteLine(maxCalories[i]);
            }
            Console.WriteLine(total);
        }

        private void TryAddMax(ref int[] max, int value)
        {
            if (value <= max[0])
            {
                return;
            }

            if (value <= max[1])
            {
                max[0] = value;
                return;
            }

            if (value <= max[2])
            {
                max[0] = max[1];
                max[1] = value;
                return;
            }
            
            max[0] = max[1];
            max[1] = max[2];
            max[2] = value;

        }

        /// <summary>
        ///  Part 1
        /// </summary>
        private void Part1(string input)
        {
            string inputProcessed = input.Replace("\r", "");
            string[] list = inputProcessed.Split("\n");
            
            int maxCalories = 0;
            int currentCount = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(list[i]))
                {
                    if (currentCount > maxCalories)
                        maxCalories = currentCount;
                    currentCount = 0;
                }
                else
                {
                    currentCount += int.Parse(list[i]);
                }
                
            }

            Console.WriteLine(maxCalories);
        }
        
    }
}