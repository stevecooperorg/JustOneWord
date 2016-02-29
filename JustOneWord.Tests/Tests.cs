using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace JustOneWord.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void JustOneWord_Nevertheless_Neverland()
        {
            var dict = new HashSet<string>();
            dict.Add("never");
            dict.Add("land");
            dict.Add("neverland");
            dict.Add("the");
            dict.Add("less");
            dict.Add("nevertheless");
           
            var searcher = new Searcher();
            var twoPartWords = searcher.words(2, dict, dict).ToArray();
            Assert.AreEqual("-never-land-", twoPartWords[0]);

            var threePartWords = searcher.words(3, dict, dict).ToArray();
            Assert.AreEqual("-never-the-less-", threePartWords[0]);
        }

        [TestMethod]
        public void JustOneWord_EntireDictionary()
        {
            var resourceFolder = @"C:\src\JustOneWord\JustOneWord\Resources";
            var loader = new DictionaryLoader();
            var candidates = loader.Load(resourceFolder, "english-words.*");
            var officialWords = loader.Load(resourceFolder, "sowpods.txt");
            officialWords.Add("A");

            var searcher = new Searcher();

            var all = new List<string>();
            foreach (var word in searcher.words(3, candidates, officialWords))
            {
                all.Add(word);
            }

            all = all.OrderBy(s => -s.Length).ToList();

            //*
            var funny = new[] { "BUM", "POO", "WEE", "CUM" }
                .Select(s => $"-{s}-")
                .ToArray();

            all.RemoveAll(s => !funny.Any(f => s.Contains(f)));
            //*/
            foreach (var word in all)
            {
                //word = word.Substring(2, word.Length - 2);
                var inSowpods = officialWords.Contains(word.Replace("-", ""));
                var display = inSowpods ? word.ToUpperInvariant() : word.ToLowerInvariant();
                Console.WriteLine($"{display}  - {word.Length-4}");
            }
        }


    }
}
