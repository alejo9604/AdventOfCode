using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day11 : Solution
    {
        protected override string FileName => "day11";

        public override void Run()
        {
            string[] list = GetInputLines();

            // Part-1
            //const int expansionValue = 2;
            // Part-2
            const int expansionValue = 1_000_000;
            
            int x = 0, y = 0;

            bool[] needToExpandHor = new bool[list[0].Length];
            for (int i = 0; i < needToExpandHor.Length; i++)
                needToExpandHor[i] = true;
            bool[] needToExpandVert = new bool[list.Length];
            
            List<int> coordX = new List<int>();
            List<int> coordY = new List<int>();
            foreach (string l in list)
            {
                bool needToExpand = true;
                x = 0;
                foreach (char c in l)
                {
                    if (c == '#')
                    {
                        needToExpandHor[x] = false;
                        needToExpand = false;
                        coordX.Add(x);
                        coordY.Add(y);
                    }
                    x++;
                }

                if (needToExpand)
                    needToExpandVert[y] = true;
                y++;
            }

            List<int> indexOnExpand = new();
            //Check expandValues Hor
            for (int i = 0; i < needToExpandHor.Length; i++)
                if(needToExpandHor[i])
                    indexOnExpand.Add(i);
            
            for (int i = 0; i < coordX.Count; i++)
            {
                int add = 0;
                for (int j = 0; j < indexOnExpand.Count; j++)
                {
                    if(coordX[i] < indexOnExpand[j])
                        break;
                    add += expansionValue - 1;
                }
                
                coordX[i] += add;
            }
            
            
            //Check expandValues Ver
            indexOnExpand.Clear();
            for (int i = 0; i < needToExpandVert.Length; i++)
                if(needToExpandVert[i])
                    indexOnExpand.Add(i);
            
            for (int i = 0; i < coordY.Count; i++)
            {
                int add = 0;
                for (int j = 0; j < indexOnExpand.Count; j++)
                {
                    if(coordY[i] < indexOnExpand[j])
                        break;
                    add += expansionValue - 1;
                }
                
                coordY[i] += add;
            }

            // foreach (int cx in coordX)
            //     Console.Write($"{cx}  ");
            // Console.WriteLine();
            // foreach (int cy in coordY)
            //     Console.Write($"{cy}  ");
            // Console.WriteLine();
            
            Console.WriteLine(CalculateManualDistance(coordX, coordY));
            Console.WriteLine(TotalDistanceSum(coordX.ToArray(), coordY.ToArray()));
        }
        
        // Return the sum of Manhattan  distance of one axis.
        private double ManhattanDistanceSum(int[] values)
        {
            Array.Sort(values);
            double res = 0, sum = 0;
            int n = values.Length;
            for (int i = 0; i < n; i++) {
                res += ((double)values[i] * i - sum);
                sum += values[i];
            }
 
            return res;
        }
 
        private double TotalDistanceSum(int[] x, int[] y)
        {
            return ManhattanDistanceSum(x) + ManhattanDistanceSum(y);
        }

        private double CalculateManualDistance(List<int> x, List<int> y)
        {
            int n = x.Count;
            double total = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if(i == j)
                        continue;
                    total +=  ((double)Math.Abs(x[i] - x[j]) + (double)Math.Abs(y[i] - y[j]));
                }
            }

            return total;
        }
    }
    
    //710674907809
    //894851088384
    //232324969302
}