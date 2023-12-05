using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day4 : Solution
    {
        protected override string FileName => "day4";

        public override void Run()
        {
            string[] list = GetInputLines();

            List<int> copies = new List<int> { 1 };
            
            int total = 0;
            int index = -1;
            foreach (var l in list)
            {
                index++;
                if (index >= copies.Count)
                    copies.Add(1);
                
                string[] card = l.Split(':', StringSplitOptions.TrimEntries);
                string[] entries = card[1].Split('|', StringSplitOptions.TrimEntries);

                string[] winningNumbers = entries[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                HashSet<string> winnerNumbers = new HashSet<string>(winningNumbers);
                
                string[] myNumbers = entries[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                //int points = 0;
                int winnerNumberCount = 0;
                foreach (string entry in myNumbers)
                {
                    if (winnerNumbers.Contains(entry))
                    {
                        // Part-1
                        // points = points == 0 ? 1 : points * 2;
                        
                        winnerNumberCount++;
                    }
                }
                
                int ownCopies = copies[index];
                for (int i = index + 1; i < index + 1 + winnerNumberCount; i++)
                {
                    if (i >= copies.Count)
                        copies.Add(ownCopies + 1);
                    else
                        copies[i] += ownCopies;
                }

                //Part-1
                //total += points;
            }

            foreach (int copy in copies)
                total += copy;

            Console.WriteLine(total);
        }
    }
}