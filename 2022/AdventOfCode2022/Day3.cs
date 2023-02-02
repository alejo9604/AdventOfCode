using System;
using System.Collections.Generic;

namespace AllieJoe.AdventOfCode2022
{
    public class Day3
    {
        public void Run(string input)
        {
            string[] list = input.Split("\n");
            int total = 0;
            for (int i = 0; i < list.Length; i++)
            {
                Dictionary<char, int> commun = new Dictionary<char, int>();
                bool found = false;

                for (int j = 0; j < 3; j++)
                { 
                    string content = list[i + j];
                    HashSet<char> hash = new HashSet<char>();
                    foreach (char c in content)
                    {
                        if (j == 0)
                        {
                            if(!commun.ContainsKey(c))
                                commun.Add(c, 1);
                            continue;
                        }
                        
                        if (!hash.Contains(c) && commun.ContainsKey(c))
                        {
                            commun[c]++;
                            if (commun[c] >= 3)
                            {
                                found = true;
                                total += GetPriority(c);
                                break;
                            }
                        }

                        hash.Add(c);
                    }
                    
                    if(found)
                        break;
                }

                i += 2;
            }
            
            Console.WriteLine(total);
        }

        private void Part1(string input)
        {
            string[] list = input.Split("\n");
            int total = 0;
            for (int i = 0; i < list.Length; i++)
            {
                string content = list[i];
                HashSet<char> halfContent = new HashSet<char>();
                for (int j = 0; j < content.Length; j++)
                {
                    if (j < content.Length / 2)
                    {
                        halfContent.Add(content[j]);
                    }
                    else if(halfContent.Contains(content[j]))
                    {
                        total += GetPriority(content[j]);
                        break;
                    }
                }
            }
            
            Console.WriteLine(total);
        }
        
        private int GetPriority(char c)
        {
            int baseValue = c;
            if (baseValue > 96)
                return baseValue - 96;
            else
                return baseValue - 38;
        }
    }
}