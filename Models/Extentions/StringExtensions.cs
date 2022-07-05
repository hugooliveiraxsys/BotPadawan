using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Extentions
{
    public static class StringExtensions
    {
        public static List<string> ToCommands(this string text)
        {
            return text.Split().ToList();
        }
    }
}
