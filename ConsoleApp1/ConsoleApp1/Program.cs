using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var strList = new List<string>()
            {
                "百度搜索引擎",
                "360搜索引擎%@！￥#",
                "就卡都会受到开始",
                "专家——长期失眠易引发多种疾病",
                "擎天柱"
            };

            var find = "引擎";
            var results = strList.Select(v => 
                                    new Tuple<string, double>(v, MatchingAlgorithm.MatchingLevel(find, v)))
                                    .OrderBy(v2 => v2.Item2).ToList();

            results.ForEach(v => Console.WriteLine($"{v.Item1} :  方差值 = {v.Item2}"));
            Console.Read();
        }
    }
}
