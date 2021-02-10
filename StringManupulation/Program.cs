using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StringManupulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            //string inputUS = "12/24/2020 03:00PM";
            //string inputUK = "24/12/20 3:00PM";
            //string inputSE = "2020-12-24 15:00:00";

            //List<string> dateFormats = new List<string>() { inputUS, inputUK, inputSE };
            //foreach (string input in dateFormats)
            //{
            //    printDate(input);
            //}

            Console.WriteLine("Please insert the date and time");
            string input = Console.ReadLine();
            printDate(input);
        }

        private static void printDate(string input)
        {
            string patternUS = @"\d{2}\/\d{2}\/\d{4} \d{1,2}\:\d{2}\s?((AM)|(PM)|(am)|(pm))";
            string patternUK = @"\d{2}\/\d{2}\/\d{2} \d{{1,2}}\:\d{2}\s?((AM)|(PM)|(am)|(pm))";
            string patternSE = @"\d{4}\/\d{2}\/\d{2} \d{{1,2}}\:\d{2}";
            Dictionary<string, string> patterns = new Dictionary<string, string>() {
                {"US", patternUS },
                {"UK", patternUK },
                {"SE", patternSE }
            };

            string dateFormatUS = @"MM/dd/yyyy hh:mmtt";
            string dateFormatUK = @"dd/MM/y h:mmtt";
            string dateFormatSE = @"yyyy-MM-dd HH:mm:ss";
            Dictionary<string, string> formats = new Dictionary<string, string>()
            {
                {"US", dateFormatUS },
                {"UK", dateFormatUK },
                {"SE", dateFormatSE }
            };

            foreach (string item in patterns.Keys)
            {
                Match m = Regex.Match(input, patterns[item]);
                DateTime result;
                if (DateTime.TryParseExact(m.Groups[0].ToString(), formats[item], System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    Console.WriteLine(
                       $"{result.Day} " +
                       $"of " +
                       $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(result.Month)} {result.Year} " +
                       $"at " +
                       $"{result.ToString("hh")}{result.ToString("tt")}");

                    break;
                }
            }
        }
    }
}
