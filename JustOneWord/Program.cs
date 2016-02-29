using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candidates = System.Collections.Generic.HashSet<string>;// System.Collections.Generic.Dictionary<string, string>;

namespace JustOneWord
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordlistPath = @"\\Mac\Home\Downloads\scowl-2016.01.19\final\english-words.95";
            if (!File.Exists(wordlistPath))
            {
                throw new FileNotFoundException("no dictionary found", wordlistPath);
            }


        }
    }

    public class DictionaryLoader
    {
        public HashSet<string> Load(string wordlistPath, string pattern)
        {
            var items = new HashSet<string>();

            foreach (var file in Directory.GetFiles(wordlistPath, pattern))
            {
                var content = File.ReadAllLines(file);

                foreach (var word in content)
                {
                    items.Add(word.ToUpperInvariant());
                }
            }

            return items;
        }
    }

    public class Searcher
    {
        public IEnumerable<string> words(int numberOfWords, HashSet<string> candidates, HashSet<string> parentDictionary)
        {
            foreach (var word in candidates)
            {
                foreach (var result in wordsAtDepth(numberOfWords, "", word, parentDictionary))
                {
                    yield return result + "-";
                }
            }

        }

        public IEnumerable<string> wordsAtDepth(int depth, string previousPrefix, string remainder, HashSet<string> master)
        {
            if (depth == 1)
            {
                if (master.Contains(remainder))
                {
                    yield return previousPrefix + "-" + remainder;
                }
            }
            else
            {
                for (var l = 1; l < remainder.Length - depth; l++)
                {
                    var prefix = remainder.Substring(0, l);
                    var candidate = remainder.Substring(l);
                    if (master.Contains(prefix))
                    {
                        var newPrefix = $"{previousPrefix}-{prefix}";
                        foreach (var result in wordsAtDepth(depth - 1, newPrefix, candidate, master))
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
        
    }
}
