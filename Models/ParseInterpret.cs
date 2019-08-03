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

        private static Parser p = new Parser();
            
        protected IEnumerable<string> Go(string program, string input, bool verbose = false)
        {
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