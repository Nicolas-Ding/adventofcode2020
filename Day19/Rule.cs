using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day19
{
    public class Rule
    {
        public string Letter { get; set; }

        public List<List<int>> PossibleRules { get; set; }

        public (bool isMatch, int matchLength) IsMatch(Dictionary<int, Rule> allRules, string input)
        {
            var result = IsMatch_Internal(allRules, input);
            //Console.WriteLine($"Called IsMatch on input = {input} and rule {this}, result was {result.isMatch}");
            return result;
        }


        public (bool isMatch, int matchLength) IsMatch_Internal(Dictionary<int, Rule> allRules, string input)
        {
            if (!string.IsNullOrEmpty(Letter))
            {
                return (input.StartsWith(Letter), Letter.Length);
            }

            int position = 0;
            //Console.WriteLine($"Calling IsMatch on input = {input} and rule {this}. There are {PossibleRules.Count} possible rules");
            return (PossibleRules.Any(subRule =>
            {
                //Console.WriteLine($"  testing subrule {string.Join("", subRule)}");
                position = 0;
                foreach (int rule in subRule)
                {
                    var result = allRules[rule].IsMatch(allRules, input.Substring(position));
                    //Console.WriteLine($"    Called IsMatch on input = {input.Substring(position)}, position = {position} and rule {allRules[rule]}, result was {result.isMatch}");
                    if (!result.isMatch)
                    {
                        return false;
                    }
                    position += result.matchLength;
                }
                return true;
            }), position);
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Letter) ? Letter : string.Join('|', PossibleRules.Select(_ => string.Join("", _)));
        }
    }
}
