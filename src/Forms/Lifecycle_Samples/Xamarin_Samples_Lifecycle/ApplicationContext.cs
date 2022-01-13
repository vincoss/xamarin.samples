using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Samples_Lifecycle
{
    public static class ApplicationContext
    {
        private readonly static IList<string> Messages = new List<string>();

        static ApplicationContext()
        { }

        public static void Add(string message)
        {
            Messages.Add(message);
        }

        public static string GetMessageString()
        {
            var sb = new StringBuilder();
            
            foreach(var m in Messages)
            {
                sb.AppendLine(m);
            }

            return sb.ToString();
        }
    }
}
