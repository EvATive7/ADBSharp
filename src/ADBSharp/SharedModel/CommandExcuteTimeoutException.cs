using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public class CommandExcuteTimeoutException:Exception
    {
        public override string Message => "The command execution cost longer than the specified maximum wait time.";
    }
}
