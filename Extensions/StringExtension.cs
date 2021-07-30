using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatDelete.Extensions
{
    public static class StringExtension
    {
        public static string FixColorCodes(this string str) => new Regex("&(\\d|[a-f]|r)").Replace(str, "§$1");
    }
}
