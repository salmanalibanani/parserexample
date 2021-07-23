using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Parser
{
    public class RegexHelperBase
    {
        private string _regex;
        private string _description;

        public RegexHelperBase(string regex, string description)
        {
            _regex = regex;
            _description = description;
        }

        public bool Find(string line, out string result)
        {
            result = "";
            var match = Regex.Match(line, _regex, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                result = match.Value;
                return true;
            }

            return false;
        }

        public string Description
        {
            get => _description;
        }
    }

    public class IPRegexHelper : RegexHelperBase
    {
        public IPRegexHelper() : base("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)", "IP Addresses") { }
    }

    public class URLRegexHelper : RegexHelperBase
    {
        public URLRegexHelper() : base("(?<=GET)(.*)(?=HTTP)", "URLs") { }
    }
}
