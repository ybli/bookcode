using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace FileHelper
{
    public static class StringExt
    {
        //该类用于解决输出文本的时padright填充宽度受单双字节影响的问题;
        public static string PadLeftWhileDouble(this string input, int length, char paddingChar = ' ')
        {
            var singleLength = GetSingleLength(input);
            return input.PadLeft(length - singleLength + input.Length, paddingChar);
        }
        private static int GetSingleLength(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException();
            }
            return Regex.Replace(input, @"[^\x00-\xff]|[°′″]", "aa").Length;//计算得到该字符串对应单字节字符串的长度;
        }
        public static string PadRightWhileDouble(this string input, int length, char paddingChar = ' ')
        {
            var singleLength = GetSingleLength(input);
            return input.PadRight(length - singleLength + input.Length, paddingChar);
        }
    }
}
