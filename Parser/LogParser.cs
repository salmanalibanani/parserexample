using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser
{
    public class LogParser
    {
        private string _fileName;
        private IList<RegexHelperBase> _regexList;
        private int _topCount;

        public delegate IEnumerable<string> GetLines(string fileName);
        private GetLines _getLinesHandler;

        public LogParser(string fileName, IList<RegexHelperBase> regexList, int topCount, GetLines getLinesHandler)
        {
            _fileName = fileName;
            _regexList = regexList;
            _topCount = topCount;
            _getLinesHandler = getLinesHandler;
        }

        public IList<TopNList> GetLists()
        {
            int regexCount = _regexList.Count;

            var dictionaryList = new List<Dictionary<string, int>>();
            var returnList = new List<TopNList>();

            for (var i = 0; i < regexCount; i++)
            {
                dictionaryList.Add(new Dictionary<string, int>());
                returnList.Add(new TopNList(_topCount, _regexList[i].Description));
            }

            var lines = _getLinesHandler(_fileName); 

            foreach (var line in lines)
            {
                for (var i = 0; i < regexCount; i++)
                {
                    string match;

                    if (_regexList[i].Find(line, out match))
                    {
                        AddToDictionary(dictionaryList[i], match);
                    }
                }
            }

            for (var i = 0; i < regexCount; i++)
            {
                foreach (var kvp in dictionaryList[i])
                {
                    returnList[i].Add(kvp.Value, kvp.Key);
                }
            }

            return returnList;
        }

        private static void AddToDictionary(Dictionary<string, int> ipAddressDictionary, string value)
        {
            int count;

            if (ipAddressDictionary.TryGetValue(value, out count))
            {
                ipAddressDictionary[value] = ++count;
            }
            else
            {
                ipAddressDictionary[value] = 1;
            }

        }
    }
}
