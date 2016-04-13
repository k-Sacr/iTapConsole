using System;
using System.IO;

namespace iTapConsole
{
    class Program
    {
        static void Main()
        {

            Trie trie = new Trie();
            TextReader reader = new StreamReader(Console.OpenStandardInput());

            int lines = int.Parse(reader.ReadLine());

            for (int i = 0; i < lines; i++)
            {
                trie.AddValue(reader.ReadLine());
            }

            lines = int.Parse(reader.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                string prefix = reader.ReadLine();
                var words = trie.FindValues(prefix);
                if (words != null)
                    foreach (var word in words)
                    {
                        Console.WriteLine(word);
                    }
                if (i + 1 < lines)
                    Console.WriteLine();
            }
        }
    }
}
