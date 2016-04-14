using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Obviously, this is not ideal code structure. :)
*/

namespace StructuresAndAlgosTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            //MersennePrimes.FindAllBelow("primes.txt");
            //lcs("lcs.txt");
            reverseGroups("reverseGroups.txt");

            Console.ReadLine();
        }

        private static void newProblem(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    
                }
        }

        private static void reverseGroups(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(';');
                    int[] numList = Array.ConvertAll(inputs[0].Split(','), int.Parse);

                    int groupSize = int.Parse(inputs[1]);
                    int remainder = numList.Count() % groupSize;
                    int groupBeginIndex = 0;
                    int groupEndIndex = groupSize - 1;
                    int groupCount = 0;

                    while (groupEndIndex < numList.Count() - remainder)
                    {
                        if (groupEndIndex > groupBeginIndex)
                        {
                            int temp = numList[groupBeginIndex];
                            numList[groupBeginIndex] = numList[groupEndIndex];
                            numList[groupEndIndex] = temp;

                            groupBeginIndex++;
                            groupEndIndex--;
                        }
                        else
                        {
                            groupCount++;
                            groupBeginIndex = groupCount * groupSize;
                            groupEndIndex = groupBeginIndex + groupSize - 1;
                        }
                    }

                    Console.Write(numList[0]);

                    for (int i = 1; i < numList.Count(); i++)
                    {
                        Console.Write(String.Format(",{0}", numList[i]));
                    }

                    Console.Write("\n");
                }
        }

        private static void reverseElements(int[] elements, int begin, int count)
        {
            int end = begin + count - 1;
            while (begin != end)
            {
                int temp = elements[begin];
                elements[begin] = elements[end];
                elements[end] = temp;

                begin++;
                end--;
            }
        }

        private static void numberOfOnes(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string binary = Convert.ToString(int.Parse(line), 2);
                    int count = 0;
                    foreach (char c in binary.ToCharArray())
                    {
                        if (c == '1')
                        {
                            count++;
                        }
                    }
                    Console.WriteLine(count);
                }
        }

        private static void removeCharacter(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(new string[] { ", " }, StringSplitOptions.None);

                    string output = "";
                    foreach (char c in inputs[0].ToCharArray())
                    {
                        if (inputs[1].IndexOf(c) < 0)
                            output += c;
                    }

                    Console.WriteLine(output);
                }
        }

        private static void stringSearching(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(',');
                    int firstPos = 0;
                    int secondPos = 0;
                    while (firstPos < inputs[0].Length && secondPos < inputs[1].Length)
                    {
                        if (inputs[1][secondPos] == '*')
                        {
                            if (inputs[1].Length - 1 == secondPos || inputs[0][firstPos + 1] == inputs[1][secondPos + 1])
                            {
                                firstPos += 2;
                                secondPos += 2;
                            }
                            else
                            {
                                firstPos++;
                            }
                        }
                        else if (inputs[1][secondPos] == '\\' && inputs[1][secondPos + 1] == '*' && inputs[0][firstPos] == '*')
                        {
                            firstPos++;
                            secondPos += 2;
                        }
                        else
                        {
                            if (inputs[0][firstPos] != inputs[1][secondPos])
                            {
                                firstPos++;
                                secondPos = 0;
                            }
                            else
                            {
                                firstPos++;
                                secondPos++;
                            }
                        }
                    }

                    if (secondPos >= inputs[1].Length)
                    {
                        Console.WriteLine("true");
                    }
                    else
                    {
                        Console.WriteLine("false");
                    }
                }
        }

        private static void lcs(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(';');

                    //remove all chars from string 0, which do not appear in string 1
                    for (int i = 0; i < inputs[0].Length; i++)
                    {
                        if (inputs[1].IndexOf(inputs[0][i]) < 0)
                        {
                            inputs[0] = inputs[0].Remove(i, 1);
                        }
                    }

                    //remove all chars from string 1, which do not appear in string 0
                    for (int i = 0; i < inputs[1].Length; i++)
                    {
                        if (inputs[0].IndexOf(inputs[1][i]) < 0)
                        {
                            inputs[1] = inputs[1].Remove(i, 1);
                        }
                    }

                    /*
                    for (int i = 0; i < inputs[1].Length - 1; i++)
                    {
                        if (inputs[0].IndexOf(inputs[1][i + 1]) < inputs[0].IndexOf(inputs[1][i]))
                        {
                            inputs[1] = inputs[1].Remove(i, 1);
                        }
                    }

                    Console.WriteLine(inputs[1]);
                    */

                    //now, I know the strings contain the same chars, but possibly
                    //out of order (and therefore, not in sequence)
                    string longest = "";
                    if (inputs[0].Length > 0 && inputs[1].Length > 0)
                    {
                        //if both strings have at least 1 char, then I know any single
                        //char will be a sequence (of length 1), repeated in both
                        longest = inputs[0][0].ToString();

                        //foreach char in string 0, use it as the beginning
                        //of a potential sequence in string 1
                        for (int i = 0; i < inputs[0].Length - 1; i++)
                        {
                            string sequence = inputs[0][i].ToString();

                            //check each subsequent char in string 1 to see if it occurs
                            //after the corresponding position of char i in string 0,
                            //thereby increasing the sequence length
                            int subsequent = inputs[1].IndexOf(inputs[0][i]) + 1;
                            while (subsequent < inputs[1].Length)
                            {
                                if (inputs[0].IndexOf(inputs[1][subsequent]) > i)
                                {
                                    sequence += inputs[1][subsequent].ToString();
                                }

                                subsequent++;
                            }

                            for (int j = 0; j < sequence.Length - 1; j++)
                            {
                                if (inputs[0].IndexOf(sequence[j + 1]) < inputs[0].IndexOf(sequence[j]))
                                {
                                    sequence = sequence.Remove(j, 1);
                                }
                            }

                            if (sequence.Length > longest.Length)
                            {
                                longest = sequence;
                            }
                        }
                    }

                    Console.WriteLine(longest);
                }
        }

        private static void happyNumbers(string file)
        {
            //https://www.codeeval.com/open_challenges/39/
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;
                    
                    List<int> steps = new List<int>();
                    bool isHappy = false;
                    while (!isHappy)
                    {
                        int sum = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            int digit = int.Parse(line[i].ToString());
                            sum += digit * digit;
                        }

                        if (sum == 1)
                        {
                            isHappy = true;
                        }
                        else if (steps.Contains(sum))
                        {
                            break;
                        }
                        else
                        {
                            steps.Add(sum);
                            line = sum.ToString();
                            sum = 0;
                        }
                    }

                    if (isHappy)
                    {
                        Console.WriteLine("1");
                    }
                    else
                    {
                        Console.WriteLine("0");
                    }
                }
        }

        private static void rightmostChar(string file)
        {
            //https://www.codeeval.com/open_challenges/31/
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(',');
                    bool found = false;
                    for (int i = inputs[0].Length - 1; i >= 0; i--)
                    {
                        if (inputs[0][i].ToString() == inputs[1])
                        {
                            found = true;
                            Console.WriteLine(i);
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("-1");
                    }
                }
        }

        private static void setIntersection(string file)
        {
            //https://www.codeeval.com/open_challenges/30/
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(';');
                    int[] left = Array.ConvertAll(inputs[0].Split(','), int.Parse);
                    int[] right = Array.ConvertAll(inputs[1].Split(','), int.Parse);

                    int length = 0;
                    int firstElement = 0;
                    for (int posLeft = 0; posLeft < left.Count(); posLeft++)
                    {
                        for (int posRight = 0; posRight < right.Count(); posRight++)
                        {
                            if (left[posLeft] == right[posRight])
                            {
                                //in an intersection
                                if (length == 0)
                                {
                                    //first element of the intersection
                                    firstElement = posLeft;
                                }

                                length++;
                                if (posLeft < left.Count() - 1)
                                {
                                    posLeft++;
                                }
                                else
                                {
                                    //we've reached the end of input left
                                    break;
                                }
                            }
                            else if (length > 0)
                            {
                                //intersection has ended
                                break;
                            }
                        }

                        if (length > 0)
                        {
                            break;
                        }
                    }

                    StringBuilder output = new StringBuilder("");
                    for (int i = 0; i < length; i++)
                    {
                        output.Append(left[firstElement + i]);
                        if (i < length - 1)
                        {
                            output.Append(",");
                        }
                    }

                    Console.WriteLine(output);
                }
        }

        private static void uniqueElements(string file)
        {
            //https://www.codeeval.com/open_challenges/29/
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    int[] inputs = Array.ConvertAll(line.Split(','), int.Parse);

                    Dictionary<int, bool> uniques = new Dictionary<int, bool>();
                    foreach (int value in inputs)
                    {
                        try
                        {
                            uniques.Add(value, true);
                        }
                        catch (ArgumentException) { }
                    }

                    int[] keys = uniques.Keys.ToArray();
                    int last = 1;
                    foreach (int key in keys)
                    {
                        Console.Write(key);
                        if (last < keys.Count())
                        {
                            Console.Write(',');
                        }
                        last++;
                    }
                    Console.Write('\n');
                }
        }

        private static void oddNumbers(string file)
        {
            //https://www.codeeval.com/open_challenges/25/
            for (int i = 1; i <= 99; i++)
            {
                if (i % 2 == 1)
                {
                    Console.WriteLine(i);
                }
            }
        }

        private static void sumOfIntsFromFile(string file)
        {
            //https://www.codeeval.com/open_challenges/24/
            int sum = 0;
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    sum += int.Parse(line);
                }
            Console.WriteLine(sum);
        }

        private static void multiplicationTables()
        {
            //https://www.codeeval.com/open_challenges/23/
            for (int vertical = 1; vertical <= 12; vertical++)
            {
                for (int horizontal = 1; horizontal <= 12; horizontal++)
                {
                    Console.Write(String.Format("{0,4}", vertical * horizontal));
                }
                Console.Write('\n');
            }
        }

        private static void fibonacciSeries(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    Console.WriteLine(fib(int.Parse(line)));
                }
        }

        private static int fib(int index)
        {
            if (index < 2)
            {
                return index;
            }

            return fib(index - 1) + fib(index - 2);
        }

        private static void bitPositions(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    int[] inputs = Array.ConvertAll(line.Split(','), int.Parse);
                    string binary = Convert.ToString(inputs[0], 2);
                    if (binary[binary.Length - inputs[1]] == binary[binary.Length - inputs[2]])
                    {
                        Console.WriteLine("true");
                    }
                    else {
                        Console.WriteLine("false");
                    }
                }
        }

        private static void multipleOfANumber(string file)
        {
            //https://www.codeeval.com/open_challenges/18/
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    int[] inputs = Array.ConvertAll(line.Split(','), int.Parse);
                    int initial = inputs[1];
                    while (inputs[1] < inputs[0])
                    {
                        inputs[1] += initial;
                    }
                    Console.WriteLine(inputs[1]);
                }
        }

        private static void missingForPanagram(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    line = line.ToLower();
                    line = line.Replace(" ", String.Empty);
                    List<char> alphabet = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (alphabet.Contains(line[i]))
                        {
                            alphabet.Remove(line[i]);
                        }
                    }

                    if (alphabet.Count == 0)
                    {
                        Console.WriteLine("NULL");
                    }
                    else {
                        foreach (char letter in alphabet)
                        {
                            Console.Write(letter);
                        }
                        Console.Write('\n');
                    }
                }
        }

        private static void trailingString(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] inputs = line.Split(',');
                    if (inputs[1].Length <= inputs[0].Length && inputs[0].Substring(inputs[0].Length - inputs[1].Length) == inputs[1])
                    {
                        Console.WriteLine("1");
                    }
                    else {
                        Console.WriteLine("0");
                    }
                }
        }

        private static void firstNonRepeated(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    /*
                    for (int i = 0; i < line.Length; i++) {
                        bool isNotRepeated = true;
                        for (int j = 1; j < line.Length; j++) {
                            if (line.ElementAt<char>(j) == line.ElementAt<char>(i)) {
                                isNotRepeated = false;
                                break;
                            }
                        }

                        if (isNotRepeated) {
                            Console.WriteLine(line.ElementAt<char>(i));
                            break;
                        }
                    }
                    */

                    Dictionary<char, int> instances = new Dictionary<char, int>();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (!instances.ContainsKey(line[i]))
                        {
                            instances.Add(line[i], 1);
                        }
                        else {
                            instances[line[i]]++;
                        }
                    }

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (instances[line[i]] == 1)
                        {
                            Console.WriteLine(line[i]);
                            break;
                        }
                    }
                }
        }

        private static void longestTwoLines(string file)
        {
            StreamReader reader = File.OpenText(file);
            int inputNum = int.Parse(reader.ReadLine());
            Dictionary<int, string> lines = new Dictionary<int, string>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                    continue;

                lines.Add(line.Length, line);
            }

            int[] lengths = lines.Keys.ToArray();
            Array.Sort(lengths);

            for (int i = 1; i <= inputNum; i++)
            {
                Console.WriteLine(lines[lengths[lengths.Count() - i]]);
            }
        }

        private static void isArmstrong(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    int input = int.Parse(line);
                    int sum = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        int digitToPower = (int)Math.Pow(int.Parse(line[i].ToString()), line.Length);
                        sum += digitToPower;
                    }

                    if (input == sum)
                    {
                        Console.WriteLine("True");
                    }
                    else {
                        Console.WriteLine("False");
                    }
                }
        }

        private static void sumOfDigits(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    int sum = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        sum += int.Parse(line.Substring(i, 1));
                    }

                    Console.WriteLine(sum);
                }
        }

        private static void lowercase(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    Console.WriteLine(line.ToLower());
                }
        }

        private static void footballFans(string file)
        {
            using (StreamReader reader = File.OpenText(file))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (null == line)
                        continue;

                    string[] countryInputs = line.Split(new string[] { " | " }, StringSplitOptions.None);
                    Dictionary<int, List<int>> supportingCountriesByClubNum = new Dictionary<int, List<int>>();
                    int countryNum = 1;
                    foreach (string country in countryInputs)
                    {
                        int[] supported = Array.ConvertAll(country.Split(' '), int.Parse);
                        foreach (int club in supported)
                        {
                            if (!supportingCountriesByClubNum.ContainsKey(club))
                            {
                                supportingCountriesByClubNum.Add(club, new List<int>() { countryNum });
                            }
                            else {
                                supportingCountriesByClubNum[club].Add(countryNum);
                            }
                        }
                        countryNum++;
                    }

                    int[] clubNums = supportingCountriesByClubNum.Keys.ToArray();
                    Array.Sort(clubNums);
                    foreach (int clubNum in clubNums)
                    {
                        Console.Write(clubNum + ":");
                        int last = 0;
                        supportingCountriesByClubNum[clubNum].Sort();
                        foreach (int supportingCountry in supportingCountriesByClubNum[clubNum])
                        {
                            Console.Write(supportingCountry);
                            last++;

                            if (last < supportingCountriesByClubNum[clubNum].Count())
                            {
                                Console.Write(",");
                            }
                            else {
                                Console.Write("; ");
                            }
                        }
                    }

                    Console.WriteLine();
                }
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

    /*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //keep track of all mersenne primes globally, so we only have to 
        //calc new ones when our input number grows because our
        //max discovered mersenne prime
        List<int> mersennePrimes = new List<int>();
        //we know 3 will be in all
        mersennePrimes.Add(3);
            
        using (StreamReader reader = File.OpenText(args[0]))
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (null == line)
                continue;
            
            int inputNum = int.Parse(line);
            StringBuilder allBelow = new StringBuilder("3");

            int lastPrimeAdded = 3;

            //go through our globally-recorded list of mersenne primes,
            //adding all which are below the input num
            if (mersennePrimes.Count() > 1)
            {
                int i = 1;
                while (i < mersennePrimes.Count() && inputNum > mersennePrimes[i])
                {
                    allBelow.Append(string.Format(", {0}", mersennePrimes[i]));
                    lastPrimeAdded = mersennePrimes[i];
                    i++;
                }
                
                if(i < mersennePrimes.Count())
                {
                    //we still have more mersenne primes saved globally, but they
                    //are >= our read-in number, so we're done
                    Console.WriteLine(allBelow);
                    continue;
                }
            }

            //if we haven't continued yet, then our input num is greater
            //than our maximum, globally-recorded mersenne prime, so we'll need to
            //calculate more
            for (int i = ++lastPrimeAdded; i < inputNum; i++)
            {
                if (isMersennePrime(i))
                {
                    allBelow.Append(string.Format(", {0}", i));
                    mersennePrimes.Add(i);
                }
            }

            Console.WriteLine(allBelow);
        }
    }
    
    private static bool isMersennePrime(int num)
    {
        //a candidate must first, be a prime number
        if (isPrime(num)) {
            //and there must be some int n, where 2^n - 1 == candidate
            for (int powerOfTwo = 2; Math.Pow(2, powerOfTwo) -1 <= num; powerOfTwo++)
            {
                if (Math.Pow(2, powerOfTwo) - 1 == num)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool isPrime(int num)
    {
        if (num % 2 == 0 || num % Math.Sqrt(num) == 0)
        {
            return false;
        }

        for (int i = 3; i < Math.Ceiling(Math.Sqrt(num)); i += 2)
        {
            if (num % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}
    */

    class MersennePrimes
    {
        public static void FindAllBelow(string file)
        {
            //keep track of all mersenne primes globally, so we only have to 
            //calc new ones when our input number grows because our
            //max discovered mersenne prime
            List<int> mersennePrimes = new List<int>();
            //we know 3 will be in all
            mersennePrimes.Add(3);

            StreamReader reader = File.OpenText(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                int inputNum = int.Parse(line);
                StringBuilder allBelow = new StringBuilder("3");

                int lastPrimeAdded = 3;

                //go through our globally-recorded list of mersenne primes,
                //adding all which are below the input num
                if (mersennePrimes.Count() > 1)
                {
                    int i = 1;
                    while (i < mersennePrimes.Count() && inputNum > mersennePrimes[i])
                    {
                        allBelow.Append(string.Format(", {0}", mersennePrimes[i]));
                        lastPrimeAdded = mersennePrimes[i];
                        i++;
                    }
                    
                    if(i < mersennePrimes.Count())
                    {
                        //we still have more mersenne primes saved globally, but they
                        //are >= our read-in number, so we're done
                        Console.WriteLine(allBelow);
                        continue;
                    }
                }

                //if we haven't continued yet, then our input num is greater
                //than our maximum, globally-recorded mersenne prime, so we'll need to
                //calculate more
                for (int i = ++lastPrimeAdded; i < inputNum; i++)
                {
                    if (isMersennePrime(i))
                    {
                        allBelow.Append(string.Format(", {0}", i));
                        mersennePrimes.Add(i);
                    }
                }

                Console.WriteLine(allBelow);
            }
        }

        private static bool isMersennePrime(int num)
        {
            //a candidate must first, be a prime number
            if (isPrime(num)) {
                //and there must be some int n, where 2^n - 1 == candidate
                for (int powerOfTwo = 2; Math.Pow(2, powerOfTwo) -1 <= num; powerOfTwo++)
                {
                    if (Math.Pow(2, powerOfTwo) - 1 == num)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool isPrime(int num)
        {
            if (num % 2 == 0 || num % Math.Sqrt(num) == 0)
            {
                return false;
            }

            for (int i = 3; i < Math.Ceiling(Math.Sqrt(num)); i += 2)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
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
