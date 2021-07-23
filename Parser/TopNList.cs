using System;
using System.Collections;
using System.Collections.Generic;

namespace Parser
{
    public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            int result = y.CompareTo(x);

            if (result == 0)
                return 1; 
            else          
                return result;
        }   
    }

    public class TopNList 
    {
        private SortedList<int, string> _sortedList;
        private int _size;
        private string _description;
        private int _totalUniqueAttempted;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">Maximum number of elements allowed in the list</param>
        public TopNList(int size, string description)
        {
            _size = size;
            _description = description;
            _sortedList = new SortedList<int, string>(new DuplicateKeyComparer<int>());
        }

        public void Add(int key, string value)
        {
            _sortedList.Add(key, value);

            if (_sortedList.Count > _size)
            {
                _sortedList.RemoveAt(_size);
            }
            _totalUniqueAttempted++;
        }

        public int Count
        {
            get
            {
                return _sortedList.Count;
            }
        }

        public string Description
        {
            get => _description;
        }

        public int TotalUniqueAttempted
        {
            get => _totalUniqueAttempted;
        }

        public KeyValuePair<int, string> GetAt(int n)
        {
            if (n > _size)
                throw new InvalidOperationException();

            return new KeyValuePair<int, string>(new List<int>(_sortedList.Keys).ToArray()[n], new List<string>(_sortedList.Values).ToArray()[n]);
        }

    }
}
