using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Parser.Test
{
    [TestClass]
    public class TopNListUnitTests
    {
        [TestMethod]
        public void TestListSize1()
        {
            TopNList list = new TopNList(3, "Description");

            foreach (var item in DataGenerator.GetDataSet1())
            {
                list.Add(item.Key, item.Value);
            }

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void TestListSize2()
        {
            TopNList list = new TopNList(5, "Description");

            foreach (var item in DataGenerator.GetDataSet1())
            {
                list.Add(item.Key, item.Value);
            }

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void TestListTopElements()
        {
            TopNList list = new TopNList(5, "Description");

            foreach (var item in DataGenerator.GetDataSet1())
            {
                list.Add(item.Key, item.Value);
            }

            var topElement = list.GetAt(0);
            var secondElement = list.GetAt(1);
            var thirdElement = list.GetAt(2);

            Assert.AreEqual(23, topElement.Key);
            Assert.AreEqual(17, secondElement.Key);
            Assert.AreEqual(10, thirdElement.Key);

            Assert.AreEqual("string 14", topElement.Value);
            Assert.AreEqual("string 8", secondElement.Value);
            Assert.AreEqual("string 24", thirdElement.Value);
        }
    }


    public class DataGenerator
    {
        public static IList<KeyValuePair<int, string>> GetDataSet1()
        {
            var list = new List<KeyValuePair<int, string>>();

            list.Add(new KeyValuePair<int, string>(4, "string 1"));
            list.Add(new KeyValuePair<int, string>(1, "string 2"));
            list.Add(new KeyValuePair<int, string>(2, "string 3"));
            list.Add(new KeyValuePair<int, string>(4, "string 4")); 
            list.Add(new KeyValuePair<int, string>(3, "string 5"));
            list.Add(new KeyValuePair<int, string>(8, "string 6")); 
            list.Add(new KeyValuePair<int, string>(4, "string 7"));
            list.Add(new KeyValuePair<int, string>(17, "string 8")); 
            list.Add(new KeyValuePair<int, string>(4, "string 9"));
            list.Add(new KeyValuePair<int, string>(4, "string 10")); 
            list.Add(new KeyValuePair<int, string>(4, "string 11"));
            list.Add(new KeyValuePair<int, string>(1, "string 12"));
            list.Add(new KeyValuePair<int, string>(4, "string 13"));
            list.Add(new KeyValuePair<int, string>(23, "string 14"));
            list.Add(new KeyValuePair<int, string>(4, "string 15"));
            list.Add(new KeyValuePair<int, string>(4, "string 16"));
            list.Add(new KeyValuePair<int, string>(3, "string 17"));
            list.Add(new KeyValuePair<int, string>(4, "string 18"));
            list.Add(new KeyValuePair<int, string>(4, "string 19"));
            list.Add(new KeyValuePair<int, string>(6, "string 20"));
            list.Add(new KeyValuePair<int, string>(7, "string 21"));
            list.Add(new KeyValuePair<int, string>(8, "string 22"));
            list.Add(new KeyValuePair<int, string>(9, "string 23"));
            list.Add(new KeyValuePair<int, string>(10, "string 24"));

            return list;
        }
    }
}
