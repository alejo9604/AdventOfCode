using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day12 : Solution
    {
        private const char DOT = '.';
        private const char QUESTION = '?';
        private const char SPRINT = '#';
        
        Dictionary<string, long> _combinationsCacheDict = new Dictionary<string, long>();
        
        protected override string FileName => "day12";

        public override void Run()
        {
            string[] list = GetInputLines();

            long total = 0;
            
            foreach (string l in list)
            {
                string[] input = l.Split(' ');
                string springInput = input[0];
                
                string[] groupsStr = input[1].Split(',');
                int[] groups = new int[groupsStr.Length];
                
                for (int i = 0; i < groupsStr.Length; i++)
                    groups[i] = int.Parse(groupsStr[i]);
                
                // Part-2
                springInput = string.Join('?', Enumerable.Repeat(springInput, 5));
                groups = Enumerable.Repeat(groups, 5).SelectMany(g => g).ToArray();
                
                long result = GetCombinations(springInput, groups);

                total += result;
            }
            
            Console.WriteLine(total);
        }
        
        long GetCombinations(string input, int[] groups, int currentGroupIndex = 0)
        {
            var key = $"{input}<{string.Join(',', groups)}>[{currentGroupIndex}]";
            if (_combinationsCacheDict.TryGetValue(key, out var combinations))
                return combinations;

            combinations = FindCombinations(input, groups, currentGroupIndex);
            _combinationsCacheDict.Add(key, combinations);
            
            return combinations;
        }

        long FindCombinations(string input, int[] groups, int currentGroupIndex)
        {
            //If no more groups and no more match, we have an occurence
            if (groups.Length == currentGroupIndex)
                return input.Contains('#') ? 0 : 1;

            //If we check all the input, but still have groups to fill -> no valid
            if (string.IsNullOrEmpty(input))
                return 0;

            //Remove empty slots (end to since it doesn't matter). And check again
            if (input[0] == DOT)
                return FindCombinations(input.Trim(DOT), groups, currentGroupIndex);

            if (input[0] == QUESTION)
                return GetCombinations($"{SPRINT}{input[1..]}", groups, currentGroupIndex) + GetCombinations($"{DOT}{input[1..]}", groups, currentGroupIndex);
            
            //--------------------------------------
            //If reach here, the first char it's '#'
            
            // No more groups but we have inputs to fill -> no valid
            if (groups.Length == currentGroupIndex)
                return 0;

            int currentGroupLength = groups[currentGroupIndex];
            
            // Current input doesn't fit the group lenght -> no valid
            if (input.Length < currentGroupLength)
                return 0;

            // This mean the first group in the input doesn't match the group size -> no valid
            if (input[..currentGroupLength].Contains(DOT))
                return 0;

            int groupsLeft = groups.Length - currentGroupIndex;
            if (groupsLeft > 1)
            {
                // If not enough character to fill the next group OR the next char it's an sprint -> no valid
                //  The next char need to be a '.' or '?' to be valid
                if (input.Length < currentGroupLength + 1 || input[currentGroupLength] == SPRINT)
                    return 0;
            }
            
            // If we have more groups to check, we skip the next character since it's a '.' or '?'
            int indexToSubString = currentGroupLength + (groupsLeft > 1 ? 1 : 0);
            string nextSubstring = input[indexToSubString..];
            return FindCombinations(nextSubstring, groups, currentGroupIndex +1);
        }
        
    }
}