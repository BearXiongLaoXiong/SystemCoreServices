namespace System.Framework.Common
{
    public static class StringExtension
    {
        public static string Left(this string param, int length)
        {
            if (param.Length > length)
                return param.Substring(0, length);
            return param;
        }

        public static string Right(this string param, int length)
        {
            return param.Length > length ? param.Substring(param.Length - length, length) : param;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }
        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }
    }
}
