using System.IO;

namespace AdventOfCode2023
{
    public abstract class Solution
    {
        const string INPUT_FOLDER_PATH = "D:/Work/Personal/AdventOfCode/2023/AdventOfCode2023/AdventOfCode2023/Inputs";

        protected abstract string FileName { get; }
        public abstract void Run();

        protected string[] GetInputLines()
        {
            return GetLines(GetInput(FileName));
        }

        private static string GetInput(string filename)
        {
            return File.ReadAllText($"{INPUT_FOLDER_PATH}/{filename}.txt");
        }

        private static string[] GetLines(string input)
        {
            string inputProcessed = input.Replace("\r", "");
            return inputProcessed.Split("\n");
        }
    }
}