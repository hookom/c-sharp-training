using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Obviously, this is far from ideal code structure. :)
*/

namespace StructuresAndAlgosTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Substitute.GetValue("asdfghjkli", "weorauadsdaf"));

            //CapitalEncodeFromBinary.Encode("encodeCapital.txt");

            //Halloween.CandyCount("halloween.txt");

            //Pirates.SpotGame("pirates.txt");

            //Primes.FindAllBelow("primes.txt");

            //FizzBuzz.Game("fizzbuzz.txt");

            //SentenceWords.Reverse("reverseSentence.txt");

            //Antivirus.Worked("virus.txt");

            //Cards.War("cards.txt");

            //NumberSequences.Detect("numSeqs.txt");

            //MthFromLast.FindElement("strings.txt");

            //PalindromeFromAddition.Create("palindrome.txt");

            Quicksort.FindPivots("quicksort.txt");

            Console.ReadLine();
        }
    }

    class Quicksort
    {
        private static int Pivots;

        public static void FindPivots(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }
                
                int[] input = Array.ConvertAll(line.Split(' '), int.Parse);
                
                quicksort(input, 0, input.Length - 1);

                //foreach (int i in input)
                //{
                //    Console.Write(i + " ");
                //}
                //Console.WriteLine();

                Console.WriteLine(Pivots);

                Pivots = 0;
            }
        }

        private static void quicksort(int[] input, int leftBound, int rightBound)
        {
            if (leftBound < rightBound)
            {
                int pivot = partition(input, leftBound, rightBound);
                Pivots++;
                quicksort(input, leftBound, pivot - 1);
                quicksort(input, pivot + 1, rightBound);
            }
        }

        private static int partition(int[] input, int leftBound, int rightBound)
        {
            int pivot = input[leftBound];

            int i = leftBound;
            int j = rightBound;
            while (true)
            {
                while (input[i] < pivot)
                {
                    i++;
                }
                while (input[j] > pivot)
                {
                    j--;
                }

                if (i >= j)
                {
                    return j;
                }
                else
                {
                    int temp = input[i];
                    input[i] = input[j];
                    input[j] = temp;
                }
            }
        }
    }

    class PalindromeFromAddition
    {
        public static void Create(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                line = Int64.Parse(line).ToString();

                int additions = 0;
                while (!isPalindrome(line))
                {
                    char[] elements = line.ToCharArray();
                    Array.Reverse(elements);
                    string reversed = new string(elements);
                    line = (Int64.Parse(line) + Int64.Parse(reversed)).ToString();

                    additions++;
                }

                Console.WriteLine(additions + " " + line);
            }
        }

        private static bool isPalindrome(string testme)
        {
            int begin = 0;
            int end = testme.Length - 1;
            while (begin < end)
            {
                if (testme.ElementAt(end) != testme.ElementAt(begin))
                {
                    return false;
                }

                begin++;
                end--;
            }

            return true;
        }
    }

    class MthFromLast
    {
        public static void FindElement(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                List<string> input = line.Split(' ').ToList<string>();
                int index = int.Parse(input.ElementAt(input.Count - 1));
                input.Remove(input.ElementAt(input.Count -1));

                string found = "";

                if (input.Count >= index)
                {
                    found = input.ElementAt(input.Count - index);
                }

                Console.WriteLine(found);
            }
        }
    }

    class NumberSequences
    {
        public static void Detect(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string[] input = line.Split(' ');

                int position = 0;
                int compare = position + 1;
                bool noSequence = true;

                //test for sequence up until final 2 values (position:length-2, compare:length-1)
                while (position < input.Length - 1 && noSequence)
                {
                    //if 6 elements remain, each can only be max length 3
                    while (compare - position <= (input.Length - position) / 2)
                    {
                        //if the first nums don't match, don't bother looking further
                        if (input[position] == input[compare])
                        {
                            noSequence = false;
                            //check each subsequent num in the first "string" to the corresponding num in the comparison "string"
                            for (int i = 1; i < compare - position; i++)
                            {
                                if (input[position + i] != input[compare + i])
                                {
                                    noSequence = true;
                                }
                            }

                            //if no mismatches were found, we have the first repeated sequence
                            if (!noSequence)
                            {
                                StringBuilder first = new StringBuilder(input[position]);
                                for (int i = 1; i < compare - position; i++)
                                {
                                    first.Append(" ");
                                    first.Append(input[position + i]);
                                }

                                Console.WriteLine(first);
                                //exit inner loop, outer loop will exit because noSequence is now false
                                break;
                            }
                        }

                        compare++;
                    }

                    //couldn't match the beginning num from first "string" wwith
                    //beginning of any possible comparison "string", so move to next position
                    position++;
                    compare = position + 1;
                }

                //if no repeating sequences were found, print a blank line
                if (noSequence)
                {
                    Console.WriteLine();
                }
            }
        }
    }

    class Cards
    {
        public static void War(string file)
        {
            Dictionary<string, int> cardValues = new Dictionary<string, int>();
            for (int i = 2; i < 11; i++)
            {
                cardValues.Add(i.ToString(), i);
            }
            cardValues.Add("J", 11);
            cardValues.Add("Q", 12);
            cardValues.Add("K", 13);
            cardValues.Add("A", 14);

            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string[] inputs = line.Split(new string[] { " | " }, StringSplitOptions.None);
                string trump = inputs[1];

                string[] cards = inputs[0].Split(' ');
                string cardOne = cards[0];
                string cardTwo = cards[1];
                string winner;

                if (cardOne.Substring(cardOne.Length -1) == trump && cardTwo.Substring(cardOne.Length - 1) != trump)
                {
                    winner = cardOne;
                }
                else if (cardOne.Substring(cardOne.Length - 1) != trump && cardTwo.Substring(cardOne.Length - 1) == trump)
                {
                    winner = cardTwo;
                }
                else if (cardValues[cardOne.Substring(0, cardOne.Length - 1)] > cardValues[cardTwo.Substring(0, cardTwo.Length - 1)])
                {
                    winner = cardOne;
                }
                else if (cardValues[cardTwo.Substring(0, cardTwo.Length - 1)] > cardValues[cardOne.Substring(0, cardOne.Length - 1)])
                {
                    winner = cardTwo;
                }
                else
                {
                    winner = cardOne + " " + cardTwo;
                }

                Console.WriteLine(winner);
            }
        }
    }

    class Antivirus
    {
        public static void Worked(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string[] inputs = line.Split(new string[] { " | " }, StringSplitOptions.None);

                int virusTotal = 0;
                foreach (string virusVal in inputs[0].Split(' '))
                {
                    virusTotal += Convert.ToInt32(virusVal, 16);
                }

                int antivirusTotal = 0;
                foreach (string antivirusVal in inputs[1].Split(' '))
                {
                    antivirusTotal += Convert.ToInt32(antivirusVal, 2);
                }

                if (antivirusTotal >= virusTotal)
                {
                    Console.WriteLine("True");
                }
                else
                {
                    Console.WriteLine("False");
                }
            }
        }
    }

    class SentenceWords
    {
        public static void Reverse(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string[] words = line.Split(' ');
                StringBuilder reversed = new StringBuilder(words[words.Length - 1]);

                for (int i = words.Length - 2; i >= 0; i--)
                {
                    reversed.Append(" ");
                    reversed.Append(words[i]);
                }

                Console.WriteLine(reversed);
            }
        }
    }

    class FizzBuzz
    {
        public static void Game(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                string[] inputs = line.Split(' ');

                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                int count = int.Parse(inputs[2]);

                StringBuilder output = new StringBuilder();
                for (int i = 1; i <= count; i++)
                {
                    if(i > 1)
                    {
                        output.Append(' ');
                    }

                    if (i % x == 0 && i % y == 0)
                    {
                        output.Append("FB");
                    }
                    else if (i % x == 0)
                    {
                        output.Append("F");
                    }
                    else if (i % y == 0)
                    {
                        output.Append("B");
                    }
                    else
                    {
                        output.Append(i);
                    }
                }

                Console.WriteLine(output);
            }
        }
    }

    class Primes
    {
        public static void FindAllBelow(string file)
        {
            List<int> primes = new List<int>();
            primes.Add(3);

            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                int readNum = int.Parse(line);
                StringBuilder allBelow = new StringBuilder("3");

                int lastPrimeAdded = 5;

                if (primes.Count() > 1)
                {
                    int i = 1;
                    while (i < primes.Count() && readNum > primes[i])
                    {
                        allBelow.Append(string.Format(", {0}", primes[i]));
                        lastPrimeAdded = primes[i];
                        i++;
                    }
                    
                    if(i < primes.Count())
                    {
                        //we still have record of more primes, but they are >= our read-in number, so we're done
                        Console.WriteLine(allBelow);
                        continue;
                    }
                }

                for (int i = lastPrimeAdded; i < readNum; i++)
                {
                    if (isPrime(i))
                    {
                        allBelow.Append(string.Format(", {0}", i));
                        primes.Add(i);
                    }
                }

                Console.WriteLine(allBelow);
            }
        }

        private static bool isPrime(int num)
        {
            bool primeBool = true;

            int factor = num / 2;

            for (int i = 2; i < factor; i++)
            {
                if((num % i) == 0)
                {
                    primeBool = false;
                }
            }

            return primeBool;
        }
    }

    class Pirates
    {
        public static void SpotGame(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                {
                    continue;
                }

                int count = int.Parse(line.Substring(line.IndexOf('|') + 2));
                int countCopy = count;

                string nameString = line.Substring(0, line.IndexOf('|') - 1);
                List<string> players = new List<string> (nameString.Split(' '));

                while (players.Count() > 1)
                {
                    if (countCopy > players.Count())
                    {
                        countCopy = countCopy % players.Count();
                        if(countCopy == 0)
                        {
                            countCopy = players.Count();
                        }
                    }

                    players.RemoveAt(countCopy - 1);
                    countCopy = count;
                }

                Console.WriteLine(players[0]);
            }
        }
    }

    class Halloween
    {
        public static void CandyCount(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                {
                    continue;
                }
                string[] seps = new string[] { ", " };
                string[] pairs = line.Split(seps, StringSplitOptions.None);

                Dictionary<string, int> treats = new Dictionary<string, int>();

                foreach (string pair in pairs)
                {
                    treats.Add( pair.Substring(0, pair.IndexOf(":")), int.Parse(pair.Substring(pair.IndexOf(' ') + 1)) );
                }

                int totalTreats = ((treats["Vampires"] * 3) + (treats["Zombies"] * 4) + (treats["Witches"] * 5)) * treats["Houses"] ;
                int totalKids = treats["Vampires"] + treats["Zombies"] + treats["Witches"];

                Console.WriteLine(totalTreats / totalKids);
            }
        }
    }

    class CapitalEncodeFromBinary
    {
        public static void Encode(string file)
        {
            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                {
                    continue;
                }

                int index = line.IndexOf(" ");
                string word = line.Substring(0, index);
                string binary = line.Substring(index + 1, line.Length - index - 1);

                char[] wordAsArray = word.ToCharArray();
                char[] binaryAsArray = binary.ToCharArray();

                StringBuilder encoded = new StringBuilder();

                for (int i = 0; i < wordAsArray.Length; i++)
                {
                    if (binaryAsArray[i] == '1')
                    {
                        encoded.Append(wordAsArray[i].ToString().ToUpper());
                    }
                    else
                    {
                        encoded.Append(wordAsArray[i]);
                    }
                }

                Console.WriteLine(encoded);
            }
        }
    }

    class Substitute
    {
        //takes a key (10 unique chars) and code - decodes to an int
        public static int GetValue(string key, string code)
        {
            StringBuilder returnValue = new StringBuilder();
            char[] codeAsArray = code.ToCharArray();

            for (int i = 0; i < codeAsArray.Length; i++)
            {
                if (key.Contains(codeAsArray[i]))
                {
                    int decodedNum = key.IndexOf(codeAsArray[i]) + 1;
                    if (decodedNum == 10)
                    {
                        decodedNum = 0;
                    }

                    returnValue.Append(decodedNum);
                }
            }

            return int.Parse(returnValue.ToString());
        }
    }
}