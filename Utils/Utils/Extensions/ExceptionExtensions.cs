using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Utils.Extensions
{
    public static class ExceptionExtensions
    {
        public static string RabbitHole(this Exception ex, int level = 0,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            if (ex == null) return Environment.NewLine;

            var prePend = new string('\t', level);
            if (level == 0) level++;
            var subExceptions = RabbitHole(ex.InnerException, ++level);

            return $"{prePend}{ex.Message}{Environment.NewLine}{subExceptions}";
        }

        public static void WriteToFile(this string message, int level = 0,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            var contents =
                $"{DateTime.Now.ToLongTimeString()}\t|\t{caller} : {lineNumber}{Environment.NewLine}\t{message}{Environment.NewLine}";
            File.AppendAllText(@"C:\Temp\Logging.log", contents);
        }
    }
}