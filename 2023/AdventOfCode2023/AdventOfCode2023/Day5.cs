using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day5 : Solution
    {
        protected override string FileName => "day5";

        class SeedRange
        {
            public ulong Start;
            public ulong End;
            public bool Discard;

            public SeedRange(ulong start, ulong end)
            {
                Start = start;
                End = end;

                if (start > end)
                    Console.WriteLine("Start bigger than error");
            }
            
            public SeedRange(SeedRange seedRange) : this(seedRange.Start, seedRange.End) { }

            public SeedRange(ulong start, ulong end, bool discard) : this(start, end)
            {
                Discard = discard;
            }

            public bool Overlap(SeedRange range)
            {
                return Start <= range.End && End >= range.Start;
            }

            public override string ToString()
            {
                return $"{Start} - {End}";
            }
        }
        
        public override void Run()
        {
            string[] list = GetInputLines();

            List<SeedRange> ranges = new();
            List<SeedRange> rangesAfterSegment = new();

            string[] seeds = list[0].Split(':')[1].Split(' ', StringSplitOptions.TrimEntries);

            // i:0 it's empty. ignoring
            for (int i = 1; i < seeds.Length; i++)
            {
                ulong start = ulong.Parse(seeds[i]);
                ulong end = start + ulong.Parse(seeds[i+1]) - 1;
                i++;
                
                ranges.Add(new SeedRange(start, end));
            }

            ranges = MergeRanges(ranges);
            
            int level = 1;
            for (int i = 3; i < list.Length; i++)
            {
                string l = list[i];
                if (string.IsNullOrEmpty(l))
                {
                    i++;

                    ranges = ranges.Concat(rangesAfterSegment).ToList();
                    rangesAfterSegment.Clear();
                    //ranges = MergeRanges(ranges); //Possible optimization, but for the input seems it's not needed
                    continue;
                }
            
                string[] entries = l.Split(' ', StringSplitOptions.TrimEntries);
                ranges = ProcessMap(ranges, 
                    ulong.Parse(entries[0]), ulong.Parse(entries[1]), ulong.Parse(entries[2]),
                    rangesAfterSegment);
            }
            
            
            ranges = ranges.Concat(rangesAfterSegment).ToList();

            ulong min = ulong.MaxValue;
            foreach (SeedRange range in ranges)
            {
                //Console.WriteLine(range);
                if (range.Start < min)
                    min = range.Start;
            }
            
            Console.WriteLine(min);
        }

        private List<SeedRange> MergeRanges(List<SeedRange> inputList)
        {
            var tempArray = inputList.OrderBy(x => x.Start).ToArray();
            List<SeedRange> newRange = new() { tempArray[0]};
            for (int i = 1; i < tempArray.Length; i++)
            {
                if (newRange[^1].End >= tempArray[i].Start)
                    newRange[^1] = new SeedRange(newRange[^1].Start, tempArray[i].End);
                else
                    newRange.Add(new SeedRange(tempArray[i]));
            }

            return newRange;
        }

        private List<SeedRange> ProcessMap(List<SeedRange> ranges, ulong destination, ulong start, ulong lenght, List<SeedRange> newRanges)
        {
            SeedRange startRange = new(start, start + lenght - 1);
            List<SeedRange> rangesAfterSplit = new();
            foreach (var range in ranges)
            {
                if (!range.Overlap(startRange))
                {
                    rangesAfterSplit.Add(range);
                    continue;
                }

                if (range.Start < startRange.Start)
                    rangesAfterSplit.Add(new SeedRange(range.Start, startRange.Start - 1));
                if (range.End > startRange.End)
                    rangesAfterSplit.Add( new SeedRange(startRange.End + 1, range.End));

                ulong startValue = Math.Max(range.Start, startRange.Start);
                ulong endValue = Math.Min(range.End, startRange.End);

                newRanges.Add(new SeedRange(destination + startValue - start, destination + endValue - start));
            }

            return rangesAfterSplit;
        }

#region Part-1

        private void Part1(string[] list)
        {
            Dictionary<int, List<ulong>> map = new();

            string[] seeds = list[0].Split(':')[1].Split(' ', StringSplitOptions.TrimEntries);
            foreach (string seed in seeds)
            { 
                if(string.IsNullOrEmpty(seed) || !int.TryParse(seed, out int seedNumber)) 
                    continue; 
                map.Add(seedNumber, new(){ (ulong) seedNumber });
            }

            int level = 1;
            
            //Ignore 1-line : seeds values. 2-line: empty. 3-line: seed-to-soil text
            for (int i = 3; i < list.Length; i++)
            {
                string l = list[i];
                if (string.IsNullOrEmpty(l))
                {
                    i++;
                    level++;
                    ApplyNoMatches(map, level);
                    continue;
                }

                string[] entries = l.Split(' ', StringSplitOptions.TrimEntries);
                ProcessMap(map, ulong.Parse(entries[0]), ulong.Parse(entries[1]), ulong.Parse(entries[2]), level);
            }
            
            //Las check
            level++;
            ApplyNoMatches(map, level);

            //Print
            // foreach (int key in map.Keys)
            // {
            //     var seedList = map[key];
            //     for (int i = 0; i < seedList.Count; i++)
            //     {
            //         if(i > 0)
            //             Console.Write($" -> {seedList[i]}");
            //         else
            //             Console.Write(seedList[i]);
            //     }
            //     Console.WriteLine();
            // }
            Console.WriteLine(GetMin(map));
        }

        private void ApplyNoMatches(Dictionary<int, List<ulong>> map, int desiredLenght)
        {
            foreach (int key in map.Keys)
            {
                List<ulong> trace = map[key];
                if(trace.Count < desiredLenght)
                    trace.Add(trace[^1]);
                else if(trace.Count > desiredLenght)
                    Console.WriteLine($"Extra number with {trace[0]}");
            }
        }
        
        private void ProcessMap(Dictionary<int, List<ulong>> map, ulong destination, ulong start, ulong range, int level)
        {
            foreach (int key in map.Keys)
            {
                List<ulong> trace = map[key];
                if(trace.Count > level)
                    continue;
                
                (bool success, ulong mappedValue) = ProcessValue(trace[^1], destination, start, range);
                if(success)
                    trace.Add(mappedValue);
            }
        }
        
        private (bool inRange, ulong mappedValue) ProcessValue(ulong value, ulong destination, ulong start, ulong range)
        {
            if (value >= start && value < start + range)
                return (true, destination + value - start);
            return (false, 0);
        }

        private ulong GetMin(Dictionary<int, List<ulong>> map)
        {
            ulong min = int.MaxValue;
            foreach (int key in map.Keys)
            {
                ulong value = map[key][^1];
                if (value < min)
                    min = value;
            }

            return min;
        }
#endregion
    }
}