using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day8 : Solution
    {
        protected override string FileName => "day8";

        private const char LEFT = 'L';
        private const char RIGHT = 'R';
        private const char A = 'A';
        private const char Z = 'Z';
        private const string FIRST_NODE = "AAA";
        private const string LAST_NODE = "ZZZ";
        
        struct Node
        {
            public string Name;
            
            public string L;
            public string R;

            public Node(string name, string l, string r)
            {
                Name = name;
                L = l;
                R = r;
            }

            public override string ToString() => $"{Name} = ({L}, {R})";
        }
        
        public override void Run()
        {
            string[] list = GetInputLines();

            string input = list[0];
            
            
            Dictionary<string, Node> nodes = new();

            //Part-2
            List<Node> nodesToTrack = new();

            for (int i = 2; i < list.Length; i++)
            {
                string l = list[i];
                string[] line = l.Split('=', StringSplitOptions.TrimEntries);
                string name = line[0];
                string[] lr = line[1].Split(',', StringSplitOptions.TrimEntries);

                var newNode = new Node(name, lr[0].Remove(0, 1), lr[1].Remove(3, 1));
                nodes.Add(name, newNode);
                
                if(name[^1] == A)
                    nodesToTrack.Add(newNode);
            }
            
            
            //Part-2
            int[] cicles = new int[nodesToTrack.Count];
            for (int i = 0; i < nodesToTrack.Count; i++)
            {
                cicles[i] = 0;
                Node currentNode = nodesToTrack[i];
                while (currentNode.Name[^1] != Z)
                {
                    foreach (char dir in input)
                    {
                        cicles[i]++;
                        currentNode = dir == LEFT ? nodes[currentNode.L] : nodes[currentNode.R];
                        if(currentNode.Name[^1] == Z)
                            break;
                    }
                }
            }

            ulong steps = 1;
            for (int i = 0; i < cicles.Length; i++)
            {
                steps = LCM(steps, (ulong)cicles[i]);
            }
            
            
            
            //Part-1
            // bool found = false;
            // int attempts = 0;
            // int steps = 0;
            // Node currentNode = nodes[FIRST_NODE];
            // while (!found)
            // {
            //     if (attempts > 500)
            //     {
            //         Console.WriteLine("Max attempts reached. Fail?");
            //         break;
            //     }
            //
            //     attempts++;
            //     foreach (char dir in input)
            //     {
            //         steps++;
            //         
            //         if (dir == LEFT)
            //             currentNode = nodes[currentNode.L];
            //         else
            //             currentNode = nodes[currentNode.R];
            //         if (currentNode.Name == LAST_NODE)
            //         {
            //             found = true;
            //             break;
            //         }
            //     }
            // }
            // Console.WriteLine($"Attempts: {attempts}");
            
            
            Console.WriteLine(steps);
        }
        
        // Greatest Common Divisor or Highest Common Factor of a 
        static ulong GCD(ulong a, ulong b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
        
        static ulong LCM(ulong a, ulong b)
        {
            return (a * b) / GCD(a, b);
        }
    }
}