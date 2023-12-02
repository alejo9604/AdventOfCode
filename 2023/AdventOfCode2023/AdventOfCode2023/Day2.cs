using System;

namespace AdventOfCode2023
{
    public class Day2 : Solution
    {
        // Part-1
        //------------------------
        const int MAX_RED = 12;
        const int MAX_GREEN = 13;
        const int MAX_BLUE = 14;
        //------------------------
        
        protected override string FileName => "day2";

        public override void Run()
        {
            string[] list = GetInputLines();
            
            //Console.WriteLine(RunPart1(list));
            Console.WriteLine(RunPart2(list));
        }

        private int RunPart2(string[] list)
        {
            int total = 0;

            int[] maxValues = new int[3];
            
            foreach (string l in list)
            {
                string[] game = l.Split(':', StringSplitOptions.TrimEntries);
                
                maxValues[0] = maxValues[1] = maxValues[2] = 0;
                
                string[] entries = game[1].Split(';', StringSplitOptions.TrimEntries);
                foreach (string entry in entries)
                {
                    string[] items = entry.Split(',', StringSplitOptions.TrimEntries);
                    foreach (string item in items)
                    {
                        string[] values = item.Split(' ', StringSplitOptions.TrimEntries);
                        UpdateMaxValues(values[0], values[1], maxValues);
                    }
                }
                
                total += (maxValues[0] * maxValues[1] * maxValues[2]);
            }

            return total;
        }
        private void UpdateMaxValues(string value, string id, int[] maxValues)
        {
            int num = int.Parse(value);
            switch (id)
            {
                case "red":
                    maxValues[0] = Math.Max(maxValues[0], num);
                    break;
                case "green":
                    maxValues[1] = Math.Max(maxValues[1], num);
                    break;
                case "blue":
                    maxValues[2] = Math.Max(maxValues[2], num);
                    break;
            }
        }
        

        private int RunPart1(string[] list)
        {
            int total = 0;
            
            foreach (string l in list)
            {
                string[] game = l.Split(':', StringSplitOptions.TrimEntries);
                
                bool isPossible = true;
                string[] entries = game[1].Split(';', StringSplitOptions.TrimEntries);
                foreach (string entry in entries)
                {
                    string[] items = entry.Split(',', StringSplitOptions.TrimEntries);
                    foreach (string item in items)
                    {
                        string[] values = item.Split(' ', StringSplitOptions.TrimEntries);
                        if(!CheckMax(values[0], values[1]))
                        {
                            isPossible = false;
                            break;
                        }
                    }
                    
                    if(!isPossible)
                        break;
                }
                
                if (isPossible)
                {
                    total += int.Parse(game[0].Split(' ', StringSplitOptions.TrimEntries)[1]);
                }
            }

            return total;
        }
        private bool CheckMax(string value, string id)
        {
            int num = int.Parse(value);
            switch (id)
            {
                case "red":
                    return num <= MAX_RED;
                case "green":
                    return num <= MAX_GREEN;
                case "blue":
                    return num <= MAX_BLUE;
            }

            return false;
        }
    }
}