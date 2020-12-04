using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        public static List<string> MandatoryFields = new List<string> {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        public static List<string> ValidEyeColors = new List<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        public static Regex ValidHairColors = new Regex(@"^#[0-9a-f]{6}$");

        public static Regex ValidPids = new Regex(@"^[0-9]{9}$");

        static void Main(string[] args)
        {
            int result = 0;
            string line;
            line = Console.ReadLine();

            do
            {
                StringBuilder fullId = new StringBuilder();
                do
                {
                    fullId.Append(" ");
                    fullId.Append(line);
                    line = Console.ReadLine();

                } while (!string.IsNullOrEmpty(line));

                if (IsValidPart2(fullId.ToString()))
                {
                    result++;
                }
                line = Console.ReadLine();
            } while (!string.IsNullOrEmpty(line));

            Console.WriteLine(result);
        }

        public static bool IsValidPart1(string line)
        {
            return line
                .Trim()
                .Split(' ')
                .ToList()
                .Select(field => field.Split(':').First())
                .Intersect(MandatoryFields)
                .Count() == MandatoryFields.Count;
        }

        public static bool IsValidPart2(string line)
        {
            Dictionary<string, string> idDocument = line
                .Trim()
                .Split(' ')
                .Select(kv => kv.Split(':'))
                .ToDictionary(x => x[0], x => x[1]);

            return idDocument.Keys.Intersect(MandatoryFields).Count() == MandatoryFields.Count
                   && ValidByr(idDocument["byr"])
                   && ValidIyr(idDocument["iyr"])
                   && ValidEyr(idDocument["eyr"])
                   && ValidHgt(idDocument["hgt"])
                   && ValidHairColor(idDocument["hcl"])
                   && ValidEyeColor(idDocument["ecl"])
                   && ValidPid(idDocument["pid"]);
        }

        public static bool ValidByr(string byrStr)
        {
            return int.TryParse(byrStr, out int byr) && byr >= 1920 && byr <= 2002;
        }

        public static bool ValidIyr(string iyrStr)
        {
            return int.TryParse(iyrStr, out int iyr) && iyr >= 2010 && iyr <= 2020;
        }

        public static bool ValidEyr(string eyrStr)
        {
            return int.TryParse(eyrStr, out int eyr) && eyr >= 2020 && eyr <= 2030;
        }

        public static bool ValidHgt(string hgtStr)
        {
            return hgtStr.EndsWith("cm") ? int.TryParse(hgtStr.Substring(0, hgtStr.Length - 2), out int hgtcm) && hgtcm >= 150 && hgtcm <= 193 :
                hgtStr.EndsWith("in") && (int.TryParse(hgtStr.Substring(0, hgtStr.Length - 2), out int hgtin) && hgtin >= 59 && hgtin <= 76);
        }

        public static bool ValidHairColor(string hcl)
        {
            return ValidHairColors.IsMatch(hcl);
        }

        public static bool ValidEyeColor(string ecl)
        {
            return ValidEyeColors.Contains(ecl);
        }

        public static bool ValidPid(string pidStr)
        {
            return ValidPids.IsMatch(pidStr);
        }
    }
}
