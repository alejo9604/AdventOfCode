using System;

namespace AllieJoe.AdventOfCode2022
{
    public class Day2
    {
        private enum Hand { Rock, Paper, Scissors }
        private enum Result { Win = 6, Lose = 0, Tie = 3 }
        
        public void Run(string input)
        {
            string inputProcessed = input.Replace("\r", "");
            string[] list = inputProcessed.Split("\n");

            int totalScore = 0;
            for (int i = 0; i < list.Length; i++)
            {
                Hand opponent = GetHand(list[i][0]);
                Result result = GetResult(list[i][2]);
                Hand owner = GetDesiredHand(opponent, result);
                totalScore += TypeScore(owner) + (int)result;
            }
            
            Console.WriteLine(totalScore);
        }

        /// <summary>
        /// Part 1
        /// </summary>
        private void Part1(string input)
        {
            string inputProcessed = input.Replace("\r", "");
            string[] list = inputProcessed.Split("\n");

            int totalScore = 0;
            for (int i = 0; i < list.Length; i++)
            {
                Hand opponent = GetHand(list[i][0]);
                Hand owner = GetHand(list[i][2]);
                totalScore += TypeScore(owner) + Play(opponent, owner);
            }
            
            Console.WriteLine(totalScore);
        }
        
        private int TypeScore(Hand hand)
        {
            if (hand == Hand.Rock) return 1;
            if (hand == Hand.Paper) return 2;
            /*if (hand == Hand.Scissors)*/ return 3;
        }

        private int Play(Hand opponent, Hand owner)
        {
            //Use int/byte conversion?
            if (opponent == Hand.Rock)
            {
                if (owner == Hand.Paper) return 6;
                if (owner == Hand.Rock) return 3;
                if (owner == Hand.Scissors) return 0;
            }
            
            if (opponent == Hand.Paper)
            {
                if (owner == Hand.Scissors) return 6;
                if (owner == Hand.Paper) return 3;
                if (owner == Hand.Rock) return 0;
            }
            
            if (opponent == Hand.Scissors)
            {
                if (owner == Hand.Rock) return 6;
                if (owner == Hand.Scissors) return 3;
                if (owner == Hand.Paper) return 0;
            }

            return 0;
        }
        
        private Hand GetHand(char c)
        {
            if (c is 'X' or 'A')
                return Hand.Rock;
            
            if (c is 'Y' or 'B')
                return Hand.Paper;
            
            if (c is 'Z' or 'C')
                return Hand.Scissors;

            return Hand.Rock;
        }

        private Hand GetDesiredHand(Hand opponent, Result result)
        {
            if (opponent == Hand.Rock)
            {
                if (result == Result.Win) return Hand.Paper;
                if (result == Result.Tie) return Hand.Rock;
                if (result == Result.Lose) return Hand.Scissors;
            }
            
            if (opponent == Hand.Paper)
            {
                if (result == Result.Win) return Hand.Scissors;
                if (result == Result.Tie) return Hand.Paper;
                if (result == Result.Lose) return Hand.Rock;
            }
            
            if (opponent == Hand.Scissors)
            {
                if (result == Result.Win) return Hand.Rock;
                if (result == Result.Tie) return Hand.Scissors;
                if (result == Result.Lose) return Hand.Paper;
            }

            return Hand.Rock;
        }

        private Result GetResult(char c)
        {
            if (c is 'X')
                return Result.Lose;
            
            if (c is 'Y')
                return Result.Tie;
            
            //if (c is 'Z')
                return Result.Win;
        }
    }
}