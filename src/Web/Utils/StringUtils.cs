using System.Globalization;
using System.Text;

namespace Blog.Web.Utils
{
    public static class StringUtils
    {
        public static string SkipOneDot(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var r = s.Split(new[] { '.' }, 2);
            if (r.Length == 1)
            {
                return s;
            }

            return r[1];
        }

        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (!char.IsUpper(s[0]))
            {
                return s;
            }

            var b = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (i != 0 && (i + 1 < s.Length) && !char.IsUpper(s[i + 1]))
                {
                    b.Append(s.Substring(i));
                    break;
                }

                char value = char.ToLower(s[i], CultureInfo.InvariantCulture);
                b.Append(value);
            }

            return b.ToString();
        }
    }
}