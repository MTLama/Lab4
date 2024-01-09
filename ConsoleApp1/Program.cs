using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var path = "H:/hebron/lab4/text.txt";
        var dict = new SortedDictionary<string, int>();
        //используя кетч создаем коллецкию и наполняем ее стрим ридеро из файла 
        try
        {
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open));

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                var wordsAndPunctuation = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in wordsAndPunctuation)
                {
                    var cleanedItem = new string(item.Where(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c)).ToArray());

                    if (dict.ContainsKey(cleanedItem))
                        dict[cleanedItem]++;
                    else
                        dict.Add(cleanedItem, 1);
                }
            }

            sr.Close();

            Console.WriteLine("Content of the file:");
            foreach (var item in dict)
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        //печатаем частоту использования слов
        Console.WriteLine("\nFrequency of usage in the text:");

        var punctuationFrequency = dict.Where(item => item.Key.Length == 1 && char.IsPunctuation(item.Key[0]))
                                       .OrderBy(item => item.Key);

        foreach (var item in punctuationFrequency)
        {
            Console.WriteLine("{0}: {1}", item.Key, item.Value);
        }

        // печатаем частоту использования знаков припинания
        Console.WriteLine("\nLINQ queries:");

        var commaFrequency = dict.Where(item => item.Key.Contains(","))
                                .Select(item => item.Value)
                                .FirstOrDefault();

        var periodFrequency = dict.Where(item => item.Key.Contains("."))
                                 .Select(item => item.Value)
                                 .FirstOrDefault();

        var questionMarkFrequency = dict.Where(item => item.Key.Contains("?"))
                                       .Select(item => item.Value)
                                       .FirstOrDefault();

        var exclamationMarkFrequency = dict.Where(item => item.Key.Contains("!"))
                                          .Select(item => item.Value)
                                          .FirstOrDefault();

        var dashFrequency = dict.Where(item => item.Key.Contains("-"))
                               .Select(item => item.Value)
                               .FirstOrDefault();

        Console.WriteLine("Frequency of commas: {0}", commaFrequency);
        Console.WriteLine("Frequency of periods: {0}", periodFrequency);
        Console.WriteLine("Frequency of question marks: {0}", questionMarkFrequency);
        Console.WriteLine("Frequency of exclamation marks: {0}", exclamationMarkFrequency);
        Console.WriteLine("Frequency of dashes: {0}", dashFrequency);

        Console.ReadKey();
    }
}
