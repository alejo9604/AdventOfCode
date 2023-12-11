using System;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    public class Day10 : Solution
    {
        protected override string FileName => "day10";

        const char W_E = '─';
        const char N_S = '│';
        const char S_E = '┌';
        const char S_W = '┐';
        const char N_E = '└';
        const char N_W = '┘';

        private const char NONE = ' ';
        
        const char S = 'S';

        private static readonly Dictionary<char, char> PIPE_MAP = new()
        {
            { '|', N_S },
            { '-', W_E },
            { 'L', N_E },
            { 'J', N_W },
            { '7', S_W },
            { 'F', S_E },
            { '.', NONE }
        };
        
        private static readonly Dictionary<char, (ENodeDir, ENodeDir)> AVAILABLE_DIR = new()
        {
            { N_S, (ENodeDir.Up, ENodeDir.Down) },
            { W_E, (ENodeDir.Left, ENodeDir.Right) },
            { N_E, (ENodeDir.Up, ENodeDir.Right) },
            { N_W, (ENodeDir.Up, ENodeDir.Left) },
            { S_W, (ENodeDir.Down, ENodeDir.Left) },
            { S_E, (ENodeDir.Down, ENodeDir.Right) },
            { NONE, (ENodeDir.None, ENodeDir.None) }
        };
        
        private static int WIDTH;
        private static int HEIGHT;

        public enum ENodeDir
        {
            Up,
            Down,
            Right,
            Left,
            None
        };
        
        public enum ENodeType{ PipeUp, PipeDown, PipeHor, Inside, None }

        class Node
        {
            public int X;
            public int Y;
            public readonly char Pipe;
            public bool Start;
            
            public int NextIndex1 = -1;
            public int NextIndex2 = -1;

            public ENodeType Type;
            

            public Node(char pipeKey, int x, int y, bool start)
            {
                Pipe = PIPE_MAP[pipeKey];
                X = x;
                Y = y;
                Start = start;
                Type = ENodeType.None;
                if (Pipe != NONE)
                {
                    (ENodeDir dir1, ENodeDir dir2) = AVAILABLE_DIR[Pipe];
                    NextIndex1 = NextIndexInDir(x, y, dir1);
                    NextIndex2 = NextIndexInDir(x, y, dir2);
                }
            }
            
            public override string ToString() => Pipe.ToString();

            public int NextIndexFrom(int prevIndex)
            {
                if (Pipe == NONE)
                {
                    Console.WriteLine("Enter a ground tile!!");
                    return -1;
                }
                
                if (prevIndex == NextIndex1)
                    return NextIndex2;
                if (prevIndex == NextIndex2)
                    return NextIndex1;
                
                Console.WriteLine("Enter pipe from a no valid access!!");
                return -1;
            }

            public void UpdatePipeInLoop(int prevX, int prevY)
            {
                if (Pipe == W_E)
                {
                    Type = ENodeType.PipeHor;
                    return;
                }

                if (prevY == Y)
                {
                    Type = Pipe == N_W || Pipe == N_E ? ENodeType.PipeUp : ENodeType.PipeDown;
                    return;
                }

                Type = prevY > Y ? ENodeType.PipeUp : ENodeType.PipeDown;

            }

            public void UpdatePipeInside() => Type = ENodeType.Inside;

            static int NextIndexInDir(int x, int y, ENodeDir dir)
            {
                switch (dir)
                {
                    case ENodeDir.Up:
                        return (y - 1) * WIDTH + x;
                    case ENodeDir.Down:
                        return (y + 1) * WIDTH + x;
                    case ENodeDir.Right:
                        return y * WIDTH + x + 1;
                    case ENodeDir.Left:
                        return y * WIDTH + x - 1;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
                }
            }
            
        }
        
        public override void Run()
        {
            string[] list = GetInputLines();

            char knowS = 'F'; //For all AOC inputs this was fine
            List<Node> nodes = new();

            WIDTH = list[0].Length;
            HEIGHT = list.Length;
            
            int startNode = -1;

            int x = 0;
            int y = 0;
            foreach (string l in list)
            {
                x = 0;
                foreach (char c in l)
                {
                    Node newNode = new Node(c == S ? knowS : c, x, y, c == S);
                    nodes.Add(newNode);

                    if (newNode.Start)
                        startNode = y * WIDTH + x;
                    
                    x++;
                }

                y++;
            }

            int prevIndex = startNode;
            int currentIndex = nodes[startNode].NextIndex1;
            int totalNodesInLoop = 0;
            int attempts = 0;
            while (currentIndex != startNode)
            {
                if (attempts > nodes.Count + 1)
                {
                    Console.WriteLine("Max attempts reach");
                    break;
                }

                attempts++;
                
                int nextIndex = nodes[currentIndex].NextIndexFrom(prevIndex);
                nodes[currentIndex].UpdatePipeInLoop(nodes[prevIndex].X, nodes[prevIndex].Y);
                if(nextIndex < 0 || nextIndex >= nodes.Count)
                    break;

                prevIndex = currentIndex;
                currentIndex = nextIndex;
                totalNodesInLoop++;
            }
            
            nodes[currentIndex].UpdatePipeInLoop(nodes[prevIndex].X, nodes[prevIndex].Y);
            
            // Part-1
            // if(totalNodesInLoop % 2 == 0)
            //     Console.WriteLine(totalNodesInLoop/2);
            // else
            //     Console.WriteLine((totalNodesInLoop+1)/2);

            // Part-2
            int totalInside = 0;
            
            for (int i = 0; i < HEIGHT; i++)
            {
                bool inside = false;
                ENodeType currentDir = ENodeType.None;
                
                for (int j = 0; j < WIDTH; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Node node = nodes[i * WIDTH + j];
                    if (node.Type == ENodeType.PipeHor)
                    {
                        Console.Write(node);
                        continue;
                    }

                    if (node.Type is ENodeType.PipeUp or ENodeType.PipeDown)
                    {
                        if (currentDir != node.Type)
                        {
                            currentDir = node.Type;
                            inside ^= true;
                        }

                        Console.Write(node);
                        continue;
                    }

                    if (inside)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('I');
                        node.UpdatePipeInside();
                        totalInside++;
                    }
                    else
                        Console.Write(' ');

                }
                Console.WriteLine();
            }
            
            Console.WriteLine(totalInside);
        }
    }
}