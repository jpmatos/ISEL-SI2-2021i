using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace View.Abstract
{
    public abstract class AbstractView
    {
        private static readonly Regex RegexString = new Regex(@"^[a-zA-Z0-9 _-]*$");
        private static readonly Regex RegexInteger = new Regex(@"^[0-9]*$");

        protected static string[] Input(string str, List<Parameter> parameters)
        {
            string[] results = new string[parameters.Count];
            int i = 0;
            foreach (Parameter parameter in parameters)
            {
                bool cont = true;
                do
                {
                    Console.WriteLine($"{str} {parameter.name} {(parameter.nullable ? "" : "(not null)")}");
                    results[i] = Console.ReadLine();

                    if (results[i].Equals("null", StringComparison.InvariantCultureIgnoreCase) || results[i].Equals(""))
                    {
                        if(!parameter.nullable)
                            continue;
                        
                        results[i] = null;
                    }

                    cont = false;

                    if (results[i] != null)
                    {
                        if (parameter.type == typeof(string) && !RegexString.IsMatch(results[i]) ||
                            parameter.type == typeof(int) && !RegexInteger.IsMatch(results[i]))
                        {
                            throw new ArgumentException($"Invalid argument: {results[i]}");
                        }
                    }
                    i++;
                } while (cont);
            }
            return results;
        }
    }
}