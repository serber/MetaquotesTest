using System;
using System.Text.RegularExpressions;

namespace Metaquotes.Geo
{
    public static class Converter
    {
        public const string IpRegex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        /// <summary>
        /// Преобразует ip адрес в <see cref="ulong"/>
        /// </summary>
        public static bool ToLong(string value, out ulong result)
        {
            if (!Regex.IsMatch(value, IpRegex))
            {
                result = 0;
                return false;
            }

            double num = 0;
            string[] ipBytes = value.Split('.');
            for (int i = ipBytes.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
            }

            result = (ulong)num;
            return true;
        }

        /// <summary>
        /// Создает <see cref="DateTime"/>
        /// </summary>
        public static DateTime ToDateTime(ulong value)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddSeconds(value).ToLocalTime();

            return date;
        }
    }
}