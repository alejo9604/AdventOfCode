using System;
using System.Collections.Generic;

namespace AllieJoe.AdventOfCode2022
{
    public class Day5
    {
        public void Run(string input, string movesInput)
        {
            string[] inputs = input.Split("\n");
            Ship ship = new Ship(inputs);
            
            string[] moves = movesInput.Split("\n");
            foreach (string move in moves)
            {
                ship.ProcessMove(move);
            }
            
            Console.WriteLine(ship.GetTop());
        }
        
        private class Ship
        {
            private List<Stack<char>> _containers = new List<Stack<char>>();

            public Ship(string[] inputs)
            {
                foreach (string input in inputs)
                {
                    Stack<char> stack = new Stack<char>();
                    for (int i = input.Length - 1; i >= 0; i--)
                    {
                        stack.Push(input[i]);
                    }
                    
                    _containers.Add(stack);
                }
            }

            public void ProcessMove(string move)
            {
                string[] data = move.Split(" ");
                int amount = int.Parse(data[1]);
                int from = int.Parse(data[3]) - 1;
                int to = int.Parse(data[5]) - 1;
                
                Move(amount, from, to);
            }

            public string GetTop()
            {
                string result = "";
                for (int i = 0; i < _containers.Count; i++)
                {
                    result += _containers[i].Peek();
                }

                return result;
            }
            
            private void Move(int amount, int from, int to)
            {
                char[] temp = new char[amount];
                Stack<char> fromContainer = _containers[from];
                Stack<char> toContainer = _containers[to];
                for (int i = 0; i < amount; i++)
                {
                    temp[i] = fromContainer.Pop();
                }

                for (int i = temp.Length - 1; i >= 0; i--)
                {
                    toContainer.Push(temp[i]);
                }
            }
            
            /// <summary>
            /// Part 1
            /// </summary>
            private void MoveSingle(int amount, int from, int to)
            {
                Stack<char> fromContainer = _containers[from];
                Stack<char> toContainer = _containers[to];
                for (int i = 0; i < amount; i++)
                {
                    char c = fromContainer.Pop();
                    toContainer.Push(c);
                }
            }
        }
    }
}