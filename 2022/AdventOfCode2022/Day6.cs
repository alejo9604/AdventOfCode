using System;
using System.Collections.Generic;

namespace AllieJoe.AdventOfCode2022
{
    public class Day6
    {
        public void Run(string input)
        {
            //int markerLength = 4;
            int markerLength = 14;
            
            HashSet<char> marker = new HashSet<char>(markerLength);
            string check = "";
            int min = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (marker.Contains(c))
                {
                    for (int j = min; j < i; j++)
                    {
                        char deleted = input[j]; 
                        marker.Remove(deleted);
                        min++;
                        check += deleted;
                        if (deleted == c)
                            break;
                    }
                }
                
                marker.Add(c);
                
                if(marker.Count == markerLength)
                {
                    //Console.WriteLine(check);
                    Console.WriteLine(i + 1);
                    // for (int j = i - 3; j <= i; j++)
                    // {
                    //     Console.WriteLine(input[j]);
                    // }
                    return;
                }
            }
        }
    }
}