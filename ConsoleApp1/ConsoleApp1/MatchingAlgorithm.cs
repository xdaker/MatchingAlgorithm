using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MatchingAlgorithm
    {
        public const int NotMatchingCode = 99;

        /// <summary>
        /// 获取字符的匹配等级（没有一个匹配的就返回NotMatchingCode）
        /// </summary>
        /// <param name="actual">用户输入的字符</param>
        /// <param name="plan">数据库中的字符</param>
        /// <returns></returns>
        public static double MatchingLevel(string actual, string plan)
        {
            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(plan))
                return NotMatchingCode;
            var values = GetMatchingIndex(Filter(actual), Filter(plan));
            if (values == null || values.Count < 1) return NotMatchingCode;
            return Variance(values);
        }

        private static string _regexText = @"\p{P}|\p{S}";//匹配标点符号

        /// <summary>
        /// 过滤掉标点符号和空格，无用的字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Filter(string text)
        {
            var str = string.Empty;
            var match = Regex.Replace(text, _regexText, str);
            return match;
        }

        /// <summary>
        /// 获取每个字符所匹配到的索引(没有一个是匹配的，那么就返回null)
        /// </summary>
        /// <param name="actual">要索搜的字符</param>
        /// <param name="plan">用来匹配的字符</param>
        /// <returns></returns>
        private static List<double> GetMatchingIndex(string actual, string plan)
        {
            var list = new List<double>();
            int notMatchingNumber = 1;
            var actuals = actual.ToCharArray();
            for (int index = 0; index < actuals.Length; index++)
            {
                var pos = plan.IndexOf(actuals[index]);
                if (pos < 0)
                {
                    list.Add(NotMatchingCode * (notMatchingNumber++));
                    continue;
                }
                list.Add(Math.Abs(pos - index));
            }
            return notMatchingNumber - 1 >= actuals.Length ? null : list;
        }

        /// <summary>
        /// 方差
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double Variance(List<double> list)
        {
            if (list == null || list.Count <= 0) throw new Exception("Variance : list is null or Count = 0");
            var m = list.Sum() / list.Count;
            var a= Math.Sqrt(list.Sum(t => Math.Pow(t - m, 2) / list.Count));
            return a;
        }
    }
}
