using System.Text;

namespace InstantIssueApp.Helpers
{
    public static class StringExtensions
    {
        public static string ToKebabCase(this string value)
        {
            return ToCase(value, '-');
        }

        public static string ToSnakeCase(this string value)
        {
            return ToCase(value, '_');
        }

        private static string ToCase(string value, char separator)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var sb = new StringBuilder();
            var isPrevCapital = false;
            var isPrevUnderscore = false;

            for (int i = 0, c = value.Length; i < c; i++)
            {
                if (value[i] >= 'A' && value[i] <= 'Z')
                {
                    if (i > 0 && !isPrevCapital && !isPrevUnderscore)
                        sb.Append(separator);

                    sb.Append(char.ToLower(value[i]));

                    isPrevCapital = true;
                    isPrevUnderscore = false;
                }
                else
                {
                    sb.Append(value[i]);

                    isPrevCapital = false;
                    isPrevUnderscore = value[i] == separator;
                }
            }

            return sb.ToString();
        }
    }
}
