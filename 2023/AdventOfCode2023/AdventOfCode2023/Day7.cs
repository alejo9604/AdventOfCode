using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023
{
    public class Day7 : Solution
    {
        protected override string FileName => "day7";

        enum ECamelHandType{ Five, Four, FullHouse, Three, TwoPar, OnePar, SingleHigh }
        class CamelHand : IComparable<CamelHand>
        {
            private const char WILD_CARD = 'J';
            private static readonly Dictionary<char, int> OrderOfImportance = new()
            {
                { 'A', 13 },
                { 'K', 12 },
                { 'Q', 11 },
                //{ 'J', 10 }, // Part-1
                { 'T', 9 },
                { '9', 8 },
                { '8', 7 },
                { '7', 6 },
                { '6', 5 },
                { '5', 4 },
                { '4', 3 },
                { '3', 2 },
                { '2', 1 },
                { 'J', 0 }, // Part-2
            };

            public string Hand;
            public int Bid;

            public ECamelHandType Type;
            
            public CamelHand(string hand, string bid)
            {
                Hand = hand;
                Bid = int.Parse(bid);
                Type = GetHandType(Hand);
            }

            public override string ToString() => $"{Hand} {Type} - ${Bid}";
            
            ECamelHandType GetHandType(string hand)
            {
                Dictionary<char, byte> count = new Dictionary<char, byte>();
                byte wildCardCount = 0;
                foreach (char c in hand)
                {
                    if (c == WILD_CARD)
                        wildCardCount++;

                    if(count.ContainsKey(c))
                        count[c] += 1;
                    else
                        count.Add(c, 1);
                }

                char firstChar = hand[0];
                switch (count.Count)
                {
                    case 1:
                        return ECamelHandType.Five;
                    case 2:
                        if (wildCardCount > 0)
                            return ECamelHandType.Five;
                        return count[firstChar] is 4 or 1 ? ECamelHandType.Four : ECamelHandType.FullHouse;
                    case 3:
                        char toCheck = count[firstChar] == 1 ? hand[1] : firstChar;
                        if (count[toCheck] != 2)
                            return wildCardCount > 0 ? ECamelHandType.Four : ECamelHandType.Three;
                        if (wildCardCount > 0)
                            return wildCardCount > 1 ? ECamelHandType.Four : ECamelHandType.FullHouse;
                        return ECamelHandType.TwoPar;
                    case 4:
                        return wildCardCount > 0 ?  ECamelHandType.Three : ECamelHandType.OnePar;
                    default:
                        return wildCardCount > 0 ?  ECamelHandType.OnePar : ECamelHandType.SingleHigh;
                }
            }

            public int CompareTo(CamelHand other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;

                if (Type != other.Type)
                    return other.Type.CompareTo(Type);

                for (int i = 0; i < Hand.Length; i++)
                {
                    int weight = OrderOfImportance[Hand[i]];
                    int otherWeight = OrderOfImportance[other.Hand[i]];
                    if(weight == otherWeight)
                        continue;

                    return weight.CompareTo(otherWeight);
                }
                
                return 0;
            }
        }
        
        class CamelHandComparer : IComparer<CamelHand>
        {
            public int Compare(CamelHand x, CamelHand y) => x.CompareTo(y);
        }
        
        public override void Run()
        {
            string[] list = GetInputLines();

            List<CamelHand> hands = new List<CamelHand>(list.Length);
            foreach (var l in list)
            {
                string[] values = l.Split(' ');
                hands.Add(new CamelHand(values[0], values[1]));
            }

            hands = hands.OrderBy(s => s, new CamelHandComparer()).ToList();

            int total = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                total += (hands[i].Bid * (i + 1));
                Console.WriteLine(hands[i]);
            }
            
            Console.WriteLine(total);
        }
        
    }
    
}