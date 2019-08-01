using Microsoft.AspNetCore.Components;
using System;
using Fluency.Execution;
using Fluency.Execution.Parsing;
using System.Collections.Generic;
using Fluency.Execution.Functions;
using Fluency.Common;
using System.Linq;

namespace FluencyOnline.Models
{
    public class ParseInterpret : ComponentBase
    {
        private IEnumerator<string> enumerator;

        protected Action<Exception> handler;

        private Value ReadInput()
        {
            if (enumerator.MoveNext())
            {
                return new Value(enumerator.Current, FluencyType.String);
            }
            else
            {
                return Value.Finished;
            }
            
        }

        protected bool StringBad(string isgood)
        {
            return string.IsNullOrWhiteSpace(isgood) || isgood.EndsWith(".");
        }

        protected IEnumerable<string> Go(string program, string input, bool verbose = false)
        {
            if (StringBad(program) || StringBad(input)) { return Enumerable.Empty<string>(); }

            Parser p = new Parser(verbose: verbose);

            Interpreter interp = new Interpreter(p);

            IEnumerable<string> split = input.Split('\n');
            enumerator = split.GetEnumerator();
            try
            {
                return interp.Execute(program.Split('\n'), ReadInput).Select(x => x.ToString()).ToArray();
            }
            catch (Exception ex)
            {
                handler(ex);
                return new string[0];
            }
        }

    }
}