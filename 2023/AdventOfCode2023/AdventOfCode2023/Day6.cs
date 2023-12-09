using System;

namespace AdventOfCode2023
{
    public class Day6 : Solution
    {
        protected override string FileName => "day6";

        public override void Run()
        {
            string[] list = GetInputLines();
            
            Console.WriteLine(Part2(list));
        }

        private ulong Part2(string[] list)
        {
            string timeStr = list[0].Split(':')[1].Replace(" ", "");
            string recordStr = list[1].Split(':')[1].Replace(" ", "");
            
            ulong time = ulong.Parse(timeStr);
            ulong record = ulong.Parse(recordStr);
            
            // Brute-force
            // ulong total = 0;
            // for (ulong j = 1; j < time; j++)
            // {
            //     if (j * (time - j) > record)
            //     {
            //         total = (time - 2*j + 1);
            //         break;
            //     }
            // }

            //Solving quadratic formula.
            // Min value = - Time + Sqrt[ Time^2 - 4(-1)(-Record)]
            //             ---------------------------------------
            //                              2(-1)
            ulong val = (ulong)(Math.Floor(time - Math.Sqrt(time * time - 4 * record)) / 2f);
            if (val * (time - val) <= record)
                val++;
            
            return (time - 2*val + 1);
        }
        
        private int Part1(string[] list)
        {
            string[] times = list[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] records = list[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int total = 1;
            for (int i = 0; i < times.Length; i++)
            {
                int time = int.Parse(times[i]);
                int record = int.Parse(records[i]);

                for (int j = 1; j < time; j++)
                {
                    if (j * (time - j) > record)
                    {
                        total *= (time - j - j + 1);
                        break;
                    }
                }
            }

            return total;
        }
    }
}