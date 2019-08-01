using Microsoft.AspNetCore.Components;
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

        private IEnumerable<string> _input;
        protected IEnumerable<string> Input { set { _input = value; enumerator = null; } }

        private IEnumerator<string> enumerator;
        private Value ReadInput()
        {
            if (enumerator == null) { enumerator = _input.GetEnumerator(); }

            if (enumerator.MoveNext())
            {
                return new Value(enumerator.Current, FluencyType.String);
            }
            else
            {
                enumerator = null;
                return Value.Finished;
            }
            
        }

        protected IEnumerable<string> Go(string program, bool verbose = false)
        {
            if (string.IsNullOrWhiteSpace(program)) { return Enumerable.Empty<string>(); }
            Parser p = new Parser(verbose: verbose);

            Interpreter interp = new Interpreter(p);

            return interp.Execute(program.Split('\n'), ReadInput).Select(x => x.ToString());
        }
    }
}