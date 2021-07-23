using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    class Program
    {
        public const int NUMBER_OF_RESULTS = 3;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();

            string fileName = GetDataFileName(configuration);

            if (string.IsNullOrEmpty(fileName))
                return;
            
            IList<RegexHelperBase> regexList = new List<RegexHelperBase>();
            regexList.Add(new IPRegexHelper());
            regexList.Add(new URLRegexHelper());

            LogParser p = new LogParser(fileName, regexList, NUMBER_OF_RESULTS, File.ReadLines);
            
            var parsedLists = p.GetLists();

            foreach (var list in parsedLists)
            {
                ShowTopNResults(list.Description, list);
                Console.WriteLine("Unique items: " + list.TotalUniqueAttempted.ToString());
                Console.WriteLine("==============");
            }

            Console.ReadKey();
        }

        private static void ShowTopNResults(string message, TopNList topIPAddressList)
        {
            Console.WriteLine(message);

            for (int i = 0; i < topIPAddressList.Count; i++)
            {
                var item = topIPAddressList.GetAt(i);
                Console.WriteLine(item.Value.Trim() + " : " + item.Key.ToString());
            }
        }

        private static string GetDataFileName(IConfigurationRoot configuration)
        {
            string fileName = string.Empty;

            try
            {
                fileName = configuration.GetSection("Settings").GetChildren().FirstOrDefault(x => x.Key.Equals("DataFilePath")).Value;
            }
            catch (Exception e)
            {
                Console.WriteLine("Config file not found.");
            }

            return fileName;
        }
    }
}
